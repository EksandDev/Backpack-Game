namespace BackpackGame.Core.Abstractions
{
    public interface IHaveDescription : IInteractable
    {
        public string Description { get; }
    }
}