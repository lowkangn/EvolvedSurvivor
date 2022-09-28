using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TeamOne.EvolvedSurvivor;

public class MergeOutputSlotUI : AbilityMergeSlotUI
{
    public void ClearSlot()
    {
        if (this.ability != null)
        {
            Destroy(ability.gameObject);
        }

        this.RemoveAbility();
    }

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        // Do nothing.
    }
}
