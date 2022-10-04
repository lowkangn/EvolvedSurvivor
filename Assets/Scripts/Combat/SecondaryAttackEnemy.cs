using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    // This class is used for enemies which have a secondary attack e.g., ranged enemies.
    public class SecondaryAttackEnemy : Enemy
    {
        [SerializeField] private float initialSecondaryDamage = 20f;
        [SerializeField] private DamageArea secDamageArea;
        [SerializeField] private float secDamageScalingFactor = 0.02f;

        private float currentSecondaryDamage;

        public float SecondaryDamage => currentSecondaryDamage;

        public override void ScaleStats(float timePassed)
        {
            base.ScaleStats(timePassed);

            this.currentSecondaryDamage = this.initialSecondaryDamage 
                + (this.initialSecondaryDamage * (Mathf.Log10(1f + (secDamageScalingFactor * timePassed))));

            if (this.secDamageArea != null)
            {
                Damage damage = new Damage();
                damage.damage = this.currentSecondaryDamage;

                damage = this.damageHandler.ProcessOutgoingDamage(damage);
                this.secDamageArea.SetDamage(damage);
            }
        }
    }
}
