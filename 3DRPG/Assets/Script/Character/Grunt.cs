using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Grunt : EnemyController
{
    [Header("GRUNT����")]
    public float kickForce = 15f;
    void KickOff()
    {
        if (attackTarget != null)
        {
            transform.LookAt(attackTarget.transform);
            Vector3 direction = attackTarget.transform.position - transform.position;
            direction.Normalize();
            //���player�ж������л���
            attackTarget.GetComponent<NavMeshAgent>().isStopped = true;
            attackTarget.GetComponent<NavMeshAgent>().velocity = direction * kickForce;
            attackTarget.GetComponent<Animator>().SetTrigger("Dizzy");
        }
    }
}
