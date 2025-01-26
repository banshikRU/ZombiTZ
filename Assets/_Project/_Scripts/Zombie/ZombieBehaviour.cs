using System;
using UnityEngine;
using UIControl;
using ObjectPoolSystem;

namespace ZombieGeneratorBehaviour
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PooledObject))]

    public class ZombieBehaviour : MonoBehaviour
    {
        public event Action<int,int> OnZombieTakeDamage;
        
        [Header("Zombie Stats")]

        [SerializeField]
        protected float _speed;
        [SerializeField]
        protected int _healPoint;
        [SerializeField]
        protected int _scoresByDeath;
        [SerializeField]
        protected Sprite _zombieSprite;

        protected Transform _player;
        private SpriteRenderer _sprite;
        private ScoreValueModel _scoreModel;
        private PooledObject _pooledObject;
        private ZombieFactory _zombieFactory;


        protected bool _isInit;
        private int _currentHealPoint;

        protected virtual void Awake()
        {
            _pooledObject = GetComponent<PooledObject>();
            _sprite = gameObject.GetComponent<SpriteRenderer>();
        }

        public virtual void Init(Transform player, ScoreValueModel gameManager,ZombieFactory zombieFactory)
        {
            _zombieFactory = zombieFactory;
            _currentHealPoint = _healPoint;
            _scoreModel = gameManager;
            _player = player;
            _isInit = true;
            _sprite.sprite = _zombieSprite;
        }

        public virtual void TakeDamage(int damage)
        {
            _currentHealPoint -= damage;
            OnZombieTakeDamage?.Invoke(_currentHealPoint,_healPoint);
            if (_currentHealPoint > 0)
                return;
            _scoreModel.AddScores(_scoresByDeath);
            DeactivateObject();
        }

        public void DeactivateObject()
        {
            _zombieFactory.DeleteFromZombieList(this);
            _pooledObject.ReturnToPool();
            gameObject.SetActive(false);
        }
        
    }
}


