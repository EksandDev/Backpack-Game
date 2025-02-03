using BackpackGame.Core.Abstractions;

namespace BackpackGame.Backpack
{
    public class BackpackPresenter : Presenter<BackpackModel, BackpackView>
    {
        public BackpackPresenter(BackpackModel model, BackpackView view) : base(model, view) {}

        public override void Initialize()
        {
            View.PlayerWantsReleaseItem += GiveItemToViewFromModel;
            View.PlayerWantsStorageItem += GiveItemToModelFromView;
        }

        private void GiveItemToViewFromModel()
        {
            if (!IsEnabled)
                return;
            
            if (Model.TryTakeItem(out var item))
                View.ReleaseItem(item);
            
            View.ReleaseItemFailure();
        }
        
        private void GiveItemToModelFromView(Storageable item)
        {
            if (!IsEnabled)
                return;
            
            if (Model.TryStorageItem(item))
                View.StorageItem();
            
            View.StorageItemFailure();
        }
    }
}
