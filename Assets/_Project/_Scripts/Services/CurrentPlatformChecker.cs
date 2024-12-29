using InputControll;
using UnityEngine;

namespace Services
{
    public class CurrentPlatformChecker
    {
        private readonly InputController _inputController;

        private string _platformName = "Desktop"; // �������� ���������� ������������ ���������

        public CurrentPlatformChecker(InputController inputController)
        {
            _inputController = inputController;
            _inputController.SetUpCurrentInput(SetUpCurrentPlatform(_platformName));
        }

        private IInput SetUpCurrentPlatform(string platformName)
        {
            switch (platformName)
            {
                case "Desktop":
                    return new DesktopInput();
                default:
                    return new DesktopInput();
            }
        }

    }

}
