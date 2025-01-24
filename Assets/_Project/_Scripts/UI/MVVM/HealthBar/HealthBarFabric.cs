using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using ZombieGeneratorBehaviour;
using Object = UnityEngine.Object;

namespace UIControl.MVVM.HealthBar
{
    public class HealthBarFabric : ITickable,IDisposable
    {
        private ZombieFactory _zombieFactory;
        private HealthBarView _healthBarViewPrefab;
        private GameObject _canvas;
        
        private Dictionary<ZombieBehaviour,HealthBarModel> _healthBarModels = new Dictionary<ZombieBehaviour, HealthBarModel>();
        private Dictionary<ZombieBehaviour,HealthBarView> _healthBarViews = new Dictionary<ZombieBehaviour, HealthBarView>();
        
        public HealthBarFabric(HealthBarView healthBarViewPrefab, GameObject canvas, ZombieFactory zombieFactory)
        {
            _healthBarViewPrefab = healthBarViewPrefab;
            _canvas = canvas;
            _zombieFactory = zombieFactory;
            
            SubscribeEvent();
        }
        
        public void Tick()
        {
            UpdateZombiesPosition();
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
            if (_healthBarModels.ContainsKey(zombieBehaviour))
            {
                _healthBarViews.TryGetValue(zombieBehaviour, out HealthBarView healthBarView);
                _healthBarModels.TryGetValue(zombieBehaviour, out HealthBarModel healthBarModel);
                healthBarModel.UpdateZombieHealthBar(1,1);
                healthBarView.gameObject.SetActive(true);
            }
            else
            {
                HealthBarModel healthBarModel = new HealthBarModel(zombieBehaviour);
                HealthBarViewModel healthBarViewModel = new HealthBarViewModel(healthBarModel);
                HealthBarView healthBar = Object.Instantiate(_healthBarViewPrefab, _canvas.transform);
                _healthBarModels.Add(zombieBehaviour, healthBarModel);
                _healthBarViews.Add(zombieBehaviour, healthBar);
                healthBar.Init(healthBarViewModel);
            }
        }
        
        private void UpdateZombiesPosition()
        {
            foreach (var zombieBehaviour in _healthBarModels.Keys)
            {
                _healthBarModels.TryGetValue(zombieBehaviour, out HealthBarModel healthBarModel);
                healthBarModel.UpdateZombiePosition(zombieBehaviour.transform.position);
            }
        }

        public void Dispose()
        {
            _zombieFactory.OnZombieDestroyed -= DisableHealBar;
            _zombieFactory.OnZombieSpawned -= GenerateHealthBar;
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