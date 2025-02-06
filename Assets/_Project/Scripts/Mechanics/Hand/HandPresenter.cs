using BackpackGame.Core.Abstractions;

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
            Model.PickUpItem(item);
            View.PickUpItem(item);
        }
    }
}
