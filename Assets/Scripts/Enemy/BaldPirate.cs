using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPirate : Enemy, IDamageable
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
}
