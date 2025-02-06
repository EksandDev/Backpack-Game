using BackpackGame.Backpack;
using BackpackGame.Hand;
using BackpackGame.Interactor;

namespace BackpackGame.Core.Initialization
{
    public struct Presenters
    {
        public Presenters
        (
            BackpackPresenter backpackPresenter,
            InteractorPresenter interactorPresenter,
            HandPresenter handPresenter
        )
        {
            BackpackPresenter = backpackPresenter;
            InteractorPresenter = interactorPresenter;
            HandPresenter = handPresenter;
        }
        
        public BackpackPresenter BackpackPresenter { get; }
        public InteractorPresenter InteractorPresenter { get; }
        public HandPresenter HandPresenter { get; }
    }
}