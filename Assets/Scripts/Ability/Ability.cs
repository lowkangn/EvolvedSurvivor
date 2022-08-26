using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public abstract class Ability : MonoBehaviour
    {
        [Header("Whether this ability will be activated while it is active")]
        [SerializeField]
        private bool activateOnlyOnce = false;
        [Header("The ability is activated once every (coolDown) seconds")]
        [SerializeField]
        protected float coolDown;

        private bool hasBuilt = false;
        private bool hasActivated;
        private float coolDownTimer;

        /// <summary>
        /// Uses the score to (randomly) define the behaviours of the ability. 
        /// E.g., Speed, Damage, CoolDown, etc.
        /// MUST be called before the ability can be activated
        /// </summary>
        public void BuildAbility(int score)
        {
            Build(score);
            hasBuilt = true;
        }

        private void Update()
        {
            if (!hasBuilt)
            {
                return;
            }

            if (activateOnlyOnce)
            {
                if (!hasActivated)
                {
                    Activate();
                }
                return;
            }

            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer < 0f)
            {
                Activate();
                coolDownTimer = coolDown;
            }
        }

        protected abstract void Build(int score);

        protected abstract void Activate();
    }
}
