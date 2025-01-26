using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace UIControl.MVVM.HealthBar
{
    public class HealthBarViewModel: IDisposable
    {
        public readonly ReactiveProperty<float> ZombieHealth = new();
        public readonly ReactiveProperty<Vector3> ZombiePosition = new();

        private readonly HealthBarModel _healthBarModel;

        public HealthBarViewModel(HealthBarModel healthBarModel)
        {
            _healthBarModel = healthBarModel;
            
            Initialize();
        }

        public void Initialize()
        {
            OnHealthChanged(_healthBarModel.ZombieHealth.Value);
            _healthBarModel.ZombieHealth.Subscribe(OnHealthChanged);
            _healthBarModel.ZombiePosition.Subscribe(OnPositionChanged);
        }
        private void OnHealthChanged(float value)
        {
            ZombieHealth.Value = value;
        }

        private void OnPositionChanged(Vector3 position)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
            ZombiePosition.Value = new Vector3(screenPosition.x, screenPosition.y + 50, screenPosition.z); 
        }

        public void Dispose()
        {
            _healthBarModel.ZombieHealth.Dispose();
        }
    }
}