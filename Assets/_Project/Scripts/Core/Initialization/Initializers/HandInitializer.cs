using BackpackGame.Core.Abstractions;
using BackpackGame.Hand;

namespace BackpackGame.Core.Initialization
{
    public class HandInitializer : MVPInitializer<HandView>
    {
        private HandPresenter _presenter;
        
        public HandInitializer(HandView view) : base(view)
        {
            HandModel model = new(view.HandPoint);
            _presenter = new(model, view);
        }

        public override void Initialize()
        {
            _presenter.Initialize();
        }
    }
}