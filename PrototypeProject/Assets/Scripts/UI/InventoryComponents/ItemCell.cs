using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype.Settings;
using TMPro;

namespace Prototype.UI.InventoryComponents
{
    /// <summary>
    /// This is a multipurpose class that is used to view item in a grid; I think it has a potential to be very useful, but in this version it's not going to show all that much
    /// </summary>
    public class ItemCell : MonoBehaviour
    {
        [Header("Required Components")]
        [SerializeField] Animator ownAnim;
        [SerializeField] Image itemIcon;
        [SerializeField] Sprite placeholderSprite;

        [Header("Optional Components")]
        [SerializeField] TextMeshProUGUI itemName;
        [SerializeField] TextMeshProUGUI itemPrice;

        public ItemData ContainedItem { get; private set; }

        //We're caching an animator variable so we can restore it when object gets toggled
        private bool _isHighlighted;

        private void OnEnable()
        {
            SetHighlight(_isHighlighted);
        }


        public void SetItem(ItemData item)
        {
            if (itemName != null)
                itemName.text = (item != null && item.IsFilled() ? item.Name : "Empty");
            if (itemPrice != null)
                itemPrice.text = (item != null && item.IsFilled() ? item.Price.ToString() : "");
            itemIcon.sprite = (item != null && item.IsFilled() ? item.Icon : placeholderSprite);
            ContainedItem = item;
        }

        public void SetHighlight(bool isHighlighted)
        {
            _isHighlighted = isHighlighted;
            ownAnim.SetBool("Highlighted", isHighlighted);
        }
    }
}