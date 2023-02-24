using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Characters.CharacterComponents;

namespace Prototype.Characters
{
    /// <summary>
    /// This is a main class that acts a singular (likely humanoid) character entity.
    /// It is expected we will inherit from it for some character-type specific behaviour.
    /// If we're trying to do something with the character from outside, we should refer to this component.
    /// </summary>
    public class Character : MonoBehaviour
    {
        [Header("Character Component References")]
        [SerializeField] CharacterMovement movement;
        [SerializeField] CharacterVisuals visuals;


        //We may not need empty virtuals right now, but I wrote them down in case I'll be needing them later so that other scripts can already call their base while overriding

        protected virtual void Awake()
        {
            
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnDestroy()
        {

        }

        public void Move(Vector2 movementAxis)
        {
            bool isMoving = movementAxis.sqrMagnitude > 0.1;
            visuals.SetMoving(isMoving);
            if (isMoving)
            {
                movement.ApplyMovement(movementAxis);
                visuals.FaceDirection(movementAxis);
            }
        }
    }
}