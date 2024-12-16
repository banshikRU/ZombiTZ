using WeaponControl;

namespace InputControll
{
    public class InputController
    {
        private PlayerFireControl _playerFireControll;

        public IInput CurrentInput { get; private set; }

        public InputController(PlayerFireControl playerFireControll)
        {
            _playerFireControll = playerFireControll;
        }

        public void Update()
        {
            TakeInput();
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
            CurrentInput.TakeShoot();
        }

    }
}

