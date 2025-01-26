using System;

namespace InputControl
{
    public interface IInput
    {
        public event Action OnShoot;

        public void TakeShoot();
    }
}

