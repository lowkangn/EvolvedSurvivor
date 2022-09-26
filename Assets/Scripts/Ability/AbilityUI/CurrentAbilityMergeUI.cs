using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TeamOne.EvolvedSurvivor;

// This class is added to the Current Abilities on the Merge Abilities screen
public class CurrentAbilityMergeUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PriSecSlotUI priSlotUI;
    [SerializeField] private PriSecSlotUI secSlotUI;
    [SerializeField] private GameObject abilitySprite;
    private Ability ability;

    public int smallSpriteSize = 60; // Sprite size in "Current Abilities" 

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (abilitySprite != null) {
            // Check for empty pri/sec slot
            bool isPriEmpty = priSlotUI.IsEmpty();
            bool isSecEmpty = secSlotUI.IsEmpty();

            if (isPriEmpty) {
                priSlotUI.AddAbility(ability, abilitySprite);
                ability = null;
                abilitySprite = null;
            } else if (isSecEmpty) {
                secSlotUI.AddAbility(ability, abilitySprite);
                ability = null;
                abilitySprite = null;
            }
        }
    }

    public void AddAbility(Ability ability) {
        this.ability = ability;
        abilitySprite = Instantiate(ability.GetSprite());
        Transform abilityTransform = abilitySprite.transform;
        abilityTransform.SetParent(gameObject.transform);
        RectTransform rectTransform = abilityTransform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localPosition = new Vector3(rectTransform.anchoredPosition.x,
                                    rectTransform.anchoredPosition.y, 0f);
        rectTransform.localScale = new Vector3(smallSpriteSize, smallSpriteSize, 1);
    }

    public void AddAbility(Ability ability, GameObject abilitySprite)
    {
        this.ability = ability;
        this.abilitySprite = abilitySprite;
        Transform abilityTransform = abilitySprite.transform;
        abilityTransform.SetParent(gameObject.transform);
        RectTransform rectTransform = abilityTransform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localPosition = new Vector3(rectTransform.anchoredPosition.x,
                                    rectTransform.anchoredPosition.y, 0f);
        rectTransform.localScale = new Vector3(smallSpriteSize, smallSpriteSize, 1);
    }
}
