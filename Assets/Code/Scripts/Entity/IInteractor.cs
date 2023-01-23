using System;

namespace Code.Scripts.Core
{
    public interface IInteractor
    {
        public void InteractWith(IInteractable interactable);
    }
}