using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : Enemy, IDamageable
{
    public void GetHit(float damage)
    {
        headth -= damage;
        if (headth < 1)
        {
            headth = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
    }

    public void SetOff()
    {
        targetPoint.GetComponent<Bomb>().TurnOff();
    }
}
