using UnityEngine;

namespace  MoreMountains.Tools
{
    /// <summary>
    /// A scriptable object containing a MMLootTable definition for game objects
    /// </summary>
    [CreateAssetMenu(fileName="LootDefinition",menuName="MoreMountains/Loot Definition")]
    public class MMLootTableGameObjectSO : ScriptableObject
    {
        /// the loot table 
        public MMLootTableGameObject LootTable;

        private int drop_probability = 75;

        /// returns an object from the loot table
        public virtual GameObject GetLoot()
        {
            int random_int = Random.Range(0, 100);
            if (random_int <= drop_probability) {
                return LootTable.GetLoot()?.Loot;
            } else {
                return null;
            }
        }
        
        /// <summary>
        /// computes the loot table's weights
        /// </summary>
        public virtual void ComputeWeights()
        {
            LootTable.ComputeWeights();
        }
    }
}
