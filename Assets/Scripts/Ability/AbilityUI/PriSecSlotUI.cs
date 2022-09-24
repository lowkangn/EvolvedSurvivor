using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// This class is added to the primary & secondary ability slots on Merge Abilities screen
public class PriSecSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool isEmpty;
    public GameObject textObj;
    private GameObject abilitySlot;

    public int priSpriteSize = 230; // Sprite size in Primary ability slot
    public int secSpriteSize = 180; // Sprite size in Secondary ability slot

    [SerializeField] private GameObject abilityToMerge;
    [SerializeField] private GameObject[] currentAbilities;

    void Start() {
        isEmpty = true;
    }

    public bool getIsEmpty() {
        return isEmpty;
    }

    public void AddAbility(GameObject ability) {
        Transform abilityTransform = ability.transform;
        abilityTransform.SetParent(gameObject.transform);
        abilityToMerge = ability;
        RectTransform rectTransform = abilityTransform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        if (gameObject.name == "PrimarySlotButton") {
            rectTransform.localScale = new Vector3(priSpriteSize, priSpriteSize, 1);
        } else if (gameObject.name == "SecondarySlotButton") {
            rectTransform.localScale = new Vector3(secSpriteSize, secSpriteSize, 1);
        }        
        isEmpty = false;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!isEmpty) { // Slot has an ability to be removed
            for (int i = 0; i < 6; i++) {
                if (currentAbilities[i].transform.childCount == 0) {
                    abilitySlot = currentAbilities[i];
                    abilitySlot.GetComponent<CurrentAbilityMergeUI>().AddAbility(abilityToMerge);
                    abilityToMerge = null;
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
