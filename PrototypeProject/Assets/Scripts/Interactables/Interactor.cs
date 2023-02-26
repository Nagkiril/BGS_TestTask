using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Interactables
{
    /// <summary>
    /// This is an interactable actor that can begin an interaction.
    /// It is possible to expand this class so we can have external logic decide which InteractableZones can be activated.
    /// Even if general case it may be expensive (due to GetComponent checks).
    /// </summary>
    public class Interactor : MonoBehaviour
    {
        [field: SerializeField] public GameObject Owner { get; private set; }
        List<InteractableZone> _activeZones;

        // Start is called before the first frame update
        void Awake()
        {
            _activeZones = new List<InteractableZone>();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            var newZone = other.attachedRigidbody.GetComponent<InteractableZone>();
            if (newZone != null && !_activeZones.Contains(newZone))
                _activeZones.Add(newZone);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var departinZone = other.attachedRigidbody.GetComponent<InteractableZone>();
            if (departinZone != null)
                _activeZones.Remove(departinZone);
        }


        //We could totally make some more interesting logic here, at least make it proximity-based, rather than just 1st item in the list...
        public void SendInteraction()
        {
            Debug.Log(_activeZones.Count);
            if (_activeZones.Count > 0)
            {
                _activeZones[0].StartInteraction(this);
            }
        }
    }
}