using UnityEngine;
namespace TeamOne.EvolvedSurvivor
{
    public class ConeAbilityHandler : AbilityHandler
    {
        private Orientation2D characterOrientation;
        private void OnEnable()
        {
            characterOrientation = GetComponentInParent<Orientation2D>();
        }
        // Update is called once per frame
        void Update()
        {
            if (characterOrientation)
            {
                Debug.Log(characterOrientation.GetFacingDirection());
                transform.rotation = Quaternion.FromToRotation(Vector3.up, characterOrientation.GetFacingDirection());
                Debug.Log(transform.rotation);
            }
        }
    }
}
