using BackpackGame.Backpack;
using BackpackGame.Core.Abstractions;

namespace BackpackGame.Core.Initialization
{
    public class BackpackInitializer : MVPInitializer<BackpackView>
    {
        private BackpackPresenter _presenter;
        
        public BackpackInitializer(BackpackView view, int itemsLimit) : base(view)
        {
            BackpackModel model = new(itemsLimit);
            _presenter = new(model, view);
        }

        public override void Initialize()
        {
            _presenter.Initialize();
        }
    }
}
