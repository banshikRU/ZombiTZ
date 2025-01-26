using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UIControl.MVVM.HealthBar
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField]
        private Image _healthBar;

        private HealthBarViewModel _healthBarViewModel;

        public void Init(HealthBarViewModel healthBarViewModel)
        {
            _healthBarViewModel = healthBarViewModel;
            SubscribeEvent();
        }
        
        private void SubscribeEvent()
        {
            _healthBarViewModel.ZombieHealth.Subscribe(HealthBarValueUpdate);
            _healthBarViewModel.ZombiePosition.Subscribe(UpdateHealthBarPosition);
        }

        private void OnDestroy()
        {
            _healthBarViewModel.ZombieHealth.Dispose();
            _healthBarViewModel.ZombiePosition.Dispose();
        }

        private void HealthBarValueUpdate(float value)
        {
            _healthBar.fillAmount = value;
        }

        private void UpdateHealthBarPosition(Vector3 position)
        {
            gameObject.transform.position = position;
        }

    }
}