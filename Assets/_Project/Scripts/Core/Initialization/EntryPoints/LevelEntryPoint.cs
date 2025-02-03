using BackpackGame.Backpack;
using UnityEngine;

namespace BackpackGame.Core.Initialization
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [SerializeField] private BackpackView _backpackView;
        [SerializeField] private int _backpackItemsLimit = 5;
        
        private void Awake()
        {
            BackpackInitializer backpackInitializer = new(_backpackView, _backpackItemsLimit);
            
            backpackInitializer.Initialize();
        }
    }
}
