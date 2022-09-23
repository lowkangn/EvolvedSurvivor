using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAbility : MonoBehaviour
{
    public void AddAbilityToCurrent(GameObject ability) {

        // Destroy duplicate sprite that already exists in button
        GameObject child = FindChildWithTag(this.gameObject, "AbilitySprite");

        if (child != null) {
            Destroy(child);
        }

        // Clone ability sprite and set its parent to 'current ability' button
        Transform abilityCopy = Instantiate(ability.transform);
        abilityCopy.SetParent(gameObject.transform);
        RectTransform rectTransform = abilityCopy.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localPosition = new Vector3(rectTransform.anchoredPosition.x,
                                    rectTransform.anchoredPosition.y, 0f); // To set z value to 0
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
