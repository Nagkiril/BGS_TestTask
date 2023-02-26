using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.UI;
using Prototype.Interactables;

namespace Prototype.Characters
{
    /// <summary>
    /// Shopkeeper is supposed to be initiating dialogue with player, but due to time constraints, I decided to simply make him open a window and warp\rotate the player.
    /// It is debatable whether or not Shopkeeper himself should have that logic.
    /// </summary>
    public class Shopkeeper : Character
    {
        [SerializeField] InteractableZone shopDialogueZone;

        new void Start()
        {
            base.Start();
            visuals.FaceDirection(Vector2.left);
            shopDialogueZone.OnInteractionPrompted += OnInteractionPrompted;
        }

        void OnInteractionPrompted(Interactor actor)
        {
            var playerActor = actor.Owner.GetComponent<Player>();
            if (playerActor != null && playerActor.IsCharacterMovable)
            {
                if (ShopOverlay.TryShowOverlay(playerActor))
                {
                    playerActor.Bind();
                    playerActor.SyncPosition(shopDialogueZone.transform.position, 0.7f);
                    playerActor.RotateTo(Vector2.right);
                }
            }
        }


    }
}