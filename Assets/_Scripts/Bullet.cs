using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Parameters")]
    [SerializeField] private float _bulletSpeed = 1;
    [SerializeField] private int _bulletDamage = 1;
    [SerializeField] private float _lifeTime = 1.5f;

    public int Damage { get;private set; }

    private Vector3 _clickPointPosition;
    private bool isInit;

    private void Awake()
    {
        Damage = _bulletDamage;
    }
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
    public void Init(Vector3 _clickPosition)
    {
        _clickPointPosition = _clickPosition; 
        isInit = true;
        Destroy(gameObject, _lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            Destroy(gameObject);
        }
    }
}
