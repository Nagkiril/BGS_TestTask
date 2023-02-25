using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.UI.InventoryComponents;
using Prototype.Characters;
using Prototype.Characters.CharacterComponents;
using Prototype.Settings;

namespace Prototype.UI
{
    /// <summary>
    /// This class controls inventory overlay. Very similar concept to "Character" being main controller for its components, similar vibe is here too.
    /// Exception is that inputs from player are received here too, because I don't think there would be enough code to fill in a new component dedicated solely for inputs.
    /// </summary>
    public class InventoryOverlay : MonoBehaviour
    {
        [SerializeField] Animator mainAnim;
        [SerializeField] AnimationEventSet animEvents;
        [SerializeField] EquipmentGrid gridEquip;
        [SerializeField] InventoryGrid gridInventory;
        static InventoryOverlay _instance;

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
                Debug.LogWarning($"There are multiple InventoryOverlays on the scene! Excessive component in {gameObject.name} will be destroyed.");
                Destroy(gameObject);
            }
        }


        void Update()
        {
            if (_isShown)
            {
                //I mentioned hacks over comments quite a lot, but I think this is among the ugliest ones
                if (Input.GetKeyDown(KeyCode.A))
                {
                    gridInventory.SelectPrevious();
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    gridInventory.SelectNext();
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    gridEquip.SelectAbove();
                    //In these specific cases, there's no need to update both grids at once - we only need to update gridInventory (even if it is the heavier of the two)
                    gridInventory.Initialize(_targetHost.HostGetInventoryContent(gridEquip.GetHighlightedType()));
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    gridEquip.SelectBelow();
                    gridInventory.Initialize(_targetHost.HostGetInventoryContent(gridEquip.GetHighlightedType()));
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    EquipSelectedItem();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    UnequipSelectedItems();
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SetVisibility(false);
                }
            }
        }

        void OnAnimationFinish()
        {
            _isInTransition = false;
        }

        void EquipSelectedItem()
        {
            var targetInventoryItem = gridInventory.GetSelectedItem();
            if (targetInventoryItem != null)
                _targetHost.HostEquipItem(targetInventoryItem);
            UpdateAllGrids();
        }

        void UnequipSelectedItems()
        {
            _targetHost.HostUnequipSlot(gridEquip.GetHighlightedType());
            UpdateAllGrids();
        }

        void SetVisibility(bool isVisible)
        {
            if (!_isInTransition)
            {
                _isShown = isVisible;
                mainAnim.SetBool("Show", isVisible);
                if (isVisible)
                {
                    gridInventory.Initialize(_targetHost.HostGetInventoryContent(gridEquip.GetHighlightedType()));
                    gridEquip.Initialize(_targetHost.HostGetEquippedContent());
                }
                else
                {
                    _targetHost.HostInventoryClosed();
                }
                _isInTransition = true;
            }
        }

        void UpdateAllGrids()
        {
            gridInventory.Initialize(_targetHost.HostGetInventoryContent(gridEquip.GetHighlightedType()));
            gridEquip.UpdateCells(_targetHost.HostGetEquippedContent());
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