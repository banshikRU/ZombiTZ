using UnityEngine;
using ZombieGeneratorBehaviour;
using ObjectPoolSystem;

namespace WeaponControl
{
    [RequireComponent(typeof(PooledObject))]

    public class BaseBullet : MonoBehaviour
    {
        [Header("Bullet Parameters")]

        [SerializeField]
        private float _bulletSpeed = 1;
        [SerializeField]
        private float _lifeTime = 1.5f;

        private float _removeTimer;
        private int _bulletDamage;
        private bool _isMove;

        private Vector3 _clickPointPosition;
        private PooledObject _pooledObject;

        private void Awake()
        {
            _pooledObject = GetComponent<PooledObject>();
        }

        private void Update()
        {
            if (_isMove)
            {
                _removeTimer -= Time.deltaTime;
                if (_removeTimer <= 0)
                {
                    _isMove = false;
                    DeactivateObject();
                }
                BulletMove();
            }
        }

        public void StartMoveBullet(Vector3 _clickPosition, int bulletDamage)
        {
            _bulletDamage = bulletDamage;
            _clickPointPosition = _clickPosition;
            _removeTimer = _lifeTime;
            _isMove = true;
        }

        private void BulletMove()
        {
            transform.position += _bulletSpeed * Time.deltaTime * _clickPointPosition;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ZombieBehaviour>(out ZombieBehaviour zombie))
            {
                zombie.TakeDamage(_bulletDamage);
                DeactivateObject();
            }
        }

        private void DeactivateObject()
        {
            _pooledObject.ReturnToPool();
            gameObject.SetActive(false);
        }
    }
}


