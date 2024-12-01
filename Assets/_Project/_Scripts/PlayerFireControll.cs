using UnityEngine;

public class PlayerFireControll : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler _weaponHandler;
    [SerializeField]
    private BaseBullet _bullet;
    [SerializeField]
    private GameStateUpdater _gameStateUpdater;
    
    private float _shootsInOneSeconds;
    private float _shootInSecond;
    private float _canShoot;

    private ObjectPool _objectPool;

    private void Start()
    {
        ObjectPoolOrganizer poolOrganizer = FindObjectOfType<ObjectPoolOrganizer>();
        _objectPool = poolOrganizer.GetPool(_bullet.gameObject.name);
    }

    private void Update()
    {
        TakeInput();
    }

    private void OnEnable()
    {
        _gameStateUpdater.OnGamePlayed += TakeWeapon;
    }

    private void OnDisable()
    {
        _gameStateUpdater.OnGamePlayed -= TakeWeapon;
    }

    private void TakeWeapon()
    {
        _weaponHandler.TakeWeapon();
        _shootsInOneSeconds = _weaponHandler.WeaponFireRate;
        _shootInSecond = _shootsInOneSeconds;
    }

    private void TakeInput()
    {
        if (!_gameStateUpdater.isGame) return;
        if (Input.GetMouseButton(0) && Time.time >= _shootInSecond)
        {
            _shootInSecond = Time.time + _shootsInOneSeconds;
            TakeBulletFromPool(Input.mousePosition);

        }
    }

    private void TakeBulletFromPool(Vector3 vector)
    {

        GameObject bulletObject = _objectPool.GetPooledObject().gameObject;
        if (bulletObject == null)
            return;

        bulletObject.SetActive(true);
        bulletObject.transform.SetPositionAndRotation(_weaponHandler.transform.position, Quaternion.identity);
        BaseBullet baseBullet = bulletObject.GetComponent<BaseBullet>();
        baseBullet.StartMoveBullet(DirectionDefine(vector), _weaponHandler.BulletDamage);
    }

    private Vector2 DirectionDefine(Vector3 vector)
    {
        return (Utilities.GetWorldMousePosition() - transform.position).normalized;
    }
}
