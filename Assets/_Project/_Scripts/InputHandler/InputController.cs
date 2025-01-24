using System;
using UnityEngine;
using WeaponControl;
using Zenject;

namespace InputControll
{
    public class InputController : ITickable,IDisposable
    {
        private readonly PlayerFireControl _playerFireControll;

        public IInput CurrentInput { get; private set; }

        public InputController(PlayerFireControl playerFireControll, IInput input)
        {
            CurrentInput = input;
            _playerFireControll = playerFireControll;
            
            SubcribeEvent();
        }

        public void Tick()
        {
            TakeInput();
        }
        
        public void UnsubcribeEvent()
        {
            CurrentInput.OnShoot -= _playerFireControll.Shot;
        }

        public void Dispose()
        {
            UnsubcribeEvent();
        }

        private void SubcribeEvent()
        {
            CurrentInput.OnShoot += _playerFireControll.Shot;
        }

        private void TakeInput()
        {
            CurrentInput?.TakeShoot();
        }
        
    }
}

