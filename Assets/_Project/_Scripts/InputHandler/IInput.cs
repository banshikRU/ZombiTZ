using System;
using Zenject;

namespace InputControl
{
    public interface IInput :ITickable
    {
        public event Action OnShoot;

        public void TakeShoot();
        
    }
}

