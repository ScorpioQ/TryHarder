using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    BoxCollider2D coll;

    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

        GameManager.instance.IsExitDoor(this);

        coll.enabled = false;
    }

    public void OpenDoor()
    {
        anim.Play("open");
        coll.enabled = true;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.NextLevel();
        }
    }
}
