using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamOne.EvolvedSurvivor;

// This class is added to the Current Abilities in Level Up screen
public class CurrentAbilityLevelUpUI : MonoBehaviour
{
    [SerializeField] private GameObject abilityInButton;
    public int smallSpriteSize = 60; // Sprite size in "Current Abilities" 

    public void AddAbilityToCurrent(GameObject abilitySprite) {

        // Destroy duplicate sprite that already exists in button
        if (abilityInButton != null) {
            Destroy(abilityInButton);
        }

        // Clone abilitySprite sprite and set its parent to 'current abilitySprite' button

        Transform abilityTransform = Instantiate(abilitySprite.transform);
        abilityTransform.SetParent(gameObject.transform);
        abilityInButton = abilityTransform.gameObject;
        RectTransform rectTransform = abilityTransform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localPosition = new Vector3(rectTransform.anchoredPosition.x,
                                    rectTransform.anchoredPosition.y, 0f); // To set z value to 0
        rectTransform.localScale = new Vector3(smallSpriteSize, smallSpriteSize, 1);
    }
}
