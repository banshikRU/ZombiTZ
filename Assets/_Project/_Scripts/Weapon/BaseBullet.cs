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

        private float removeTimer;
        private int _bulletDamage;
        private bool isMove;

        private Vector3 _clickPointPosition;
        private PooledObject _pooledObject;

        private void Awake()
        {
            _pooledObject = GetComponent<PooledObject>();
        }

        private void Update()
        {
            if (isMove)
            {
                removeTimer -= Time.deltaTime;
                if (removeTimer <= 0)
                {
                    isMove = false;
                    DeactivateObject();
                }
                BulletMove();
            }
        }

        public void StartMoveBullet(Vector3 _clickPosition, int bulletDamage)
        {
            _bulletDamage = bulletDamage;
            _clickPointPosition = _clickPosition;
            removeTimer = _lifeTime;
            isMove = true;
        }

        private void BulletMove()
        {
            transform.position += _clickPointPosition * _bulletSpeed * Time.deltaTime;
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


