using BackpackGame.Core.Abstractions;
using UnityEngine;

namespace BackpackGame.Hand
{
    public class HandPresenter : Presenter<HandModel, HandView>
    {
        public HandPresenter(HandModel model, HandView view) : base(model, view) {}

        public override void Initialize()
        {
            View.PlayerWantsDropItem += Model.DropItem;
            View.PlayerWantsUseItem += Model.UseItem;
        }

        public void PickUpItem(StorageableItem item)
        {
            if (Model.CurrentItem)
                return;
            
            Model.PickUpItem(item);
            View.PickUpItem(item);
        }
        
        public void SpawnItemInHand(StorageableItem prefab)
        {
            if (Model.CurrentItem)
                return;
            
            var item = View.SpawnItem(prefab);
            Model.PickUpItem(item);
        }

        public void DestroyItemInHand()
        {
            if (!Model.CurrentItem)
                return;

            var item = Model.CurrentItem;
            Model.DestroyCurrentItem();
            View.DestroyItem(item);
        }
    }
}
