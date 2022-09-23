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
        GameObject child = FindChildWithTag(this.gameObject, "AbilitySprite");
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

    public void AddAbility(GameObject ability) {
        Transform abilityTransform = ability.transform;
        abilityTransform.SetParent(gameObject.transform);
        RectTransform rectTransform = abilityTransform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localScale = new Vector3(60, 60, 1);
    }

    GameObject FindChildWithTag(GameObject parent, string tag) {
        GameObject child = null;
        
        foreach(Transform transform in parent.transform) {
            if(transform.CompareTag(tag)) {
                child = transform.gameObject;
                break;
            }
        }
        
        return child;
    }  
}
