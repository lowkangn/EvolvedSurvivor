using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamOne.EvolvedSurvivor
{
    public class AbilityObjectPooler : MMSimpleObjectPooler
    {
        protected override GameObject AddOneObjectToThePool()
        {
            GameObject newObject = base.AddOneObjectToThePool();

            Ability newAbility = newObject.GetComponent<Ability>();
            Ability abilityToPool = GameObjectToPool.GetComponent<Ability>();
            newAbility.CloneAbility(abilityToPool);

            return newObject;
        }
    }
}
