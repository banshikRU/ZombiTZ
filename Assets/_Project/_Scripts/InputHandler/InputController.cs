using WeaponControl;
using Zenject;

namespace InputControll
{
    public class InputController:ITickable
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

    }
}

