using BackpackGame.Core.Abstractions;
using BackpackGame.ScriptableObjects;

namespace BackpackGame.Backpack
{
    public class BackpackPresenter : Presenter<BackpackModel, BackpackView>
    {
        public BackpackPresenter(BackpackModel model, BackpackView view) : base(model, view) {}

        public override void Initialize()
        {
            View.PlayerWantsTakeItem += SendItemToViewFromModel;
            View.PlayerWantsStorageItem += SendItemToModelFromView;
        }

        private void SendItemToViewFromModel()
        {
            if (!IsEnabled)
                return;
            
            if (Model.TryTakeItem(out var item))
                View.TakeItem(item);
            
            View.TakeItemFailure();
        }
        
        private void SendItemToModelFromView(StorageableItemData item)
        {
            if (!IsEnabled)
                return;
            
            if (Model.TryStorageItem(item))
                View.StorageItem();
            
            View.StorageItemFailure();
        }
    }
}
