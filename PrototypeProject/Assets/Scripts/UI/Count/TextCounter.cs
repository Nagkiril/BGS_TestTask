using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Prototype.UI.Count
{
    /// <summary>
    /// Another script I've made for a passion project of mine. Just like the other - should put it outside specific project's namespace if I'll make it redistributable.
    /// I don't expect it to be very performant, but it served me alright on mobile, PC should be well too.
    /// </summary>
    public class TextCounter : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI animatedText;

        public event Action OnAnimationDone;

        Tweener _count;

        int _currentCount;

        public void CountTo(int valueTo, float duration)
        {
            if (_count != null)
            {
                _count.Kill();
            }
            _count = DOTween.To(() => _currentCount, x => _currentCount = x, valueTo, duration);
            _count.OnUpdate(() => animatedText.text = _currentCount.ToString() );
            _count.OnComplete(() => { animatedText.text = valueTo.ToString(); OnAnimationDone?.Invoke(); });
        }



    }
}