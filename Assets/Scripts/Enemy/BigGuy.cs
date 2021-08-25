using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGuy : Enemy, IDamageable
{
    public Transform pickupPoint;
    public float power;
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

        switch (i) {
            case 0:
                if (targetPoint.CompareTag("Bomb") && !hasBomb)
                {
                    targetPoint.gameObject.transform.position = pickupPoint.position;
                    targetPoint.SetParent(pickupPoint);
                    targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    hasBomb = true;
                }
                break;
            
            case 1:
                if (hasBomb)
                {
                    targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    targetPoint.SetParent(transform.parent.parent);

                    if (FindObjectOfType<PlayerController>().gameObject.transform.position.x - transform.position.x < 0)
                    {
                        targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * power, ForceMode2D.Impulse);
                    }
                    else
                    {
                        targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * power, ForceMode2D.Impulse);
                    }
                }
                hasBomb = false;
                break;
        }
    }
}
