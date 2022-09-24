using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This class is added to the Current Abilities on the Merge Abilities screen
public class CurrentAbilityMergeUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PriSecSlotUI priSlotUI;
    [SerializeField] private PriSecSlotUI secSlotUI;
    [SerializeField] private GameObject abilitySprite;

    public int smallSpriteSize = 60; // Sprite size in "Current Abilities" 

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (abilitySprite != null) {
            // Check for empty pri/sec slot
            bool isPriEmpty = priSlotUI.IsEmpty();
            bool isSecEmpty = secSlotUI.IsEmpty();

            if (isPriEmpty) {
                priSlotUI.AddAbility(abilitySprite);
                abilitySprite = null;
            } else if (isSecEmpty) {
                secSlotUI.AddAbility(abilitySprite);
                abilitySprite = null;
            }
        }
    }

    public void AddAbility(GameObject ability) {
        Transform abilityTransform = ability.transform;
        abilityTransform.SetParent(gameObject.transform);
        abilitySprite = ability;
        RectTransform rectTransform = abilityTransform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localScale = new Vector3(smallSpriteSize, smallSpriteSize, 1);
    }
}
