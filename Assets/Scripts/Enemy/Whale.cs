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

    public override void AnimationEventSkillTrigger(int i)
    {
        base.AnimationEventSkillTrigger(i);

        targetPoint.GetComponent<Bomb>().TurnOff();
        targetPoint.gameObject.SetActive(false);

        transform.localScale *= scale;
    }
}
