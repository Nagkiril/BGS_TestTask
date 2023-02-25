using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Characters.CharacterComponents.VisualSetup
{
    public class CharacterVisualDirection : MonoBehaviour
    {
        [SerializeField] VisualCharacterPart[] parts;


        [Serializable]
        protected class VisualCharacterPart
        {
            //Although we can still cast CharacterPart to integer and this way get index in an array, I would prefer having this field for clarity in inspector at least 
            public CharacterPart Part;
            public SpriteRenderer[] MainRenderers;
            [HideInInspector]
            public List<Sprite> DefaultSprites;
        }

        //We're using code in Initialize instead of Awake, because Awake might not trigger unless we make a turn where VisualDirection would be visible
        //On the plus side - we can also have better control of when we want to load this logic (potentially evading peaks\spikes)
        public void Initialize()
        {
            //When we unequip an item, I would like to return sprites back to their initial state, which may differ between characters with different visuals
            foreach (var part in parts)
            {
                part.DefaultSprites = new List<Sprite>();
                foreach (var renderer in part.MainRenderers)
                {
                    part.DefaultSprites.Add(renderer.sprite);
                }
            }
        }

        public void ApplyCustomization(CharacterPart targetPart, Sprite[] appliedSprites)
        {
            foreach (var part in parts)
            {
                if (part.Part == targetPart)
                {
                    for (var i = 0; i < part.MainRenderers.Length; i++)
                    {
                        part.MainRenderers[i].sprite = (appliedSprites != null && appliedSprites.Length > 0 ? appliedSprites[i] : part.DefaultSprites[i]);
                    }
                }
            }
            //Here we could add detail system which would attach bonus GameObjects into hierarchy: those could contain sprites, FX, logic - anything
            //Although an exciting possibility, I hope I'll have time to implement it...
            //Spoiler alert: I won't :(
        }
    }
}