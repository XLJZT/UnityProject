using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float lastAttackTime;
    //攻击目标对象
    GameObject attackTarget;
    CharacterStats characterStats;
    NavMeshAgent agent;
    Animator anim;
    bool isDie;
    float stopDistance;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();

    }

    private void Start()
    {
        MouseManager.Instance.OnMouseClicked += MoveToTarget;
        MouseManager.Instance.OnClickedEnemy += EventAttack;

        GameManager.Instance.RigisterPlayer(characterStats);
        stopDistance = agent.stoppingDistance;
    }
    private void Update()
    {
        isDie = characterStats.CurrentHealth == 0;
        if (isDie)
        {
            GameManager.Instance.NotifyObservers();
        }
        AnimationChange();
        if(lastAttackTime>0f)
            lastAttackTime -= Time.deltaTime;
    }
    void MoveToTarget(Vector3 target)
    {
        if (isDie) return;
        agent.isStopped = false;
        agent.stoppingDistance = stopDistance;
        StopAllCoroutines();
        agent.destination = target;
    }

    void AnimationChange()
    {
        anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
        anim.SetBool("Die", isDie);
    }

    void EventAttack(GameObject target)
    {
        if (isDie) return;

        if (target != null)
        {
            attackTarget = target;
            StartCoroutine("MoveToAttackTarget");
        }
    }
    void Hit()
    {
        if (attackTarget.CompareTag("Attackable"))
        {
            if (attackTarget.GetComponent<Rock>())
            {
                //在空中也可以击飞
                attackTarget.GetComponent<Rock>().rockStates = Rock.RockStates.HitEnemy;
                //赋予初始速度，防止fixudate时修改rock状态
                attackTarget.GetComponent<Rigidbody>().velocity = Vector3.one;
                attackTarget.GetComponent<Rigidbody>().AddForce(transform.forward * 20f,ForceMode.Impulse);

            }
        }
        else
        {
            var targetStats = attackTarget.GetComponent<CharacterStats>();
            characterStats.TakeDamage(characterStats, targetStats);
        }
    }
    IEnumerator MoveToAttackTarget()
    {
        agent.stoppingDistance = characterStats.AttackRange;
        agent.isStopped = false;
        while (Vector3.Distance(attackTarget.transform.position, transform.position) > characterStats.AttackRange)
        {
            agent.destination = attackTarget.transform.position;
            yield return null;
        }
        agent.isStopped = true;
        if (lastAttackTime < 0)
        {
            transform.LookAt(attackTarget.transform.position);
            //暴击率的计算
            characterStats.isCritical = Random.value < characterStats.CriticalChance;
            anim.SetBool("Critical",characterStats.isCritical);
            anim.SetTrigger("Attack");
            //重置冷却时间
            lastAttackTime = characterStats.CoolDown;
        }
    }
}
