using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Settings;
using Prototype.Characters.CharacterComponents;

namespace Prototype.UI.InventoryComponents
{
    /// <summary>
    /// This class handles selection of slots that can hold equipped items for each slot (one for head, one for torso, et cetera)
    /// </summary>
    public class EquipmentGrid : MonoBehaviour
    {
        [SerializeField] ItemCyclicalSelector selector;


        public void Initialize(List<ItemData> equippedItems)
        {
            UpdateCells(equippedItems);
            selector.ChangeSelection(0, true);
        }


        public void SelectAbove()
        {
            selector.ChangeSelection(-1);
        }

        public void SelectBelow()
        {
            selector.ChangeSelection(1);
        }

        public void UpdateCells(List<ItemData> equippedItems)
        {
            for (var i = 0; i < selector.SelectionVariety.Count; i++)
            {
                selector.SelectionVariety[i].SetItem(equippedItems[i]);
            }
        }

        public CharacterPart GetHighlightedType()
        {
            return (CharacterPart)selector.SelectedIndex;
        }
    }
}