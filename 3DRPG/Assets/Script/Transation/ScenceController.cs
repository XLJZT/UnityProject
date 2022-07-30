using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.AI;

public class ScenceController : SingleTon<ScenceController>
{
    GameObject player;
    NavMeshAgent playerAgent;
    public void TransitionToDestination(TransiationPoint transiationPoint)
    {
        switch (transiationPoint.transitionType)
        {
            case TransiationPoint.TransitionType.SameScence:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transiationPoint.destinationTag));
                break;
            case TransiationPoint.TransitionType.DifferentScence:
                break;
            default:
                break;
        }
    }

    IEnumerator Transition(string scenceName,TransitionDestination.DestinationTag destinationTag)
    {
        player = GameManager.Instance.playerStats.gameObject;

        playerAgent = player.GetComponent<NavMeshAgent>();
        playerAgent.isStopped = true;

        player.transform.SetPositionAndRotation(GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
        yield return null;
    }

    TransitionDestination GetDestination(TransitionDestination.DestinationTag destinationTag)
    {
        var entrances = FindObjectsOfType<TransitionDestination>();
        foreach (var item in entrances)
        {
            if (item.destinationTag == destinationTag)
            {
                return item;
            }
        }
        return null;
    }
}
