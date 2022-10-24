using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    public class ESCharacterPause : CharacterPause
    {
        // Modified from TDE's CharacterPause: Due to how the level up works, the Player's CharacterPause is only initialised once the Player is first free to move.
        // Therefore, if _condition is not yet set, this method can just do nothing (will not cause unintended behaviour since, well, it's not initialised yet).
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

        // Same as above.
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
