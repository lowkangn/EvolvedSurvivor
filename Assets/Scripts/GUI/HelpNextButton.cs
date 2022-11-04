using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelpNextButton : UIButton
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        clickSfxHandler.PlaySfx();
    }
}
