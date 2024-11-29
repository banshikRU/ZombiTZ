using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ZombieBehaviour : MonoBehaviour 
{
    [Header("Zombie Stats")]
    [SerializeField] protected float _speed;
    [SerializeField] protected int _healPoint;
    [SerializeField] protected int _scoresByDeath;
    [SerializeField] protected Sprite _zombieSprite;

    protected Transform _player;
    protected bool _isInit;
    protected SpriteRenderer _sprite;
    protected ScoreUpdater _scoreUpdater;

    protected virtual void Awake()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public virtual void Init(Transform player, ScoreUpdater gameManager)
    {
        _scoreUpdater = gameManager;
        _player = player;
        _isInit = true;
        _sprite.sprite = _zombieSprite;
    }
    public virtual void TakeDamage(int damage)
    {
        _healPoint -= damage;
        if (_healPoint <= 0)
        {
            _scoreUpdater.AddScores(_scoresByDeath);
            Destroy(gameObject);
        }
    }
}
