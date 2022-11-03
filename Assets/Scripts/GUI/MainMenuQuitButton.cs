using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuQuitButton : UIButton 
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        clickSfxHandler.PlaySfx();
        Application.Quit();
    }
}
