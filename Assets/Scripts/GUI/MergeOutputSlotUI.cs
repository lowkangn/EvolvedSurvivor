using TeamOne.EvolvedSurvivor;
using UnityEngine.EventSystems;

public class MergeOutputSlotUI : AbilityMergeSlotUI
{
    public void ClearSlot()
    {
        if (this.upgradable != null)
        {
            upgradable.ClearAnyRecursive();
            Destroy(upgradable.gameObject);
        }

        this.RemoveAbility();
    }

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        // Do nothing.
    }
}
