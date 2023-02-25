using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Characters.CharacterComponents
{
    /// <summary>
    /// This class processes input from the local player and applies it to the controlledCharacter.
    /// Although it implies multiplayer, I don't think this prototype will contain it.
    /// We're also using old input system as I'm more familiar with it and trying to avoid time constraints - for a full implementation it is worth reconsidering
    /// </summary>
    public class CharacterLocalControl : MonoBehaviour
    {
        [SerializeField] Character controlledCharacter;
        static CharacterLocalControl _instance;

        private void Awake()
        {
            //We don't want more than 1 CharacterLocalControl on the scene; if we need to control multiple charcters, we can always expand controlledCharacter to a list or an array
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Debug.LogWarning($"There are multiple CharacterLocalControls on the scene! Excessive component in {gameObject.name} will be destroyed.");
                //Instead of destruction, we can rename GameObject to something arbitrary, so that it would be easy to find in Hierarchy and get rid of.
                //I do not foresee such issues, though
                Destroy(this);
            }
        }


        private void Update()
        {
            if (controlledCharacter != null)
            {
                Vector2 movementAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                if (movementAxis != Vector2.zero)
                {
                    OrderMovement(movementAxis);
                }

                //This is a bit of a hack, but as I'm running out of time I decided to go for it: Player-specific controls
                if (Input.GetKeyDown(KeyCode.E) && controlledCharacter is Player player)
                {
                    player.OpenInventory();
                }
            }
        }

        private void OrderMovement(Vector2 movementAxis)
        {
            if (controlledCharacter != null)
            {
                controlledCharacter.Move(movementAxis);
            }
        }


        public void AssignCharacter(Character newCharacter)
        {
            controlledCharacter = newCharacter;
        }
    }
}