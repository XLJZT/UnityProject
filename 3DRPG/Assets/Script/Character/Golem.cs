using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : EnemyController
{
    [Header("GRUNTÍÆÁ¦")]
    public float kickForce = 25f;

    public GameObject rockPerfab;
    public Transform handTransform;
    //animation event
    void KickOff()
    {
        if (attackTarget != null && transform.IsFacingTarget(attackTarget.transform))
        {
            var targetStats = attackTarget.GetComponent<CharacterStats>();
            Vector3 direction = attackTarget.transform.position - transform.position;
            direction.Normalize();
            transform.LookAt(attackTarget.transform);
            attackTarget.GetComponent<NavMeshAgent>().isStopped = true;
            attackTarget.GetComponent<NavMeshAgent>().velocity = direction * kickForce;
            attackTarget.GetComponent<Animator>().SetTrigger("Dizzy");
            characterStats.TakeDamage(characterStats, targetStats);
        }
    }
    //animation event
    void ThrowRock()
    {
        if (attackTarget != null)
        {
            GameObject rock = Instantiate(rockPerfab, handTransform.position, Quaternion.identity);
            rock.GetComponent<Rock>().target = attackTarget;
        }
    }
}
