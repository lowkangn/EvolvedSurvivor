using UnityEngine;
namespace TeamOne.EvolvedSurvivor
{
    public class ConeAbilityHandler : AbilityHandler
    {
        private Orientation2D characterOrientation;
        [SerializeField]
        private ParticleSystem particles;

        private readonly float rateOverTime = 5f;
        private void OnEnable()
        {
            characterOrientation = GetComponentInParent<Orientation2D>();
        }

        public void UpdateParticles(float range, int coneNumber, float anglePerHalfCone)
        {
            var main = particles.main;
            main.startSpeed = new ParticleSystem.MinMaxCurve(0.9f * range, 1.1f * range);

            var emission = particles.emission;
            emission.rateOverTime = coneNumber * rateOverTime;

            var shape = particles.shape;
            shape.angle = Mathf.Clamp(coneNumber * anglePerHalfCone, 15f, 80f);
        }

        // Update is called once per frame
        void Update()
        {
            if (characterOrientation)
            {
                Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, characterOrientation.GetFacingDirection());
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
            }
        }
    }
}
