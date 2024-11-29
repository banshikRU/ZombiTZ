using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [Header("Bullet Parameters")]
    [SerializeField] private float _bulletSpeed = 1;
    [SerializeField] private float _lifeTime = 1.5f;

    private int _bulletDamage;
    private Vector3 _clickPointPosition;
    private bool isInit;

    private void Update()
    {
        if (isInit )
        {
            BulletMove();
        }
    }
    private void BulletMove()
    {
        transform.position += _clickPointPosition * _bulletSpeed * Time.deltaTime;
    }
    public void Init(Vector3 _clickPosition,int bulletDamage)
    {
        _bulletDamage = bulletDamage;
        _clickPointPosition = _clickPosition; 
        isInit = true;
        Destroy(gameObject, _lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ZombieBehaviour>(out ZombieBehaviour zombie))
        {
            zombie.TakeDamage(_bulletDamage);
            Destroy(gameObject);
        }
    }
}
