using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.UI.ShopComponents
{
    /// <summary>
    /// Titular card that displays a shop's mode. I think we could make some fancy procedural animations here, if there was time for it.
    /// </summary>
    public class ShopModeCard : MonoBehaviour
    {
        [SerializeField] Animator ownAnim;
        bool _isSelected;


        private void OnEnable()
        {
            ownAnim.SetBool("Selected", _isSelected);
        }

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;
            ownAnim.SetBool("Selected", _isSelected);
        }
    }
}