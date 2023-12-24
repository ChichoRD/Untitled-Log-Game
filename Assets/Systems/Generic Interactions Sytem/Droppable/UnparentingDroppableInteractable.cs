using InteractionSystem.Interactable;
using UnityEngine;

namespace GenericInteractions.Droppable
{
    internal class UnparentingDroppableInteractable : MonoBehaviour, IAgnosticInteractable
    {
        [SerializeField]
        private Transform _transform;

        public bool TryInteract()
        {
            _transform.SetParent(null);
            return true;
        }
    }
}
