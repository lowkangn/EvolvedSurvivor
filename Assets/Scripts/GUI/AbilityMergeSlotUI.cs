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
    [SerializeField] private Animator animator;

    private CurrentAbilityUI sourceSlot;

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        ClearSelection();
    }

    public void ClearSelection()
    {
        if (!this.isEmpty)
        {
            this.sourceSlot.AddAbilityToButton(this.upgradable);
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
        } 
        else 
        {
            base.OnPointerEnter(pointerEventData);
            this.animator.SetBool("isHovering", true);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        this.animator.SetBool("isHovering", false);
    }

    public Ability GetAbility()
    {
        return this.upgradable;
    }
}
