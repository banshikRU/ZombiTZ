using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/New Weapon", order = 1)]

public class Weapon : ScriptableObject
{
    public Bullet Bullet;
    public Sprite WeaponSprite;

    public string WeaponName;
    public float ShootsInOneSecond;
}
