namespace BackpackGame.Core.Abstractions
{
    public abstract class Presenter<TModel, TView> where TModel : Model where TView : View
    {
        protected Presenter(TModel model, TView view)
        {
            Model = model;
            View = view;
        }

        public TModel Model { get; }
        public TView View { get; }
        public bool IsEnabled { get; set; } = true;
        
        public abstract void Initialize();

        public void SetIsEnabledForSystem(bool value)
        {
            IsEnabled = value;
            Model.IsEnabled = value;
            View.IsEnabled = value;
        }
    }
}
