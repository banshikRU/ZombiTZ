using UnityEngine;
using WeaponControl;
using Zenject;

namespace InputControll
{
    public class InputController : ITickable
    {
        private readonly PlayerFireControl _playerFireControll;

        public IInput CurrentInput { get; private set; }

        public InputController(PlayerFireControl playerFireControll)
        {
            _playerFireControll = playerFireControll;
        }

        public void Tick()
        {
            TakeInput();
            CheckForAplicationQuit();
        }

        public void SetUpCurrentInput(IInput CurrentInput)
        {
            this.CurrentInput = CurrentInput;
            SubcribeEvent();
        }

        public void UnsubcribeEvent()
        {
            CurrentInput.OnShoot -= _playerFireControll.Shot;
        }

        private void SubcribeEvent()
        {
            CurrentInput.OnShoot += _playerFireControll.Shot;
        }

        private void TakeInput()
        {
            CurrentInput?.TakeShoot();
        }

        private void CheckForAplicationQuit()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }
    }
}

