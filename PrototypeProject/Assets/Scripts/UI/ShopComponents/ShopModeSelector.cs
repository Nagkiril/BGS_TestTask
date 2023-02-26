using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.UI.ShopComponents
{
    /// <summary>
    /// This class controls which mode is active in the shop. Implementation of actions themselves is expected in ShopOverlay.
    /// Code is a bit simplistic here, but I don't think it needs to be complex.
    /// </summary>
    public class ShopModeSelector : MonoBehaviour
    {
        [SerializeField] ShopModeCard[] modeTitleCards;
        [field: SerializeField] public ShopMode Mode { get; private set; } 

        public void ToggleShopMode()
        {
            if (Mode == ShopMode.Buy)
                Mode = ShopMode.Sell;
            else
                Mode = ShopMode.Buy;
            UpdateModeCards();
        }

        public void Initialize()
        {
            Mode = ShopMode.Buy;
            UpdateModeCards();
        }

        void UpdateModeCards()
        {
            for (var i = 0; i < modeTitleCards.Length; i++)
            {
                modeTitleCards[i].SetSelected((int)Mode == i);
            }
        }
    }
}