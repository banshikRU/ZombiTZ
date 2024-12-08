using System;

namespace InputControll
{
    public interface IInput
    {
        public event Action OnShoot;

        public void TakeShoot();
    }
}

