using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using TeamOne.EvolvedSurvivor;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    [SerializeField] protected DamageHandler damageHandler;
    [SerializeField] protected CharacterMovement movement;
    [SerializeField] protected TopDownController2D controller;
    [SerializeField] protected AIBrain aiBrain;

    private List<SpeedMultiplier> speedMultipliers = new();
    private List<Freeze> freezes = new();
    private List<Slow> slows = new();
    private List<DamageOverTime> damageOverTimes = new();

    [SerializeField] private ParticleSystem plasmaEffect;
    [SerializeField] private ParticleSystem cryoEffect;
    [SerializeField] private ParticleSystem forceEffect;
    [SerializeField] private ParticleSystem infectEffect;
    [SerializeField] private ParticleSystem pyroEffect;

    private Character owner;

    private void Awake()
    {
        owner = GetComponent<Character>();
    }

    private void OnEnable()
    {
        plasmaEffect.gameObject.SetActive(true);
        cryoEffect.gameObject.SetActive(true);
        forceEffect.gameObject.SetActive(true);
        infectEffect.gameObject.SetActive(true);
        pyroEffect.gameObject.SetActive(true);
    }

    // Plasma
    public void ApplyChaining(Damage damage, GameObject target)
    {
        target.GetComponent<DamageReceiver>().TakeDamage(damage);
        plasmaEffect.Play();
    }

    // Cryo
    public void FreezeForDuration(float duration)
    {
        Freeze freeze = new Freeze(duration);
        speedMultipliers.Add(freeze);
        freezes.Add(freeze);
        UpdateSpeed();

        damageHandler.DisableOutgoingDamage();
        aiBrain.BrainActive = false;
    }

    // Force
    public void ApplyForce(Vector3 direction, float magnitude)
    {
        controller.Impact(direction, magnitude);
        forceEffect.Play();
    }

    // Infect
    public void SlowForDuration(float magnitude, float duration)
    {
        Slow slow = new Slow(magnitude, duration);
        speedMultipliers.Add(slow);
        slows.Add(slow);
        UpdateSpeed();
    }

    // Pyro
    public void DamageOverTimeForDuration(Damage damage, float tickRate, float duration)
    {
        DamageOverTime damageOverTime = new DamageOverTime(damage, tickRate, duration, damageHandler);
        damageOverTimes.Add(damageOverTime);
    }

    private void UpdateSpeed()
    {
        // Calculate combined speed multipliers
        float combinedSpeedMultiplier = 1f;
        speedMultipliers.ForEach(x => combinedSpeedMultiplier *= x.Value);

        // Override the old speed multiplier
        movement.ContextSpeedStack.Clear();
        movement.SetContextSpeedMultiplier(combinedSpeedMultiplier);
    }

    private void Update()
    {
        // Speed
        int numOfExpired = speedMultipliers.RemoveAll(x => x.Expired);
        if (numOfExpired > 0)
        {
            UpdateSpeed();
        }

        // Freeze
        freezes.RemoveAll(x => x.Expired);
        if (freezes.Count > 0)
        {
            cryoEffect.Play();
        }
        else
        {
            damageHandler.EnableOutgoingDamage();
            aiBrain.BrainActive = true;
            cryoEffect.Stop();
        }

        // Slow
        slows.RemoveAll(x => x.Expired);
        if (slows.Count > 0)
        {
            infectEffect.Play();
        }
        else
        {
            infectEffect.Stop();
        }

        // DamageOverTime
        damageOverTimes.RemoveAll(x => x.Expired);
        damageOverTimes.ForEach(x => x.Update());
        if (damageOverTimes.Count > 0)
        {
            pyroEffect.Play();
        }
        else
        {
            pyroEffect.Stop();
        }

        if (owner.ConditionState.CurrentState == CharacterStates.CharacterConditions.Dead)
        {
            plasmaEffect.gameObject.SetActive(false);
            cryoEffect.gameObject.SetActive(false);
            forceEffect.gameObject.SetActive(false);
            infectEffect.gameObject.SetActive(false);
            pyroEffect.gameObject.SetActive(false);
        }
    }

    private abstract class EffectOverTime
    {
        public bool Expired => Time.time - startTime > duration;
        protected float duration;
        protected float startTime;

        protected EffectOverTime(float duration)
        {
            this.duration = duration;
            startTime = Time.time;
        }
    }

    private class DamageOverTime : EffectOverTime
    {
        private Damage damage;
        private float tickRate;
        private float lastTickTime = 0f;
        private DamageHandler damageHandler;

        public DamageOverTime(Damage damage, float tickRate, float duration, DamageHandler damageHandler) : base(duration)
        {
            this.damage = damage;
            this.tickRate = tickRate;
            this.damageHandler = damageHandler;
        }

        public void Update()
        {
            if (Time.time - lastTickTime > tickRate)
            {
                damageHandler.ProcessIncomingDamage(damage);
                lastTickTime = Time.time;
            }
        }
    }

    private class SpeedMultiplier : EffectOverTime
    {
        public float Value => value;

        protected float value;
        
        public SpeedMultiplier(float value, float duration) : base(duration)
        {
            this.value = value;
        }
    }

    private class Freeze : SpeedMultiplier
    {
        public Freeze(float duration) : base(0, duration)
        {
            
        }
    }

    private class Slow : SpeedMultiplier 
    {
        public Slow(float value, float duration) : base(value, duration)
        {

        }
    }
}
