using BackpackGame.Backpack;
using BackpackGame.Core.Abstractions;
using BackpackGame.Hand;
using Zenject;

namespace BackpackGame.Core.Initialization
{
    public class BackpackInitializer : MVPInitializer<BackpackView>
    {
        private BackpackPresenter _presenter;
        
        public BackpackInitializer(BackpackView view, HandPresenter handPresenter, int itemsLimit) 
            : base(view)
        {
            BackpackModel model = new(handPresenter.Model, itemsLimit);
            _presenter = new(model, view, handPresenter);
        }
        
        public override void Initialize()
        {
            _presenter.Initialize();
        }
    }
}
