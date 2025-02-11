using BackpackGame.Backpack;
using BackpackGame.Core.Abstractions;
using BackpackGame.Hand;
using Zenject;

namespace BackpackGame.Core.Initialization
{
    public class BackpackInitializer : MVPInitializer<BackpackView>
    {
        public BackpackPresenter Presenter { get; private set; }
        
        public BackpackInitializer(BackpackView view, HandPresenter handPresenter, int itemsLimit) 
            : base(view)
        {
            BackpackModel model = new(handPresenter.Model, itemsLimit);
            Presenter = new(model, view, handPresenter);
        }
        
        public override void Initialize()
        {
            Presenter.Initialize();
        }
    }
}
