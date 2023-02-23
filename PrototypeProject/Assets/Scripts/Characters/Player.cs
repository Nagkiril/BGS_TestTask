using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Prototype.Characters.CharacterComponents
{
    /// <summary>
    /// Player acts like a Character that is expected to be controlled by a human.
    /// I should note that it should be possible for us to control a non-Player Character, that may be useful for debugging or rare gameplay purposes (minigames, etc).
    /// </summary>
    public sealed class Player : Character
    {
        //If our Inspector window will grow too large, we can introduce a private class to act as a category we can open\collapse on demand
        [Header("Player Pramaters")]
        //We could also write it off with one-liner "[field: SerializeField] public bool IsLocalPlayer { get; private set; }", but then we can't apply Header directly above
        [SerializeField] bool isLocalPlayer;
        //We're not using an explicit method, because this operation is expected to be trivial throughout development
        public bool IsLocalPlayer { get => isLocalPlayer; } 

       

        new void Start()
        {
            base.Start();
        }



    }
}