using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public bool bombAvailable;
    int dir;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.position.x > other.transform.position.x)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }

        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(1);
        }
        
        if (other.CompareTag("Bomb") && bombAvailable)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 1) * 10, ForceMode2D.Impulse);
        }
    }
}
