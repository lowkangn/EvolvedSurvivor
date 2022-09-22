using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamOne.EvolvedSurvivor
{
    public class LockOnAbilityHandler : AbilityHandler
    {
        //private float timeOnScreen = 0.3f;
        public ParticleSystem particles;
        private float particleRadius = 0;

        private void OnEnable()
        {
            
            if (particleRadius > 0)
            {
                ParticleSystem.ShapeModule shape = particles.shape;
                shape.radius = particleRadius;
                GetComponent<DamageArea>().SetActive(true);
                particles.Play();
            }
            StartCoroutine(WaitAndKillCoroutine());
        }

        public void SetParticleRadius(float radius)
        {
            particleRadius = radius;
        }

        IEnumerator WaitAndKillCoroutine()
        {
            yield return new WaitUntil(() => particles.isStopped); ;
            StartCoroutine(KillCoroutine());
        }
    }
}