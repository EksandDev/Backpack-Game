namespace BackpackGame.Core.Abstractions
{
    public abstract class MVPInitializer<TView> where TView : View
    {
        protected MVPInitializer(TView view) {}
        
        public abstract void Initialize();
    }
}
