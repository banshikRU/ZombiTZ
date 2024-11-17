using UnityEngine;

public class PlayerFireControll : MonoBehaviour
{
    [SerializeField] private WeaponHandler _weaponHandler;
    [SerializeField] private BaseBullet _bullet;
    
    private float _shootsInOneSeconds;
    private float _shootInSecond;
    private float _canShoot;

    private void Update()
    {
        TakeInput();
    }
    private void OnEnable()
    {
        GameManager.OnGamePlayed += TakeWeapon;
    }
    private void OnDisable()
    {
        GameManager.OnGamePlayed -= TakeWeapon;
    }
    private void TakeWeapon()
    {
        _weaponHandler.TakeWeapon();
        _shootsInOneSeconds = _weaponHandler.WeaponFireRate;
        _shootInSecond = _shootsInOneSeconds;
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
        BaseBullet bullet = Instantiate(_bullet, _weaponHandler.transform.position, Quaternion.identity);
        bullet.Init(DirectionDefine(vector),_weaponHandler.BulletDamage);
    }
    private Vector2 DirectionDefine(Vector3 vector)
    {
        return (HelpClass.GetWorldMousePosition() - transform.position).normalized;
    }
}
