using TeamOne.EvolvedSurvivor;
using UnityEngine.EventSystems;

public class CurrentPassiveAbilityUI : UpgradableButton<PassiveAbility>
{
    public virtual void AddPassiveAbilityToButton(PassiveAbility passiveAbility)
    {
        base.AddUpgradableToButton(passiveAbility);
    }

    public virtual void RemoveAbility()
    {
        if (!this.isEmpty)
        {
            base.RemoveUpgradable();
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        // do nothing
    }

    // Detect when Cursor leaves the button
    public override void OnPointerExit(PointerEventData eventData)
    {
        this.textObj.text = "";
        this.detailedTextObj.text = "";
    }
}
