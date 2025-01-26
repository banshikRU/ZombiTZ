using System;
using UniRx;
using UnityEngine;
using Zenject;
using ZombieGeneratorBehaviour;

namespace UIControl.MVVM.HealthBar
{
    public class HealthBarModel : IDisposable
    {
        private readonly ZombieBehaviour _zombieBehaviour;
        
        public readonly ReactiveProperty<float> ZombieHealth = new();
        public readonly ReactiveProperty<Vector3> ZombiePosition = new();

        public HealthBarModel(ZombieBehaviour zombieBehaviour)
        {
            _zombieBehaviour = zombieBehaviour;
            
            Initialize();
        }
        
        public void Initialize()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            UpdateZombieHealthBar(1,1);
            _zombieBehaviour.OnZombieTakeDamage += UpdateZombieHealthBar;
        }

        public void UpdateZombieHealthBar(int value,int maxValue)
        {
            ZombieHealth.Value =(float)value/maxValue;
        }

        public void UpdateZombiePosition(Vector3 position)
        {
            ZombiePosition.Value = position;
        }

        public void Dispose()
        {
            _zombieBehaviour.OnZombieTakeDamage -= UpdateZombieHealthBar;
        }
    }
}