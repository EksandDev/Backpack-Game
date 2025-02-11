using UnityEngine;

namespace BackpackGame.Core.Abstractions
{
    public abstract class InteractorSubSystem
    {
        public bool IsEnabled { get; set; }
        
        public abstract void Interact(Collider hitCollider);
    }
}