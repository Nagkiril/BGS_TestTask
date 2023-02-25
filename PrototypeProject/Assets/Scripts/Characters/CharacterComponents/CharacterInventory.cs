using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Settings;

namespace Prototype.Characters.CharacterComponents
{
    /// <summary>
    /// This class keeps track of equipment and can potentially be used to calculate application of effects\bonuses\penalties.
    /// It has weird dynamic with Character (cyclic dependency is avoidable), but it will also help us split logic and free up space in the main class (Character).
    /// We could also use this class to apply\remove equipment, but I decided against it so we won't be circumventing Character, which will order its components directly.
    /// </summary>
    public class CharacterInventory : MonoBehaviour
    {
        [SerializeField] EquipmentSlot[] equipSlots;
        [SerializeField] List<ItemData> inventory;


        [Serializable]
        private class EquipmentSlot
        {
            public CharacterPart AccountedPart;
            public ItemData EquippedItem;
        }



        public void ApplyEquipment(ItemData newItem)
        {
            foreach (var slot in equipSlots)
            {
                if (slot.AccountedPart == newItem.Type)
                {
                    slot.EquippedItem = newItem;
                }
            }
        }

        public void RemoveEquipment(CharacterPart part)
        {
            foreach (var slot in equipSlots)
            {
                if (slot.AccountedPart == part)
                {
                    slot.EquippedItem = null;
                }
            }
        }
    }
}