using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.UI.InventoryComponents;
using Prototype.UI.ShopComponents;
using Prototype.Settings;

namespace Prototype.UI
{
    /// <summary>
    /// This class controls shop overlay\window. Very similar to InventoryOverlay.
    /// There's some duplicate code between the two that could be put into a parent or component.
    /// I think it may be worth to splitting them into several components.
    /// </summary>
    public class ShopOverlay : MonoBehaviour
    {
        [SerializeField] Animator mainAnim;
        [SerializeField] AnimationEventSet animEvents;
        [SerializeField] ShopModeSelector modeSelector;
        [SerializeField] InventoryGrid gridInventory;
        static ShopOverlay _instance;

        bool _isInTransition;
        bool _isShown;
        IEquipmentHost _targetHost;



        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                animEvents.OnAnimationEnd += OnAnimationFinish;
            } else
            {
                Debug.LogWarning($"There are multiple ShopOverlays on the scene! Excessive component in {gameObject.name} will be destroyed.");
                Destroy(gameObject);
            }
        }


        void Update()
        {
            if (_isShown)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    gridInventory.SelectPrevious();
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    gridInventory.SelectNext();
                }
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                {
                    modeSelector.ToggleShopMode();
                    UpdateInventoryGrid();
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    TransferSelectedItem();
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    SetVisibility(false);
                }
            }
        }

        void OnAnimationFinish()
        {
            _isInTransition = false;
        }

        void TransferSelectedItem()
        {
            var selectedItem = gridInventory.GetSelectedItem();
            if (selectedItem != null)
            {
                if (modeSelector.Mode == ShopMode.Buy)
                {
                    //We can make that method return bool, thus only rebuilding inventory if needed
                    _targetHost.HostInventoryBuy(selectedItem);
                } else
                {
                    _targetHost.HostInventorySell(selectedItem);
                }
            }
            UpdateInventoryGrid();
        }

        void SetVisibility(bool isVisible)
        {
            if (!_isInTransition)
            {
                _isShown = isVisible;
                mainAnim.SetBool("Show", isVisible);
                if (isVisible)
                {
                    modeSelector.Initialize();
                    UpdateInventoryGrid();
                }
                else
                {
                    _targetHost.HostOverlayRelease();
                }
                _isInTransition = true;
            }
        }

        void UpdateInventoryGrid()
        {
            if (modeSelector.Mode == ShopMode.Buy)
            {
                //This feels awfully computationally expensive (ok for a prototype), but I don't have any better ideas at the moment
                var finalSelection = GetShopSelection();
                //Need to be careful working with those lists (or just make sure methods return copies)
                var ownedItems = _targetHost.HostGetEquippedContent();
                ownedItems.AddRange(_targetHost.HostGetInventoryContent());
                finalSelection.RemoveAll(x => ownedItems.Contains(x));
                gridInventory.Initialize(finalSelection);
            } else
            {
                gridInventory.Initialize(_targetHost.HostGetInventoryContent());
            }
        }

        List<ItemData> GetShopSelection()
        {
            //Here we could implement a selection of items specific for different shops; in this prototype, however, we have just 1 shop with all the items possible
            return ItemSettings.GetAllItems();
        }


        public static bool TryShowOverlay(IEquipmentHost host)
        {
            if (!_instance._isShown && !_instance._isInTransition)
            {
                _instance._targetHost = host;
                _instance.SetVisibility(true);
                return true;
            } else
            {
                return false;
            }
        }
    }
}