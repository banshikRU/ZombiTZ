using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ZombieBehaviour : MonoBehaviour
{
    [Header("Zombie Stats")]
    [SerializeField] private float _speed;
    [SerializeField] private float _healPoint;
    [SerializeField] private float _scoresByDeath;
    [SerializeField] private Sprite _zombieSprite;

    private Transform _player;
    private bool _isMove;
    private SpriteRenderer _sprite;
    private void Awake()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public void Init(Transform player)
    {
        _player = player;
        _isMove = true;
        _sprite.sprite = _zombieSprite;
    }

    public void Update()
    {
        if (_isMove)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
