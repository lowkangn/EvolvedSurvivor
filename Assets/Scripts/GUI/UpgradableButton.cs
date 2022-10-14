using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class UpgradableButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Text textObj;
        [SerializeField] protected Image upgradableImage;
        [SerializeField] protected Upgradable upgradable;

        protected bool isEmpty = true;

        public bool IsEmpty()
        {
            return this.isEmpty;
        }

        public virtual void AddUpgradableToButton(Upgradable upgradable)
        {
            this.upgradable = upgradable;
            this.upgradableImage.gameObject.SetActive(true);
            this.upgradableImage.sprite = upgradable.GetSprite();
            this.isEmpty = false;
        }

        public abstract void OnPointerClick(PointerEventData eventData);

        // Detect if the Cursor starts to pass over the button
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (!IsEmpty())
            {
                this.textObj.text = upgradable.GetDescription();
            }
            else
            {
                this.textObj.text = "";
            }
        }

        // Detect when Cursor leaves the button
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            this.textObj.text = "";
        }

        public virtual void RemoveUpgradable()
        {
            if (!this.isEmpty)
            {
                this.isEmpty = true;
                this.upgradable = null;
                this.upgradableImage.sprite = null;
                this.upgradableImage.gameObject.SetActive(false);
            }
        }
    }
}

