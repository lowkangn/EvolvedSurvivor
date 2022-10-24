using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class SpeedTuningPassiveAbility : PassiveAbility
    {
        [SerializeField] private float[] speedMultipliers = new float[NUM_OF_TIERS + 1];
        [SerializeField] private float[] radiusMultipliers = new float[NUM_OF_TIERS + 1];

        [SerializeField] private CharacterMovement movement;
        private float baseWalkSpeed;

        [SerializeField] private CircleCollider2D pickupCollider;
        private float basePickupRadius;

        void Start()
        {
            baseWalkSpeed = movement.WalkSpeed;
            basePickupRadius = pickupCollider.radius;
        }

        public override void Upgrade()
        {
            currentTier++;
            movement.WalkSpeed = baseWalkSpeed * speedMultipliers[currentTier];
            pickupCollider.radius = basePickupRadius * radiusMultipliers[currentTier];
        }

        public override string GetDetails()
        {
            string speedIncrease = GeneralUtility.FloatToPercentString(speedMultipliers[currentTier]);
            string radiusIncrease = GeneralUtility.FloatToPercentString(radiusMultipliers[currentTier]);
            return $"+{speedIncrease} Movement Speed\n+{radiusIncrease} Pickup Radius";
        }
    }
}
