using UnityEngine;

namespace ZombieGeneratorBehaviour
{
    public class ZombieGeneratorParameters 
    {
        private ZombieFactory _zombieFabric;

        private float _timeToNewSpawnLevel;
        private float _minimalTimeToSpawn ;
        private float _baseTimeToSpawnNewZombie;
        private float _reductionTime ;
        private float _newTimeToNextSpawn;
        private float _timeToNewSpawn;

        private bool _isMinimalValueReached;

        public ZombieGeneratorParameters(float timeToNewSpawnLevel, float minimalTimeToSpawn, float baseTimeToSpawnNewZombie, float reductionTime,ZombieFactory zombieFabric)
        {
            _timeToNewSpawnLevel = timeToNewSpawnLevel;
            _minimalTimeToSpawn = minimalTimeToSpawn;
            _baseTimeToSpawnNewZombie = baseTimeToSpawnNewZombie;
            _reductionTime = reductionTime;
            _zombieFabric = zombieFabric;

            _newTimeToNextSpawn = _baseTimeToSpawnNewZombie;
            _timeToNewSpawn = _timeToNewSpawnLevel;
            _isMinimalValueReached = false;

        }

        public void Update()
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
