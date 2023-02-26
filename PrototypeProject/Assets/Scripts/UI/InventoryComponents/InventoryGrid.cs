using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Settings;

namespace Prototype.UI.InventoryComponents
{
    /// <summary>
    /// This class handles selection of items inside an inventory grid.
    /// Might be worth renaming\changing namespace as this component also runs ShopOverlay (shop window).
    /// </summary>
    public class InventoryGrid : MonoBehaviour
    {
        [SerializeField] ItemCyclicalSelector selector;
        [SerializeField] Transform gridParent;
        [SerializeField] ItemCell inventoryCellPrefab;

        //I should note that UpdateCells and Initialize are exactly same for InventoryGrid, unlike EquipmentGrid
        public void Initialize(List<ItemData> shownInventoryItems)
        {
            //Although this is very much non-performant on big inventories I am severly lacking on time right now for much better.
            //I am trying to mitigate performance losses by only destroying\adding cells that *will* be used, and simply reusing existing ones
            if (shownInventoryItems.Count != selector.SelectionVariety.Count)
            {
                //This is also hacky because even if we're not changing pointer, we're modifying pointer's collection, which will reflect inside selector itself
                var newSelection = selector.SelectionVariety;
                while (newSelection.Count < shownInventoryItems.Count)
                {
                    newSelection.Add(Instantiate(inventoryCellPrefab, gridParent));
                }
                while (newSelection.Count > shownInventoryItems.Count)
                {
                    Destroy(newSelection[0].gameObject);
                    newSelection.RemoveAt(0);
                }
                //Ideally we would want to update selector like this after we make newSelection, yet this line should be redundant as of now
                selector.UpdateSelectionVariety(newSelection);
            }
            for (var i = 0; i < selector.SelectionVariety.Count; i++)
            {
                selector.SelectionVariety[i].SetItem(shownInventoryItems[i]);
            }
            selector.ChangeSelection(0, true);
        }


        public ItemData GetSelectedItem()
        {
            if (selector.SelectionVariety.Count == 0)
                return null;
            else
                return selector.SelectionVariety[selector.SelectedIndex].ContainedItem;
        }


        public void SelectNext()
        {
            selector.ChangeSelection(1);
        }

        public void SelectPrevious()
        {
            selector.ChangeSelection(-1);
        }
    }
}