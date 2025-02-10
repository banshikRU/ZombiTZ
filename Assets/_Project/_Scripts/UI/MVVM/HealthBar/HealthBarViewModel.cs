using System;
using UniRx;
using UnityEngine;
using ZombieGeneratorBehaviour;

namespace UIControl.MVVM.HealthBar
{
    public class HealthBarViewModel: IDisposable
    {
        public readonly ReactiveProperty<float> ZombieHealth = new();
        public readonly ReactiveProperty<Vector3> ZombiePosition = new();
        
        private readonly ZombieBehaviour _zombie;

        public HealthBarViewModel(ZombieBehaviour zombie)
        {
            _zombie = zombie;
        }

        public void Initialize()
        {
            UpdateZombieHealthBar(1,1);
            SubscribeEvent();
        }
        
        public void Dispose()
        {
            _zombie.OnZombieTakeDamage -= UpdateZombieHealthBar;
        }

        public void UpdateZombieHealthBar(int value,int maxValue)
        {
            ZombieHealth.Value =(float)value/maxValue;
        }
        
        public void UpdateZombiePosition(Vector3 position)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
            ZombiePosition.Value = new Vector3(screenPosition.x, screenPosition.y + 50, screenPosition.z); 
        }
        
        private void SubscribeEvent()
        {
            UpdateZombieHealthBar(1,1);
            _zombie.OnZombieTakeDamage += UpdateZombieHealthBar;
        }
    }
}