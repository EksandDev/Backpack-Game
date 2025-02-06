using BackpackGame.Backpack;
using BackpackGame.Core.Abstractions;
using Zenject;

namespace BackpackGame.Core.Initialization
{
    public class BackpackInitializer : MVPInitializer<BackpackView>
    {
        public BackpackPresenter Presenter { get; }
        
        public BackpackInitializer(BackpackView view, int itemsLimit) : base(view)
        {
            BackpackModel model = new(itemsLimit);
            Presenter = new(model, view);
        }
        
        public override void Initialize()
        {
            Presenter.Initialize();
        }
    }
}
