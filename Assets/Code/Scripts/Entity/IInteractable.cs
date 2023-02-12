namespace Code.Scripts.Core
{
    public interface IInteractable
    {
        public void Accept(IInteractor interactor);
    }
}