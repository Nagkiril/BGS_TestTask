using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Settings.Generic;
using Prototype.Characters.CharacterComponents;

namespace Prototype.Settings
{
    [CreateAssetMenu(fileName = "ItemSettings", menuName = "Prototype/New Item Settings", order = 10)]
    public class ItemSettings : GenericSettings<ItemSettings>
    {
        //Items in the prototype are explicitly listed here
        //This can be an issue if we'll have a lot of items, as all of their resources could be loaded in at once
        //Instead it could be better to split them apart and load individually, maybe even as prefabs, via names\ids
        //To make matters better, we could also split data (ItemIconSettings, ItemSpriteSettings, etc) - harder to work with, but can handle more items simultaneously (inside inventories)
        [SerializeField] ItemData[] items;

        private const string _loadPath = "Settings/ItemSettings";
        private static ItemSettings instance => (ItemSettings)GetInstance(_loadPath);

        public static ItemData GetItemData(string itemId)
        {
            foreach (var item in instance.items)
            {
                if (item.Id == itemId)
                {
                    return item;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// General class that holds most of the data that can describe an item.
    /// It may be a good idea to split this data across multiple classes - harder to work with, but also easier to scale.
    /// </summary>
    [Serializable]
    public class ItemData
    {
        public string Id;
        //I hope to mention it somewhere, but if we're considering localization, there shouldn't be field "Name", instead display name should be looked up by using a localization key
        public string Name;
        public int Price;
        public CharacterPart Type;
        public Sprite Icon;
        public EquipmentSides[] EquipFacings;

        /// <summary>
        /// This class essentially stores sprites (and later, potentially details or other equipment-related stuff for them).
        /// Things like that are best filled in with a custom editor, which I don't think I can make in time.
        /// We could do similarly with multi-dimesional arrays instead of an inside class, but a class would be more helpful in long term as we expect adding more data here.
        /// </summary>
        [Serializable]
        public class EquipmentSides
        {
            public Sprite[] MainSprites;
        }

        public Sprite[] GetSprites(CharacterFacing face)
        {
            return EquipFacings[(int)face].MainSprites;
        }

        //I noticed that Unity seems to create empty instances, unfortunately it's getting too late for me to figure out exact circumstances, so we'll have this method as a bootleg
        //I'd -really- rather just compare with null and have a peace of mind
        public bool IsFilled()
        {
            return !string.IsNullOrEmpty(Id);
        }
    }
}