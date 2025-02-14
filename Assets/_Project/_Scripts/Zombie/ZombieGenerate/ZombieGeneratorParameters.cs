using UnityEngine;
using Zenject;
using GameStateControl;

namespace ZombieGeneratorBehaviour
{
    public class ZombieGeneratorParameters:ITickable, IInitializable
    {
        private readonly ZombieFactory _zombieFabric;
        private readonly GameStateUpdater _gameStateUpdater;

        private readonly float _timeToNewSpawnLevel;
        private readonly float _minimalTimeToSpawn ;
        private readonly float _reductionTime ;
        private float _baseTimeToSpawnNewZombie;
        private float _newTimeToNextSpawn;
        private float _timeToNewSpawn;

        private bool _isMinimalValueReached;

        public ZombieGeneratorParameters(GameStateUpdater gameStateUpdater,float timeToNewSpawnLevel, float minimalTimeToSpawn, float baseTimeToSpawnNewZombie, float reductionTime,ZombieFactory zombieFabric)
        {
            _gameStateUpdater = gameStateUpdater;
            _timeToNewSpawnLevel = timeToNewSpawnLevel;
            _minimalTimeToSpawn = minimalTimeToSpawn;
            _baseTimeToSpawnNewZombie = baseTimeToSpawnNewZombie;
            _reductionTime = reductionTime;
            _zombieFabric = zombieFabric;

        }
        
        public void Initialize()
        {
            _newTimeToNextSpawn = _baseTimeToSpawnNewZombie;
            _timeToNewSpawn = _timeToNewSpawnLevel;
            _isMinimalValueReached = false;
        }

        public void Tick()
        {
            if (_gameStateUpdater.IsGame)
            {
                _baseTimeToSpawnNewZombie -= Time.deltaTime;
                if (!_isMinimalValueReached)
                {
                    _timeToNewSpawn -= Time.deltaTime;
                }
                if (_baseTimeToSpawnNewZombie <= 0)
                {
                    _baseTimeToSpawnNewZombie = _newTimeToNextSpawn;
                    _zombieFabric.GenerateZombie(Utilities.GetInvisiblePoint());
                }
                if (_timeToNewSpawn <= 0 && !_isMinimalValueReached)
                {
                    _timeToNewSpawn = _timeToNewSpawnLevel;
                    _newTimeToNextSpawn -= _reductionTime;
                    if (_newTimeToNextSpawn <= _minimalTimeToSpawn)
                    { _isMinimalValueReached = true; }
                }
            }
        }
    }
}
