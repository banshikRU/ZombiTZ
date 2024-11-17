using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private Transform _player; 
    [SerializeField] private float _distanceFromPlayer = 0.25f;

    private SpriteRenderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        GameManager.OnGamePlayed += TakeWeapon;
    }
    private void OnDisable()
    {
        GameManager.OnGamePlayed -= TakeWeapon;
    }
    private void Start()
    {
        TakeWeapon();
    }
    private void TakeWeapon()
    {
        _renderer.sprite = _currentWeapon?.WeaponSprite;
    }
    private void Update()
    {
        RotateWeaponTowardsMouse();
    }
    private void RotateWeaponTowardsMouse()
    {
        transform.position = _player.position + GetDirectionPlayerToMouse() * _distanceFromPlayer;
        float angle = Mathf.Atan2(GetDirectionPlayerToMouse().y, GetDirectionPlayerToMouse().x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private Vector3 GetDirectionPlayerToMouse()
    {
        return (HelpClass.GetWorldMousePosition() - _player.position).normalized;
    }
}
