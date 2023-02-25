using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Settings;
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
        [SerializeField] protected CharacterMovement movement;
        [SerializeField] protected CharacterVisuals visuals;
        [SerializeField] protected CharacterInventory inventory;
        public bool IsCharacterMovable { get; protected set; }


        //We may not need empty virtuals right now, but I wrote them down in case I'll be needing them later so that other scripts can already call their base while overriding

        protected virtual void Awake()
        {
            IsCharacterMovable = true;
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnDestroy()
        {

        }

        public void Move(Vector2 movementAxis)
        {
            bool isMoving = movementAxis.sqrMagnitude > 0.1 && IsCharacterMovable;
            visuals.SetMoving(isMoving);
            if (isMoving)
            {
                movement.ApplyMovement(movementAxis);
                visuals.FaceDirection(movementAxis);
            }
        }

        public void EquipItem(ItemData item)
        {
            visuals.ApplyItemLooks(item);
            inventory.ApplyEquipment(item);
        }


        public void UnequipFromPart(CharacterPart part)
        {
            visuals.ResetLooks(part);
            inventory.RemoveEquipment(part);
        }
    }
}