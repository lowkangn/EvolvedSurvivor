using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TeamOne.EvolvedSurvivor;
using UnityEngine.Events;

// This class is added to the primary & secondary ability slots on Merge Abilities screen
public class PriSecSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool isEmpty;
    public GameObject textObj;
    private Ability ability;
    private GameObject abilitySprite;
    private GameObject abilitySlot;

    public UnityEvent slotUpdated;

    public int priSpriteSize = 230; // Sprite size in Primary ability slot
    public int secSpriteSize = 180; // Sprite size in Secondary ability slot

    [SerializeField] private GameObject[] currentAbilities;

    void Start() {
        isEmpty = true;
    }

    public bool IsEmpty() {
        return isEmpty;
    }

    public void AddAbility(Ability ability, GameObject abilitySprite) {
        this.ability = ability;
        this.abilitySprite = abilitySprite;
        Transform abilityTransform = abilitySprite.transform;
        abilityTransform.SetParent(gameObject.transform);
        RectTransform rectTransform = abilityTransform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        if (gameObject.name == "PrimarySlotButton") {
            rectTransform.localScale = new Vector3(priSpriteSize, priSpriteSize, 1);
        } else if (gameObject.name == "SecondarySlotButton") {
            rectTransform.localScale = new Vector3(secSpriteSize, secSpriteSize, 1);
        }        
        isEmpty = false;
        slotUpdated.Invoke();
    }

    public Ability GetAbility()
    {
        return ability;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!isEmpty) { // Slot has an ability to be removed
            for (int i = 0; i < 6; i++) {
                if (currentAbilities[i].transform.childCount == 0) {
                    abilitySlot = currentAbilities[i];
                    abilitySlot.GetComponent<CurrentAbilityMergeUI>().AddAbility(ability, abilitySprite);
                    ability = null;
                    abilitySprite = null;
                    break;
                }
            }
            isEmpty = true;
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (isEmpty) {
            textObj.GetComponent<Text>().text = "Click on Current Abilities to merge them.";
        } else {
            textObj.GetComponent<Text>().text = "Description";
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (isEmpty) {
            textObj.GetComponent<Text>().text = "Click on Current Abilities to merge them.";
        } else {
            textObj.GetComponent<Text>().text = "";
        }
    }
}
