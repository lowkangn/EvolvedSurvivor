using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class SpeedTuningPassiveAbility : PassiveAbility
    {
        public string AbilityName => abilityName;
        [SerializeField] private string abilityName;

        [SerializeField] private float[] speedMultipliers = new float[NUM_OF_TIERS];
        [SerializeField] private float[] radiusMultipliers = new float[NUM_OF_TIERS];

        private CharacterMovement movement;
        private float baseWalkSpeed;

        private CircleCollider2D pickupCollider;
        private float basePickupRadius;

        void Start()
        {
            CharacterMovement movement = transform.parent.gameObject.GetComponent<CharacterMovement>();
            this.baseWalkSpeed = movement.WalkSpeed;

            CircleCollider2D pickupCollider = transform.parent.Find("PickupRadius").GetComponent<CircleCollider2D>();
            this.basePickupRadius = pickupCollider.radius;
        }

        public override void Upgrade()
        {
            currentTier++;

            CharacterMovement movement = transform.parent.gameObject.GetComponent<CharacterMovement>();
            movement.WalkSpeed = this.baseWalkSpeed * speedMultipliers[currentTier];

            CircleCollider2D pickupCollider = transform.parent.Find("PickupRadius").GetComponent<CircleCollider2D>();
            pickupCollider.radius = this.basePickupRadius * radiusMultipliers[currentTier];
        }
    }
}
