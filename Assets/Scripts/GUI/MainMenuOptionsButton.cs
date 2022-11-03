using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuOptionsButton : UIButton
{
    [SerializeField] private GameObject optionsPanel;

    public override void OnPointerClick(PointerEventData eventData)
    {
        optionsPanel.SetActive(true);
        clickSfxHandler.PlaySfx();
    }
}
