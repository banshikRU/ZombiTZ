using System;
using WeaponControl;
using Zenject;

namespace InputControl
{
    public class InputController : ITickable,IDisposable,IInitializable
    {
        private readonly PlayerFireControl _playerFireControl;
        private readonly IInput _currentInput;

        public InputController(PlayerFireControl playerFireControl, IInput input)
        {
            _currentInput = input;
            _playerFireControl = playerFireControl;
        }
        
        public void Initialize()
        {
            SubscribeEvent();
        }

        public void Tick()
        {
            TakeInput();
        }
        
        public void Dispose()
        {
            UnsubscribeEvent();
        }
        
        private void SubscribeEvent()
        {
            _currentInput.OnShoot += _playerFireControl.Shot;
        }
        
        private void UnsubscribeEvent()
        {
            _currentInput.OnShoot -= _playerFireControl.Shot;
        }

        private void TakeInput()
        {
            _currentInput?.TakeShoot();
        }
    }
}

