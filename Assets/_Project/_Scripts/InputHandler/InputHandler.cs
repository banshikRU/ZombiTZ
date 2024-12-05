using UnityEngine;

namespace InputControll
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField]
        private PlayerFireControll _playerFireControll;

        private bool isDesktop = true;
        private bool isInit;

        public IInput currentInput { get; private set; }

        private void Awake()
        {
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
 
