using BackpackGame.Core.Abstractions;
using BackpackGame.Hand;
using BackpackGame.ScriptableObjects;
using UnityEngine;

namespace BackpackGame.Backpack
{
    public class BackpackPresenter : Presenter<BackpackModel, BackpackView>
    {
        private HandPresenter _handPresenter;

        public BackpackPresenter(BackpackModel model, BackpackView view, HandPresenter handPresenter)
            : base(model, view)
        {
            _handPresenter = handPresenter;
        }

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
            {
                View.TakeItem(itemData);
                _handPresenter.SpawnItemInHand(itemData.Prefab);
            }
            
            View.TakeItemFailure();
        }
        
        private void SendItemToModelFromView()
        {
            if (!IsEnabled)
                return;

            if (Model.TryStorageItem())
            {
                View.StorageItem();
                _handPresenter.DestroyItemInHand();
            }
            
            View.StorageItemFailure();
        }
    }
}
