using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Characters.CharacterComponents;
using Prototype.Settings;

namespace Prototype.UI
{
    /// <summary>
    /// This interface is for entities that contain some form of inventory and may change their equipment.
    /// Not 100% sure the namespace is an appropriate one, but I think it kinda fits.
    /// On a sidenote, I very rarely use interfaces, and rather proud that I could finally find a use case for them here!
    /// Usually them not playing nice with Inspector is what kills a lot of uses - I don't like being able to drag the wrong component and having to verify on runtime\in custom editor.
    /// They're also often replaceable by components, narrowing uses even further.
    /// However, they can also be used to limit access to certain methods (for example: limiting some Model methods only to be used inside a Controller, but not outside them both)
    /// </summary>
    public interface IEquipmentHost
    {
        void HostEquipItem(ItemData targetItem);
        void HostUnequipSlot(CharacterPart targetPart);
        void HostOverlayRelease();
        void HostInventoryBuy(ItemData targetItem);
        void HostInventorySell(ItemData targetItem);
        List<ItemData> HostGetInventoryContent();
        List<ItemData> HostGetInventoryContent(CharacterPart specificPart);
        List<ItemData> HostGetEquippedContent();
    }
}