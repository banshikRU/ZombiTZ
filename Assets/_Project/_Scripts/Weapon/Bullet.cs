using UnityEngine;

namespace WeaponControl
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "Scriptable Objects/New Bullet", order = 1)]

    public class Bullet : ScriptableObject
    {
        public int BulletDamage;
    }
}

