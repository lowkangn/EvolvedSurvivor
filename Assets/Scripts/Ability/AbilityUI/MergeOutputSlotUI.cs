using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TeamOne.EvolvedSurvivor;

public class MergeOutputSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool isEmpty;
    public GameObject textObj;
    private Ability ability;
    private GameObject abilitySprite;

    public int spriteSize = 230;

    void Start()
    {
        isEmpty = true;
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    public void AddAbility(Ability ability)
    {
        this.ability = ability;
        abilitySprite = Instantiate(ability.GetSprite());
        Transform abilityTransform = abilitySprite.transform;
        abilityTransform.SetParent(gameObject.transform);
        RectTransform rectTransform = abilityTransform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localPosition = new Vector3(rectTransform.anchoredPosition.x,
                                    rectTransform.anchoredPosition.y, 0f);
        rectTransform.localScale = new Vector3(spriteSize, spriteSize, 1);
        isEmpty = false;
    }

    public Ability GetAbility()
    {
        return this.ability;
    }

    public void ClearSlot()
    {
        Destroy(ability);
        Destroy(abilitySprite);
        isEmpty = true;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (isEmpty)
        {
            textObj.GetComponent<Text>().text = "Click on Current Abilities to merge them.";
        }
        else
        {
            textObj.GetComponent<Text>().text = "Description";
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (isEmpty)
        {
            textObj.GetComponent<Text>().text = "Click on Current Abilities to merge them.";
        }
        else
        {
            textObj.GetComponent<Text>().text = "";
        }
    }
}
