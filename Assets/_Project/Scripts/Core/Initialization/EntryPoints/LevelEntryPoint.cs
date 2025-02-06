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
        
        private void Awake()
        {
            BackpackInitializer backpackInitializer = new(_backpackView, _backpackItemsLimit);
            HandInitializer handInitializer = new(_handView);
            InteractorInitializer interactorInitializer = new(_interactorView, handInitializer.Presenter);
            
            _sceneContext.Run();
            
            backpackInitializer.Initialize();
            interactorInitializer.Initialize();
            handInitializer.Initialize();
        }
    }
}
