using TeamOne.EvolvedSurvivor;
using UnityEngine.EventSystems;

// This class is added to the Current Abilities in Level Up screen
public class CurrentAbilityUI : AbilityButton
{
    private bool isEnabled;

    public void DisableButton()
    {
        isEnabled = false;
    }

    public void EnableButton()
    {
        isEnabled = true;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (isEnabled && ability != null)
        {

        }
    }
}
