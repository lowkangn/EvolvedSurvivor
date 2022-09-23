using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PriSecSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool isEmpty;
    public GameObject textObj;

    void Start() {
        isEmpty = true;
    }

    public bool getIsEmpty() {
        return isEmpty;
    }

    public void AddAbility(GameObject ability) {
        Transform abilityTransform = ability.transform;
        abilityTransform.SetParent(gameObject.transform);
        RectTransform rectTransform = abilityTransform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        if (gameObject.name == "PrimarySlotButton") {
            rectTransform.localScale = new Vector3(230, 230, 1);
        } else if (gameObject.name == "SecondarySlotButton") {
            rectTransform.localScale = new Vector3(180, 180, 1);
        }        
        isEmpty = false;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!isEmpty) { // Slot has an ability to be removed
            for (int i = 1; i < 7; i++) {
                // Find the first empty currentAbilities slot
                GameObject abilitySlot = GameObject.Find("CurrentAbilities/Button" + i.ToString());
                if (FindChildWithTag(abilitySlot, "AbilitySprite") == null) {
                    GameObject ability = FindChildWithTag(this.gameObject, "AbilitySprite");
                    abilitySlot.GetComponent<ChooseAbility>().AddAbility(ability);
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
