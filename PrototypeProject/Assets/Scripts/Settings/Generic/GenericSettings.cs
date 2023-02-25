using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Settings.Generic
{
    /// <summary>
    /// This is a script I have made for a passion project of mine, meant to simplify usage of scriptable objects as containers of permanent data.
    /// There's some sense in putting it outside specific projects if they're meant to be reusable - if I stick with it, I'll do it.
    /// </summary>
    public abstract class GenericSettings<T> : ScriptableObject
    {
        private static GenericSettings<T> _loadedInstance;

        protected static GenericSettings<T> GetInstance(string loadPath)
        {
            if (_loadedInstance == null)
                _loadedInstance = Resources.Load<GenericSettings<T>>(loadPath);
            return _loadedInstance;
        }
    }
}