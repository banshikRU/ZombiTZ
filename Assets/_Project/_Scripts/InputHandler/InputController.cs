using WeaponControl;

namespace InputControll
{
    public class InputController
    {
        private PlayerFireControll _playerFireControll;

        private bool _isDesktop = true;
        private bool _isInit;

        public IInput CurrentInput { get; private set; }

        public InputController(PlayerFireControll playerFireControll)
        {
            _playerFireControll = playerFireControll;
            CheckPlatform();
        }
  
        public void Update()
        {
            if (!_isInit)
                return;
            TakeInput();
        }

        private void CheckPlatform()
        {
            if (_isDesktop )
            {
                CurrentInput = new DesktopInput();
                CurrentInput.OnShoot += _playerFireControll.Shot;
                _isInit = true;
            }
        }

        private void TakeInput()
        {
            CurrentInput.TakeShoot();
        }

    }
}
 
