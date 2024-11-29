using UnityEngine;
using UnityEngine.Pool;

public class PlayerFireControll : MonoBehaviour
{
    [SerializeField] private WeaponHandler _weaponHandler;
    [SerializeField] private BaseBullet _bullet;
    [SerializeField] private ObjectPool _objectPool;
    
    private float _shootsInOneSeconds;
    private float _shootInSecond;
    private float _canShoot;

    private void Update()
    {
        TakeInput();
    }
    private void OnEnable()
    {
        GameStateUpdater.OnGamePlayed += TakeWeapon;
    }
    private void OnDisable()
    {
        GameStateUpdater.OnGamePlayed -= TakeWeapon;
    }
    private void TakeWeapon()
    {
        _weaponHandler.TakeWeapon();
        _shootsInOneSeconds = _weaponHandler.WeaponFireRate;
        _shootInSecond = _shootsInOneSeconds;
    }
    private void TakeInput()
    {
        if (!GameStateUpdater.isGame) return;
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
