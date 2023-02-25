using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Environment;
using Prototype.UI;
using Prototype.Settings;
using Prototype.Characters.CharacterComponents;

namespace Prototype.Characters
{
    /// <summary>
    /// Player acts like a Character that is expected to be controlled by a human.
    /// I should note that it should be possible for us to control a non-Player Character, that may be useful for debugging or rare gameplay purposes (minigames, etc).
    /// </summary>
    public sealed class Player : Character, IEquipmentHost
    {
        //If our Inspector window will grow too large, we can introduce a private class to act as a category we can open\collapse on demand
        [Header("Player Pramaters")]
        //We could also write it off with one-liner "[field: SerializeField] public bool IsLocalPlayer { get; private set; }", but then we can't apply Header directly above
        [SerializeField] bool isLocalPlayer;
        [SerializeField] int coins;

        //We're not using an explicit method, because this operation is expected to be trivial throughout development
        public bool IsLocalPlayer { get => isLocalPlayer; }
        public static int Coins { get; private set; }

        public static event Action OnCoinsChanged;

        new void Start()
        {
            base.Start();
            if (isLocalPlayer)
            {
                CameraController.SetFollowTarget(transform);
                //Adding starting funds in a very hacky way
                ChangeCoins(500);
                //We can have an empty inventory, but just for the sake of testing here's a couple free items
                inventory.AddItem(ItemSettings.GetItemData("head_a"));
                inventory.AddItem(ItemSettings.GetItemData("torso_b"));
            }
        }


        //Coin mechanic could be handled by a PlayerWallet component, ideally somewhere in the Model if we would be using MVC-like structure for player data
        public static bool ChangeCoins(int coinChange)
        {
            bool isValidChange = coinChange > 0 || Coins >= coinChange;
            if (isValidChange)
            {
                Coins += coinChange;
                OnCoinsChanged?.Invoke();
            }
            return isValidChange;
        }

        public void OpenInventory()
        {
            if (IsCharacterMovable && InventoryOverlay.TryShowOverlay(this))
            {
                IsCharacterMovable = false;
            }
        }

        #region IEquipmentHost implementation

        public void HostEquipItem(ItemData targetItem)
        {
            EquipItem(targetItem);
        }

        public void HostUnequipSlot(CharacterPart targetPart)
        {
            UnequipFromPart(targetPart);
        }

        public void HostInventoryClosed()
        {
            IsCharacterMovable = true;
        }

        public List<ItemData> HostGetInventoryContent()
        {
            return inventory.GetAllInventory();
        }

        public List<ItemData> HostGetInventoryContent(CharacterPart specificPart)
        {
            return inventory.GetInventoryForPart(specificPart);
        }

        public List<ItemData> HostGetEquippedContent()
        {
            return inventory.GetEquippedItems();
        }


        #endregion
    }
}