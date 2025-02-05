using UnityEngine;

namespace BackpackGame.Core.Abstractions
{
    public abstract class View : MonoBehaviour
    {
        public bool IsEnabled { get; set; } = true;
    }
}