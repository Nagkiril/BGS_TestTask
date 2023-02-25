using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Characters;
using TMPro;
using DG.Tweening;
using Prototype.UI.Count;

namespace Prototype.UI
{
    /// <summary>
    /// Just a basic counter that animates counter text using DOTween to be a bit nicer
    /// </summary>
    public class CoinCounter : MonoBehaviour
    {
        [SerializeField] TextCounter coinCounter;
        [SerializeField] float updateDuration;

        // Start is called before the first frame update
        void Awake()
        {
            Player.OnCoinsChanged += OnCoinsChanged;
        }

        private void OnDestroy()
        {
            Player.OnCoinsChanged -= OnCoinsChanged;
        }

        void OnCoinsChanged()
        {
            StartTextUpdate();
        }

        void StartTextUpdate()
        {
            coinCounter.CountTo(Player.Coins, updateDuration);
        }
    }
}