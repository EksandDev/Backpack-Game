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
            
            if (Model.TryTakeItem(out var itemData))
                View.TakeItem(itemData);
            
            View.TakeItemFailure();
        }
        
        private void SendItemToModelFromView(StorageableItemData itemData)
        {
            if (!IsEnabled)
                return;
            
            if (Model.TryStorageItem(itemData))
                View.StorageItem();
            
            View.StorageItemFailure();
        }
    }
}
