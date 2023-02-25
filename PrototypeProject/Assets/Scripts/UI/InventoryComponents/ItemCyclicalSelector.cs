using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.UI.InventoryComponents
{
    /// <summary>
    /// I noticed that InventoryGrid and EquipmentGrid share some code, which is now delegated to their subcomponent - this class.
    /// I was planning to make it their common parent, but decided against it as I could easily replace it with composition.
    /// I expect it will be useful later when I make the Shop, too.
    /// </summary>
    public class ItemCyclicalSelector : MonoBehaviour
    {
        [field: SerializeField] public List<ItemCell> SelectionVariety { get; private set; }
        public int SelectedIndex { get; private set; }
        private bool _selectedSlotExists => SelectedIndex < SelectionVariety.Count && SelectionVariety[SelectedIndex] != null;

        public void ChangeSelection(int selectionChange, bool hardSetSelection = false)
        {
            if (_selectedSlotExists)
                SelectionVariety[SelectedIndex].SetHighlight(false);
            if (hardSetSelection)
            {
                SelectedIndex = 0;
            } else
            {
                SelectedIndex += selectionChange;
                if (SelectedIndex >= SelectionVariety.Count)
                    SelectedIndex = 0;
                if (SelectedIndex < 0)
                    SelectedIndex = (SelectionVariety.Count > 0 ? SelectionVariety.Count - 1 : 0);
            }
            if (_selectedSlotExists)
                SelectionVariety[SelectedIndex].SetHighlight(true);
        }



        public void UpdateSelectionVariety(List<ItemCell> newSelection)
        {
            SelectionVariety = newSelection;
            if (SelectionVariety == null)
                SelectionVariety = new List<ItemCell>();
            ChangeSelection(0, true);
        }
    }
}