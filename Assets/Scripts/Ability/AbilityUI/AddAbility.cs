using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAbility : MonoBehaviour
{
    public void AddAbilityToCurrent(Transform ability) {
        if (transform.Find("Ability(Clone)") != null) {
            Destroy(transform.Find("Ability(Clone)").gameObject);
        }
        Transform abilityCopy = Instantiate(ability);
        abilityCopy.SetParent(gameObject.transform);
        RectTransform rectTransform = abilityCopy.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localPosition = new Vector3(rectTransform.anchoredPosition.x,
                                    rectTransform.anchoredPosition.y, 0f);
        rectTransform.localScale = new Vector3(60, 60, 1);
    }
}
