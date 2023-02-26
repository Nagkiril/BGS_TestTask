using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Prototype.Interactables
{
    /// <summary>
    /// This class is a supposed receiver of interaction between two objects.
    /// It is implied that it will be used together with another component that'll tell it what are conditions and actions of the interaction.
    /// I think with some ingenuity and UnityEvents it could be used to allow designers create their own interactions, to some extent like visul scripting (Blueprints in UE, etc)
    /// </summary>
    public class InteractableZone : MonoBehaviour
    {
        public event Action<Interactor> OnInteractionPrompted;

        public void StartInteraction(Interactor interactor)
        {
            Debug.Log("Receive");
            OnInteractionPrompted?.Invoke(interactor);
        }
    }
}