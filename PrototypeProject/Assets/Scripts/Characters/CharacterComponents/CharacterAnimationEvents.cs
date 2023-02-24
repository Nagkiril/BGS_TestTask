using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Characters.CharacterComponents
{
    /// <summary>
    /// This component is to be attached next to an Animator, and within animation clips, we can refer to it for animation events.
    /// I have a preference for middle-of-the-road approach where we don't reference main script methods for animation events directly, but also maintain useful event names.
    /// If we'd have relatively complex UI animations and behaviour, this script could be extrapolated into general animation event script - not just character.
    /// </summary>
    public class CharacterAnimationEvents : MonoBehaviour
    {
        //I'm a lot more familiar with Actions over UnityEvents, and I prefer to use the former due to how easy on the eyes they are
        //However, they're not without drawbacks and this preference may evolve later on as I get more familiar with UnityEvents
        //Plus in case we're expecting non-programmers to modify event behaviour, I think UnityEvents would be an excellent choice
        public event Action OnAnimationStart;
        public event Action OnAnimationEnd;
        //More actions could be added if we'll need more event slots, based on context of required events (such as "OnAnimationCycleResolved", et cetera)

        public void AnimationStartEvent()
        {
            OnAnimationStart?.Invoke();
        }

        public void AnimationEndEvent()
        {
            OnAnimationEnd?.Invoke();
        }
    }
}