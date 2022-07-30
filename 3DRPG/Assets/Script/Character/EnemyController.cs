using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates {GUARD,PARTOL,CHASE,DEAD }
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterStats))]
public class EnemyController : MonoBehaviour,IEndGameObserver
{
    NavMeshAgent agent;
    EnemyStates enemyStates;
    Animator anim;
    protected CharacterStats characterStats;
    Collider coll;
    //������Χ 
    public float sightRadius;
    //Ѳ�߷�Χ
    public float partolRadius;
    public bool isGuard;
    //Ѳ��ͣ��ʱ��
    public float lookAtTime;
    float remainLookAtTime;
    float speed;
    //cdʱ��
    float lastAttackTime;
    //��һ��ǰ����Ѳ�ߵ�
    Vector3 wayPoint;
    //��ʼ��
    Vector3 guardPoint;
    Quaternion guardRotation;
    protected GameObject attackTarget;
    bool isWalk, isChase, isFollow, isDie;
    bool playerDie;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        coll = GetComponent<Collider>();
        //����ԭ���ٶȣ�
        speed = agent.speed;
        guardPoint = transform.position;
        remainLookAtTime = lookAtTime;
        lastAttackTime = characterStats.CoolDown;
        guardRotation = transform.rotation;
    }
    void Start()
    {
        if (isGuard)
        {
            enemyStates = EnemyStates.GUARD;
        }
        else
        {
            enemyStates = EnemyStates.PARTOL;
            GetNewPartolPoint();
        }
        GameManager.Instance.AddObserver(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (characterStats.CurrentHealth == 0)
        {
            isDie = true;
        }
        if (!playerDie)
        {
            SwitchStates();
            SwitchAnim();
            if (lastAttackTime > 0f)
            {
                lastAttackTime -= Time.deltaTime;
            }
        }
    }
    
    //private void OnEnable()
    //{
    //    Gamemanager.Instance.AddObserver(this);
    //}

    private void OnDisable()
    {
        if (!GameManager.isInitialized) return;
        GameManager.Instance.RemoveObserver(this);
    }

    void SwitchStates()
    {
        if (isDie)
        {
            enemyStates = EnemyStates.DEAD;
        }
        else if (FindPlayer())
        {
            enemyStates = EnemyStates.CHASE;
            
        }
        switch (enemyStates)
        {
            case EnemyStates.GUARD:
                isChase = false;
                isWalk = false;
                if (transform.position!=guardPoint)
                {
                    isWalk = true;
                    agent.isStopped = false;
                    agent.destination = guardPoint;
                    if(Vector3.SqrMagnitude(guardPoint - transform.position) <= agent.stoppingDistance)
                    {
                        isWalk = false;
                        agent.isStopped = true;
                        //��ת���ʼ�ĽǶ�
                        Quaternion.Lerp(transform.rotation, guardRotation, 0.1f);
                    }
                }
                break;
            case EnemyStates.PARTOL:
                isChase = false;
                agent.speed = speed * 0.5f;
                if (Vector3.Distance(transform.position, wayPoint) <= 1)
                {
                    isWalk = false;
                    if (remainLookAtTime < 0f)
                    {
                        GetNewPartolPoint();
                        remainLookAtTime = lookAtTime;
                    }
                    else
                        remainLookAtTime -= Time.deltaTime;
                }
                else
                {
                    isWalk = true;
                    agent.destination = wayPoint;
                }
                break;
            case EnemyStates.CHASE:

                agent.speed = speed;
                isWalk = false;
                isChase = true;
                //��ս
                if (!FindPlayer())
                {
                    isFollow = false;
                    agent.destination = transform.position;
                    if (remainLookAtTime > 0)
                    {
                        remainLookAtTime -= Time.deltaTime;
                    }
                    else
                    {
                        if (isGuard)
                        {
                            enemyStates = EnemyStates.GUARD;
                        }
                        else
                        {
                            enemyStates = EnemyStates.PARTOL;
                        }
                    }
                }
                else
                {
                    //׷��
                    isFollow = true;
                    agent.isStopped = false;
                    agent.destination = attackTarget.transform.position;
                    //�ж��Ƿ�׷����
                    if (TargetInAttackRange() || TargetInSkillRange())
                    {
                        isFollow = false;
                        agent.isStopped = true;
                        if (lastAttackTime < 0f)
                        {
                            lastAttackTime = characterStats.CoolDown;
                            //�����ж�
                            characterStats.isCritical = Random.value < characterStats.CriticalChance;
                            
                            Attack();
                        }
                    }
                }

                break;
            case EnemyStates.DEAD:
                coll.enabled = false;
                //agent.enabled = false;
                agent.radius = 0f;
                Destroy(gameObject, 2f);
                break;
            default:
                break;
        }
    }
    void Attack()
    {
        transform.LookAt(attackTarget.transform);
        anim.SetBool("Critical", characterStats.isCritical);
        if (TargetInAttackRange())
        {
            anim.SetTrigger("Attack");
        }
        if(TargetInSkillRange())
        {
            anim.SetTrigger("Skill");
        }
    }

    bool FindPlayer()
    {
        Collider[] colliderNum = Physics.OverlapSphere(transform.position, sightRadius);
        foreach (var collider in colliderNum)
        {
            
            if (collider.gameObject.CompareTag("Player"))
            {
                attackTarget = collider.gameObject;
                return true;
            }
        }
        return false;
    }

    void SwitchAnim()
    {
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Chase", isChase);
        anim.SetBool("Follow", isFollow);
        anim.SetBool("Die", isDie);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
    void GetNewPartolPoint()
    {
        float ran_x = Random.Range(-partolRadius, partolRadius);
        float ran_z = Random.Range(-partolRadius, partolRadius);
        //���޸ĸ߶ȣ���ֹ�ӿ����ݵĵ���
        wayPoint = new Vector3(guardPoint.x + ran_x, transform.position.y, guardPoint.z + ran_z);
        NavMeshHit hit;
        wayPoint = NavMesh.SamplePosition(wayPoint, out hit, sightRadius,1) ? hit.position : transform.position;
        

    }
    bool TargetInAttackRange()
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, transform.position) < characterStats.AttackRange;
        }
        else
        {
            return false;
        }
    }
    bool TargetInSkillRange()
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, transform.position) < characterStats.SkillRange;
        }
        else
        {
            return false;
        }
    }
    void Hit()
    {
        if (attackTarget != null && transform.IsFacingTarget(attackTarget.transform))
        {
            var targetStats = attackTarget.GetComponent<CharacterStats>();
            characterStats.TakeDamage(characterStats, targetStats);
        }
    }

    public void EndsNotify()
    {
        isChase = false;
        isWalk = false;
        attackTarget = null;
        playerDie = true;
        anim.SetBool("Victory", true);
    }
}
