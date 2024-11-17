
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/New Weapon", order = 1)]

public class Weapon : ScriptableObject
{
    public string WeaponName;
    public Sprite WeaponSprite;
}
