using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ZombieBehaviour : MonoBehaviour
{
    [Header("Zombie Stats")]
    [SerializeField] private float _speed;
    [SerializeField] private int _healPoint;
    [SerializeField] private int _scoresByDeath;
    [SerializeField] private Sprite _zombieSprite;

    private Transform _player;
    private bool _isInit;
    private SpriteRenderer _sprite;
    private GameManager _gameManager;

    private void Awake()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public void Init(Transform player,GameManager gameManager)
    {
        _gameManager = gameManager;
        _player = player;
        _isInit = true;
        _sprite.sprite = _zombieSprite;
    }

    public void Update()
    {
        if (_isInit)
        {
            MoveToPlyer();
        }
    }
    private void MoveToPlyer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        float step = _speed * Time.deltaTime;
        transform.position += direction * step;
    }
    private void TakeDamage(int damage)
    {
        _healPoint -=  damage;
        if (_healPoint <= 0)
        {
            _gameManager.AddScores(_scoresByDeath);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Debug.Log("Take Damage");
            Bullet bullet = collision.GetComponent<Bullet>();
            TakeDamage(bullet.Damage);
        }
    }
}
