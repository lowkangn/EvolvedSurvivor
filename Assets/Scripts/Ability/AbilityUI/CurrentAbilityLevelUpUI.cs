using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is added to the Current Abilities in Level Up screen
public class CurrentAbilityLevelUpUI : MonoBehaviour
{
    [SerializeField] private GameObject abilitySprite;
    public int smallSpriteSize = 60; // Sprite size in "Current Abilities" 

    public void AddAbilityToCurrent(GameObject ability) {

        // Destroy duplicate sprite that already exists in button
        if (abilitySprite != null) {
            Destroy(abilitySprite);
        }

        // Clone ability sprite and set its parent to 'current ability' button
        Transform abilityCopy = Instantiate(ability.transform);
        abilityCopy.SetParent(gameObject.transform);
        abilitySprite = abilityCopy.gameObject;
        RectTransform rectTransform = abilityCopy.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localPosition = new Vector3(rectTransform.anchoredPosition.x,
                                    rectTransform.anchoredPosition.y, 0f); // To set z value to 0
        rectTransform.localScale = new Vector3(smallSpriteSize, smallSpriteSize, 1);
    }
}
