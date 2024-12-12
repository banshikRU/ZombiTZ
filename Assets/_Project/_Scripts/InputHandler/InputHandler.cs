using UnityEngine;
using WeaponControl;

namespace InputControll
{
    public class InputController
    {
        private PlayerFireControll _playerFireControll;

        private bool isDesktop = true;
        private bool isInit;

        public IInput currentInput { get; private set; }

        public InputController(PlayerFireControll playerFireControll)
        {
            _playerFireControll = playerFireControll;
            CheckPlatform();
        }
  
        public void Update()
        {
            if (!isInit)
                return;
            TakeInput();
        }

        private void CheckPlatform()
        {
            if (isDesktop )
            {
                currentInput = new DesktopInput();
                currentInput.OnShoot += _playerFireControll.Shot;
                isInit = true;
            }
        }

        private void TakeInput()
        {
            currentInput.TakeShoot();
        }

    }
}
 
