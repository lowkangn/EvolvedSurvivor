using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelUpHeaderButton : MMTouchButton
{
    [SerializeField] private bool startsSelected;

    [SerializeField] private Color selectedButtonColor;
    [SerializeField] private Color selectedTextColor;
    [SerializeField] private Color unselectedTextColor;

    [Header("Button Text")]
    [SerializeField] private Text buttonText;

    [Header("Header buttons")]
    [SerializeField] private LevelUpHeaderButton[] headerButtons;

    private void Start()
    {
        ReturnToInitialSpriteAutomatically = false;

        if (startsSelected)
        {
            SetSelectAppearance();
        }
    }

    public override void OnPointerDown(PointerEventData data)
    {
        base.OnPointerDown(data);

        SetSelectAppearance();

        foreach (LevelUpHeaderButton headerButton in headerButtons)
        {
            headerButton.ResetAppearance();
        }
    }

    public void ResetAppearance()
    {
        _image.color = _initialColor;
        buttonText.color = unselectedTextColor;
    }

    private void SetSelectAppearance()
    {
        _image.color = selectedButtonColor;
        buttonText.color = selectedTextColor;
    }
}
