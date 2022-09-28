using UnityEngine;
using UnityEngine.EventSystems;
using TeamOne.EvolvedSurvivor;
using MoreMountains.TopDownEngine;

// This class is added to the primary & secondary ability slots on Merge Abilities screen
public class AbilityMergeSlotUI : AbilityButton
{
    private const string TIP_MERGE_INSTRUCTION = "Click on Current Abilities to merge them.";

    [SerializeField] protected MergeAbilityHandler mergeAbilityHandler;
    [SerializeField] protected LevelUpScreenManager levelUpManager;

    private CurrentAbilityUI sourceSlot;

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        ClearSelection();
    }

    public void ClearSelection()
    {
        if (!this.isEmpty)
        {
            this.sourceSlot.AddAbilityToButton(this.ability);
            this.RemoveAbility();
            this.mergeAbilityHandler.UpdateOutput();
        }
    }

    public void AddAbilityToButton(Ability ability, CurrentAbilityUI sourceSlot)
    {
        this.AddAbilityToButton(ability);
        this.sourceSlot = sourceSlot;
    }

    public override void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (this.isEmpty) {
            this.textObj.text = TIP_MERGE_INSTRUCTION;
        } else {
            this.textObj.text = ability.GetDescription();
        }
    }

    public Ability GetAbility()
    {
        return this.ability;
    }
}
