using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Environment
{
    /// <summary>
    /// This class controls all strictly camera-related behaviour. It should be accessed from another classes.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform followTarget;
        // Although I'm not using cinemachine for the sake of simplicity, depending on a style of the game it may be worth considering
        static CameraController _instance;

        Vector3 _followOffset;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            } else
            {
                Debug.LogWarning($"There are multiple CameraControllers on the scene! Excessive component in {gameObject.name} will be destroyed.");
            }
        }

        void Update()
        {
            if (followTarget != null)
            {
                transform.position = followTarget.position + _followOffset;
            }
        }

        public static void SetFollowTarget(Transform newTarget, bool centerOnTarget = false)
        {
            _instance.followTarget = newTarget;
            //This can be written much nicer with a one-liner, but if there's more logic to be written later, branches are clearly defined and ready to go
            if (centerOnTarget)
            {
                _instance._followOffset = Vector3.zero;
            }
            else
            {
                _instance._followOffset = _instance.transform.position - newTarget.position;
            }
        }
    }
}