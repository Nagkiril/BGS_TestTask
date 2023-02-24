using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Characters.CharacterComponents.VisualSetup;

namespace Prototype.Characters.CharacterComponents
{
    /// <summary>
    /// This class implements visual behaviour that character's model should follow, such as turning to cardinal direction, performing walking cycle, etc.
    /// </summary>
    public class CharacterVisuals : MonoBehaviour
    {
        //We're using animator statemachine here to handle most of the visual transitions
        [SerializeField] Animator ownAnim;
        [SerializeField] CharacterAnimationEvents animEvents;
        [SerializeField] CharacterVisualDirection[] characterDirections;

        CharacterFacing _currentFacing;
        bool _isMoving;

        private void ApplyFacing(CharacterFacing newFacing)
        {
            if (_currentFacing != newFacing)
            {
                _currentFacing = newFacing;
                //Feels kinda hacky, not sure if performant either, but at least it'll only run if we actually need to turn the character
                foreach (string facingName in Enum.GetNames(typeof(CharacterFacing)))
                {
                    //Although pretty redundant in the prototype, it is possible we'll try to rotate a character whose visuals should not be rotated while in certain states
                    //To implement that, we can add more variables in the Animator, and here we're already clearing trigger queue, so that only the most recent turn order applies
                    ownAnim.ResetTrigger(facingName);
                }

                ownAnim.SetTrigger(_currentFacing.ToString());
                //As an alternative, we can have 3 animators for each facing separately and programmatically switch them on and off
                //Advantage of that would be us getting rid of potential glitching (especially on lower FPS) due to transitions
                //Disadvantage would be so that we'd have to reset animator variables each time we switch it on, which still can be mitigated with us caching those variables
                //Although we're already caching movement variable, this may be challenging if we'll have more complex states.
            }
        }

        public void ApplyCustomization()
        {

        }

        public void SetMoving(bool isMoving)
        {
            //We're caching animator variable so we won't have to reapply it potentially each frame (and wouldn't have to call GetBool on animator each frame either) 
            if (_isMoving != isMoving)
            {
                _isMoving = isMoving;
                ownAnim.SetBool("IsMoving", isMoving);
            }
        }

        public void FaceDirection(Vector2 direction2D)
        {
            //If we're not really having a new direction, it seems like a good idea to maintain current direction
            if (direction2D != Vector2.zero)
            {
                CharacterFacing newFacing;
                //Moving diagonally would make us face to the side
                if (direction2D.x != 0)
                {
                    //If we're moving directly left or right, we'll show the side sprite
                    newFacing = CharacterFacing.Side;
                    //To make sure we won't have duplicated aimations, we'll flip the character on side change; I think some years back Unity 2D Platformer was working very similarly
                    //I think conditional can be rewritten to be more efficient, but I doubt this case can be a bottleneck in the game
                    var sideVisualsCore = characterDirections[(int)CharacterFacing.Side].transform;
                    if (Mathf.Sign(sideVisualsCore.localScale.x) != Mathf.Sign(direction2D.x))
                    {
                        Vector3 newLocalScale = sideVisualsCore.localScale;
                        newLocalScale.x *= -1;
                        sideVisualsCore.localScale = newLocalScale;
                    }

                } else
                {
                    newFacing = (direction2D.y > 0 ? CharacterFacing.Backward : CharacterFacing.Forward);
                }
                ApplyFacing(newFacing);
            }
        }

        /// <summary>
        /// Triggers specific animation to be performed by the character
        /// </summary>
        public void PerformAction(string actionName)
        {
            //If some of our characters could have drastically different model prefabs capable of only specific, variable set of actions
            //we could verify and return bool whether or not a specific action is possble; this functionality wouldn't be useful in the prototype, however
            ownAnim.SetTrigger(actionName);
        }
    }

    public enum CharacterFacing
    {
        Forward = 0,
        Backward = 1,
        Side = 2,
    }

    public enum CharacterPart
    {
        Head,
        Torso,
        Hands,
        Legs
    }
}