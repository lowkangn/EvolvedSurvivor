using TeamOne.EvolvedSurvivor;
using UnityEngine;
using UnityEngine.EventSystems;

public class CurrentPassiveAbilityUI : UpgradableButton
{
    [SerializeField] protected PassiveAbility passiveAbility;
    [SerializeField] private RadarChartUI radarChart;
    [SerializeField] private GameObject notAvailableBox;

    public virtual void AddPassiveAbilityToButton(PassiveAbility passiveAbility)
    {
        base.AddUpgradableToButton(passiveAbility);
        this.passiveAbility = passiveAbility;
    }

    public virtual void RemoveAbility()
    {
        if (!this.isEmpty)
        {
            base.RemoveUpgradable();
            this.passiveAbility = null;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        // do nothing
    }

    // Detect if the Cursor starts to pass over the button
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!isEmpty)
        {
            this.textObj.text = upgradable.GetName();
            this.detailedTextObj.text = upgradable.GetDescription();

            if (this.radarChart.gameObject.activeSelf)
            {
                this.notAvailableBox.SetActive(true);
            }
        }
    }

    // Detect when Cursor leaves the button
    public override void OnPointerExit(PointerEventData eventData)
    {
        this.textObj.text = "";
        this.detailedTextObj.text = "";

        this.notAvailableBox.SetActive(false);
    }
}
