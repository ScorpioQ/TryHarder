using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : Enemy, IDamageable
{
    public float scale;
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

    public void Swalow()
    {
        targetPoint.GetComponent<Bomb>().TurnOff();
        targetPoint.gameObject.SetActive(false);

        transform.localScale *= scale;
    }
}
