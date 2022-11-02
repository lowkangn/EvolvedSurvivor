using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [Header("Sound")]
    [SerializeField] protected SfxHandler clickSfxHandler;
    [SerializeField] protected SfxHandler enterSfxHandler;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (clickSfxHandler != null) clickSfxHandler.PlaySfx();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        // do nothing
    }
}
