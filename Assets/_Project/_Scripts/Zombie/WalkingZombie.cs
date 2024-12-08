using UnityEngine;

namespace ZombieGeneratorBehaviour
{
    public class WalkingZombie : ZombieBehaviour
    {
        private void MoveToPlyer()
        {
            Vector3 direction = (_player.position - transform.position).normalized;
            float step = _speed * Time.deltaTime;
            transform.position += direction * step;
        }

        private void Update()
        {
            if (_isInit)
            {
                MoveToPlyer();
            }
        }
    }

}
