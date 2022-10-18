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
        [SerializeField] private Transform pickupRingTransform;
        private float basePickupRadius;
        private float basePickupRingScale;

        void Start()
        {
            baseWalkSpeed = movement.WalkSpeed;
            basePickupRadius = pickupCollider.radius;
            basePickupRingScale = pickupRingTransform.localScale.x;
        }

        public override void Upgrade()
        {
            currentTier++;
            movement.WalkSpeed = baseWalkSpeed * speedMultipliers[currentTier];
            pickupCollider.radius = basePickupRadius * radiusMultipliers[currentTier];
            pickupRingTransform.localScale = new Vector3(basePickupRingScale * radiusMultipliers[currentTier], basePickupRingScale * radiusMultipliers[currentTier], basePickupRingScale * radiusMultipliers[currentTier]);
        }

        protected override string GetStatsDescription()
        {
            return "Movement Speed Multiplier: " + speedMultipliers[currentTier] + "x\nPickup Radius Multiplier: " + radiusMultipliers[currentTier] + "x\n";
        }
    }
}
