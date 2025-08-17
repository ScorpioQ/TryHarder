using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{
    EnemyBaseState currentState;

    public Animator anim;
    public int animState;

    private GameObject alarmSign;

    public float headth;
    public bool isDead;
    public bool hasBomb;
    public bool isBoss;
    public bool isPlayer;

    public float speed;
    public Transform pointA, pointB;
    public Transform targetPoint;

    public float attackRate;
    public float attackRange, skillRange;
    public float nextAttack = 0;

    public List<Transform> attackList = new List<Transform>();

    public PatrolState patrolState = new PatrolState();
    public AttackState attackState = new AttackState();

    public virtual void Init()
    {
        anim = GetComponent<Animator>();
        alarmSign = transform.GetChild(0).gameObject;
    }

    public void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        TransitionToState(patrolState);

        if (isBoss)
        {
            UIManager.instance.SetBossHealth(headth);
        }
        GameManager.instance.IsEnemy(this);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        anim.SetBool("dead", isDead);
        if (isDead)
        {
            GameManager.instance.EnemyDead(this);
            return;
        }
        currentState.OnUpdate(this);
        anim.SetInteger("state", animState);

        if (isBoss)
        {
            UIManager.instance.UpdateBossHealth(headth);
        }
    }

    public void TransitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        FlipDirection();
    }

    public void AttackAction()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < attackRange)
        {
            if (Time.time > nextAttack)
            {
                anim.SetTrigger("attack");
                nextAttack = Time.time + attackRate;
            }
        }
    }

    public virtual void SkillAction()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)
        {
            if (Time.time > nextAttack)
            {
                anim.SetTrigger("skill");
                nextAttack = Time.time + attackRate;
            }
        }
    }

    public virtual void FlipDirection()
    {
        if (transform.position.x < targetPoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    public void SwitchPoint()
    {
        if (Mathf.Abs(transform.position.x - pointA.position.x) > Mathf.Abs(transform.position.x - pointB.position.x))
        {
            targetPoint = pointA;
        }
        else
        {
            targetPoint = pointB;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!attackList.Contains(collision.transform) && !hasBomb && !isDead && !GameManager.instance.gameOver)
        {
            attackList.Add(collision.transform);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (attackList.Contains(collision.transform))
        {
            attackList.Remove(collision.transform);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && !GameManager.instance.gameOver && !isPlayer)
        {
            StartCoroutine(OnAlarm());
        }
    }

    IEnumerator OnAlarm()
    {
        alarmSign.SetActive(true);
        yield return new WaitForSeconds(alarmSign.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        alarmSign.SetActive(false);
    }
    
    public virtual void AnimationEventSkillStart()
    {

    }

    public virtual void AnimationEventSkillTrigger(int i)
    {

    }

    public virtual void AnimationEventSkillEnd()
    {

    }

    public virtual void AnimationEventLandStart()
    {

    }

    public virtual void AnimationEventJumpStart()
    {

    }
}
