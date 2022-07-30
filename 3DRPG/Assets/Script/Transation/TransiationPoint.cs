using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransiationPoint : MonoBehaviour
{
    public enum TransitionType { SameScence,DifferentScence}

    [Header("Transition Info")]
    public string scenceName;
    public TransitionType transitionType;
    public TransitionDestination.DestinationTag destinationTag;

    bool canTrans;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canTrans)
        {
            ScenceController.Instance.TransitionToDestination(this);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrans = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrans = false;
        }
    }
}
