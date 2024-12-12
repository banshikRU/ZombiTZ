using System;
using UnityEngine;

namespace InputControll
{
    public class DesktopInput : IInput
    {
        public event Action OnShoot;

        public void TakeShoot()
        {
            if (Input.GetMouseButton(0))
            {
                OnShoot?.Invoke();
            }
        }
    }
}

