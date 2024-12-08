using UnityEngine;
using WeaponControl;

namespace InputControll
{
    public class InputHandler : MonoBehaviour
    {
        private PlayerFireControll _playerFireControll;

        private bool isDesktop = true;
        private bool isInit;

        public IInput currentInput { get; private set; }

        public void Init(PlayerFireControll playerFireControll)
        {
            _playerFireControll = playerFireControll;
            CheckPlatform();
        }

        private void Update()
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
                isInit = true;
            }
        }

        private void TakeInput()
        {
            currentInput.TakeShoot();
        }

    }
}
 
