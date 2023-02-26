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


        public void AddItem(ItemData newItem)
        {
            if (!inventory.Contains(newItem) && equipSlots[(int)newItem.Type].EquippedItem != newItem)
            {
                inventory.Add(newItem);
            }
        }

        public void RemoveItem(ItemData removedItem)
        {
            inventory.Remove(removedItem);
        }

        //It should be noted that we can equip an item that is not inside inventory, and then if unequipped normally, it'll be inside inventory - that is intended behavior.
        public void ApplyEquipment(ItemData newItem)
        {
            foreach (var slot in equipSlots)
            {
                if (slot.AccountedPart == newItem.Type)
                {
                    var unequippedItem = slot.EquippedItem;
                    slot.EquippedItem = newItem;
                    RemoveItem(newItem);
                    if (unequippedItem != null && unequippedItem.IsFilled())
                        AddItem(unequippedItem);
                }
            }
        }

        public void RemoveEquipment(CharacterPart part)
        {
            foreach (var slot in equipSlots)
            {
                if (slot.AccountedPart == part)
                {
                    if (slot.EquippedItem != null && slot.EquippedItem.IsFilled())
                    {
                        var unequippedItem = slot.EquippedItem;
                        slot.EquippedItem = null;
                        AddItem(unequippedItem);
                    }
                }
            }
        }

        public List<ItemData> GetAllInventory()
        {
            return inventory;
        }

        public List<ItemData> GetInventoryForPart(CharacterPart targetPart)
        {
            var specificInventory = new List<ItemData>();
            foreach (var item in inventory)
            {
                if (item.Type == targetPart)
                {
                    specificInventory.Add(item);
                }
            }
            return specificInventory;
        }

        public List<ItemData> GetEquippedItems()
        {
            var equippedInventory = new List<ItemData>();
            foreach (var slot in equipSlots)
            {
                equippedInventory.Add(slot.EquippedItem);
            }
            return equippedInventory;
        }
    }
}