using UnityEngine;

public class WalkingZombie : ZombieBehaviour
{
    protected override void MoveToPlyer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        float step = _speed * Time.deltaTime;
        transform.position += direction * step;
    }
}
