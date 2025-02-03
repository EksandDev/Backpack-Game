namespace BackpackGame.Core.Abstractions
{
    public abstract class Presenter<TModel, TView> where TModel : Model where TView : View
    {
        protected Presenter(TModel model, TView view)
        {
            Model = model;
            View = view;
        }

        public TModel Model { get; private set; }
        public TView View { get; private set; }
        public bool IsEnabled { get; set; } = true;
        
        public abstract void Initialize();
    }
}
