using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZombieGeneratorBehaviour;
using Object = UnityEngine.Object;

namespace UIControl.MVVM.HealthBar
{
    public class HealthBarFabric : ITickable,IDisposable, IInitializable
    {
        private readonly ZombieFactory _zombieFactory;
        private readonly HealthBarView _healthBarViewPrefab;
        private readonly GameObject _canvas;
        
        private readonly Dictionary<ZombieBehaviour,HealthBarViewModel> _healthBarViewModels = new ();
        private readonly Dictionary<ZombieBehaviour,HealthBarView> _healthBarViews = new ();
        
        public HealthBarFabric(HealthBarView healthBarViewPrefab, GameObject canvas, ZombieFactory zombieFactory)
        {
            _healthBarViewPrefab = healthBarViewPrefab;
            _canvas = canvas;
            _zombieFactory = zombieFactory;
        }
        
        public void Initialize()
        {
            SubscribeEvent();
        }
        
        public void Tick()
        {
            UpdateZombiesPosition();
        }
        
        public void Dispose()
        {
            UnsubscribeEvent();
        }

        private void UnsubscribeEvent()
        {
            _zombieFactory.OnZombieDestroyed -= DisableHealBar;
            _zombieFactory.OnZombieSpawned -= GenerateHealthBar;
        }
        
        private void SubscribeEvent()
        {
            _zombieFactory.OnZombieDestroyed += DisableHealBar;
            _zombieFactory.OnZombieSpawned += GenerateHealthBar;
        }

        private void GenerateHealthBar(ZombieBehaviour zombieBehaviour)
        {
            CheckForGeneratedZombies(zombieBehaviour);
        }

        private void CheckForGeneratedZombies(ZombieBehaviour zombieBehaviour)
        {
            if (_healthBarViewModels.ContainsKey(zombieBehaviour))
            {
                _healthBarViews.TryGetValue(zombieBehaviour, out HealthBarView healthBarView);
                _healthBarViewModels.TryGetValue(zombieBehaviour, out HealthBarViewModel healthBarModel);
                healthBarModel?.UpdateZombieHealthBar(1,1);
                healthBarView?.gameObject.SetActive(true);
            }
            else
            {
                HealthBarViewModel healthBarViewModel = new HealthBarViewModel(zombieBehaviour);
                healthBarViewModel.Initialize(); 
                HealthBarView healthBar = Object.Instantiate(_healthBarViewPrefab, _canvas.transform);
                _healthBarViewModels.Add(zombieBehaviour, healthBarViewModel);
                _healthBarViews.Add(zombieBehaviour, healthBar);
                healthBar.Init(healthBarViewModel);
            }
        }
        
        private void UpdateZombiesPosition()
        {
            foreach (var zombieBehaviour in _healthBarViewModels.Keys)
            {
                _healthBarViewModels.TryGetValue(zombieBehaviour, out HealthBarViewModel healthBarViewModel);
                healthBarViewModel?.UpdateZombiePosition(zombieBehaviour.transform.position);
            }
        }
        
        private void DisableHealBar(ZombieBehaviour zombieBehaviour)
        {
            if (_healthBarViews.TryGetValue(zombieBehaviour, out HealthBarView healthBarView))
            {
                healthBarView.gameObject.SetActive(false);
            }
        }
    }
}