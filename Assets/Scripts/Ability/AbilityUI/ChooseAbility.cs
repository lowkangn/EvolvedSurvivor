using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseAbility : MonoBehaviour, IPointerClickHandler
{
    private Transform child;
    private GameObject priSlot;
    private GameObject secSlot;

    void Awake() {
        priSlot = GameObject.Find("PrimarySlotButton");
        secSlot = GameObject.Find("SecondarySlotButton");
    }
    
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        child = gameObject.transform.Find("Ability");
        if (child != null) {
            // Check for empty pri/sec slot
            PriSecSlotUI priSlotUI = priSlot.GetComponent<PriSecSlotUI>();
            PriSecSlotUI secSlotUI = secSlot.GetComponent<PriSecSlotUI>();
            bool isPriEmpty = priSlotUI.getIsEmpty();
            bool isSecEmpty = secSlotUI.getIsEmpty();

            if (isPriEmpty) {
                priSlotUI.AddAbility(child);
            } else if (isSecEmpty) {
                secSlotUI.AddAbility(child);
            }
        }
    }

    public void AddAbility(Transform ability) {
        ability.SetParent(gameObject.transform);
        RectTransform rectTransform = ability.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localScale = new Vector3(60, 60, 1);
    }
}
