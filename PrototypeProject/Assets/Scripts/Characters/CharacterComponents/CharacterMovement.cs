using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Prototype.Characters.CharacterComponents
{
    /// <summary>
    /// This class controls actual movement and position of the character in game's space.
    /// For syncing position we're using DOTween package.
    /// </summary>
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] Rigidbody2D ownRigidbody;
        //If we're doing something like old RPGs or Stardew Valley, speed on X and Y axis are the same - but in other concepts they may not be
        [SerializeField] float movementSpeed;


        public void ApplyMovement(Vector2 movement)
        {
            //We're using sqrMagnitude so we don't have to calculate root, but want movementSpeed to limit top possible speed
            //That way diagonal movement is possible, will not be faster than axis-aligned speed, but will be somewhat computationally more expenive
            if (movement.sqrMagnitude > 1)
                movement.Normalize();
            ownRigidbody.MovePosition(ownRigidbody.position + movement * movementSpeed);
        }

        /// <summary>
        /// This method will sync character's position with worldPosition, over moveDuration (instantly if <= 0).
        /// This will ignore physics and can be used for teleportation.
        /// </summary>
        /// <param name="worldPosition"></param>
        /// <param name="moveDuration"></param>
        public void SyncPosition(Vector2 worldPosition, float moveDuration = -1)
        {
            if (moveDuration > 0)
            {
                ownRigidbody.DOMove(worldPosition, moveDuration);
            }
            else
            {
                ownRigidbody.position = worldPosition;
            }
        }
    }
}
