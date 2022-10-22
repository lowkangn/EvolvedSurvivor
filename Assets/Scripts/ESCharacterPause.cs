using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    public class ESCharacterPause : CharacterPause
    {
        // Modified method from TDE's CharacterPause: At the beginning, this method is called before initialisation occurs.
        // Therefore, if _condition is not yet set, this method should do nothing.
        public override void PauseCharacter()
        {
            if (!this.enabled)
            {
                return;
            }

            if (_condition != null)
            {
                _condition.ChangeState(CharacterStates.CharacterConditions.Paused);
            }
        }

        // Modified method from TDE's CharacterPause: At the beginning, this method is called before initialisation occurs.
        // Therefore, if _condition is not yet set, this method should do nothing.
        public override void UnPauseCharacter()
        {
            if (!this.enabled)
            {
                return;
            }

            if (_condition != null)
            {
                _condition.RestorePreviousState();
            }
        }
    }
}