using UnityEngine;

public class PlayerFireControll : MonoBehaviour
{
    [SerializeField] private BaseBullet _bullet;
    [SerializeField] private float _shootsInOneSeconds = 0.1f;

    private float _shootInSecond;
    private float _canShoot;

    private void Awake()
    {
        _shootInSecond = _shootsInOneSeconds;
    }
    private void Update()
    {
        TakeInput();
    }
    private void TakeInput()
    {
        if (!GameManager.isGame) return;
        if (Input.GetMouseButton(0) && Time.time >= _shootInSecond)
        {
            _shootInSecond = Time.time + _shootsInOneSeconds;
            GenerateBullet(Input.mousePosition);

        }
    }
    private void GenerateBullet(Vector3 vector)
    {
        BaseBullet bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
        bullet.Init(DirectionDefine(vector));
    }
    private Vector2 DirectionDefine(Vector3 vector)
    {
        return (HelpClass.GetWorldMousePosition() - transform.position).normalized;
    }
}
