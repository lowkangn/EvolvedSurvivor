using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelUpHeaderButton : MMTouchButton
{
    [SerializeField] private bool startsHighlighted = false;

    [SerializeField] private Color highlightedTextColor;
    [SerializeField] private Color normalTextColor;

    [Header("Button Text")]
    [SerializeField] private Text buttonText;

    private void Start()
    {
        if (startsHighlighted && HighlightedChangeColor)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            _image.color = HighlightedColor;
            buttonText.color = highlightedTextColor;
        }
    }

    public override void OnPointerDown(PointerEventData data)
    {
        base.OnPointerDown(data);

        if (HighlightedChangeColor)
        {    
            buttonText.color = highlightedTextColor;
        }
    }

    protected override void ResetButton()
    {
        base.ResetButton();

        buttonText.color = normalTextColor;
    }
}
