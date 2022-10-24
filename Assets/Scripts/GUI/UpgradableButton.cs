using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class UpgradableButton<T> : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler where T : Upgradable
    {
        [SerializeField] protected Text textObj;
        [SerializeField] protected Text detailedTextObj;
        [SerializeField] protected Image upgradableImage;
        [SerializeField] protected T upgradable;
        [SerializeField] protected RadarChartUI radarChart;

        protected bool isEmpty = true;

        public bool IsEmpty()
        {
            return this.isEmpty;
        }

        public virtual void AddUpgradableToButton(T upgradable)
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
                this.textObj.text = upgradable.GetName() + upgradable.GetDescription();
                this.detailedTextObj.text = upgradable.GetDetails();

                if (radarChart.isActiveAndEnabled)
                {
                    radarChart.UpdateVisual(upgradable);
                }
            }
        }

        // Detect when Cursor leaves the button
        public abstract void OnPointerExit(PointerEventData eventData);

        public virtual void RemoveUpgradable()
        {
            if (!this.isEmpty)
            {
                this.isEmpty = true;
                this.upgradable = default;
                this.upgradableImage.sprite = null;
                this.upgradableImage.gameObject.SetActive(false);
            }
        }
    }
}

