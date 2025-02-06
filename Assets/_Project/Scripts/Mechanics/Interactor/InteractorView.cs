using System;
using BackpackGame.Core.Abstractions;
using UnityEngine;

namespace BackpackGame.Interactor
{
    public class InteractorView : View
    {
        [SerializeField] private Transform _originRaycastPoint;
        [SerializeField] private float _raycastMaxDistance = 5f;

        public event Action<Collider> RaycastReachedObject;
        
        private void Update()
        {
            RaycastHit raycastHit;

            if (!Physics.Raycast(_originRaycastPoint.position, _originRaycastPoint.forward,
                    out raycastHit, _raycastMaxDistance))
            {
                //null на результат рейкаста поменять
                RaycastReachedObject?.Invoke(null);
                return;
            }
            
            if (raycastHit.collider.TryGetComponent(out IInteractable interactableObject))
                RaycastReachedObject?.Invoke(raycastHit.collider);
        }
    }
}