using System;
using BackpackGame.Core.Abstractions;
using UnityEngine;

namespace BackpackGame.Interactor
{
    public class InteractorView : View
    {
        [SerializeField] private Transform _originRaycastPoint;
        [SerializeField] private float _raycastMaxDistance = 2f;

        public event Action<Collider> RaycastReachedObject;
        
        private void Update()
        {
            RaycastHit raycastHit;

            if (!Physics.Raycast(_originRaycastPoint.position, _originRaycastPoint.forward,
                    out raycastHit, _raycastMaxDistance))
            {
                RaycastReachedObject?.Invoke(null);
                return;
            }
            
            if (raycastHit.collider.TryGetComponent(out IInteractable interactableObject))
                RaycastReachedObject?.Invoke(raycastHit.collider);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_originRaycastPoint.position, _originRaycastPoint.forward * _raycastMaxDistance);
        }
    }
}