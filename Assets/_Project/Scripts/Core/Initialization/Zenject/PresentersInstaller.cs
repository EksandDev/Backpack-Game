using BackpackGame.Backpack;
using BackpackGame.Hand;
using BackpackGame.Interactor;
using Zenject;

namespace BackpackGame.Core.Initialization
{
    public class PresentersInstaller : MonoInstaller
    {
        private BackpackPresenter _backpackPresenter;
        private InteractorPresenter _interactorPresenter;
        private HandPresenter _handPresenter;
        
        public override void InstallBindings()
        {
            Container.Bind<BackpackPresenter>().FromInstance(_backpackPresenter).AsSingle();
            Container.Bind<InteractorPresenter>().FromInstance(_interactorPresenter).AsSingle();
            Container.Bind<HandPresenter>().FromInstance(_handPresenter).AsSingle();
        }

        public void Initialize(Presenters presenters)
        {
            _backpackPresenter = presenters.BackpackPresenter;
            _interactorPresenter = presenters.InteractorPresenter;
            _handPresenter = presenters.HandPresenter;
        }
    }
}
