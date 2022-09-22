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

    public void AddAbility(Transform ability) {
        ability.SetParent(gameObject.transform);
        RectTransform rectTransform = ability.GetComponent<RectTransform>();
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
                if (abilitySlot.transform.Find("Ability") == null) {
                    Transform ability = transform.Find("Ability");
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
    
}
