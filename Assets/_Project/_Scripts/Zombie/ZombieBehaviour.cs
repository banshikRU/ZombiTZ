using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PooledObject))]

public class ZombieBehaviour : MonoBehaviour 
{
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
    protected SpriteRenderer _sprite;
    protected ScoreValueUpdater _scoreUpdater;
    private PooledObject _pooledObject;

    protected bool _isInit;
    private int _currentHealPoint;

    protected virtual void Awake()
    {
        _pooledObject = GetComponent<PooledObject>();
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public virtual void Init(Transform player, ScoreValueUpdater gameManager)
    {
        _currentHealPoint = _healPoint;
        _scoreUpdater = gameManager;
        _player = player;
        _isInit = true;
        _sprite.sprite = _zombieSprite;
    }

    public virtual void TakeDamage(int damage)
    {
        _currentHealPoint -= damage;
        if (_currentHealPoint <= 0)
        {
            _scoreUpdater.AddScores(_scoresByDeath);
            DeactivateObject();
        }
    }

    private void DeactivateObject()
    {
        _pooledObject.ReturnToPool();
        gameObject.SetActive(false);
    }
}
