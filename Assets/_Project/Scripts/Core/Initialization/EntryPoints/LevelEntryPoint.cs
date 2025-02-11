using BackpackGame.Backpack;
using BackpackGame.Hand;
using BackpackGame.Interactor;
using UnityEngine;
using Zenject;

namespace BackpackGame.Core.Initialization
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [SerializeField] private SceneContext _sceneContext;
        [SerializeField] private BackpackView _backpackView;
        [SerializeField] private InteractorView _interactorView;
        [SerializeField] private HandView _handView;
        [SerializeField] private int _backpackItemsLimit = 5;
        [SerializeField] private BackpackSlot[] _backpackSlots;
        
        private void Start()
        {
            HandInitializer handInitializer = new(_handView);
            BackpackInitializer backpackInitializer = new(_backpackView, handInitializer.Presenter, _backpackItemsLimit);
            InteractorInitializer interactorInitializer = new(_interactorView, handInitializer.Presenter);
            BackpackSlotsProvider backpackSlotsProvider = new(_backpackSlots, backpackInitializer.Presenter.Model);
            
            _sceneContext.Run();
            
            backpackInitializer.Initialize();
            interactorInitializer.Initialize();
            handInitializer.Initialize();
            backpackSlotsProvider.Initialize();
        }
    }
}
