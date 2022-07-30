using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAi : MonoBehaviour,IInput
{
    public Action<Vector3> OnMovementDirectionInput { get ; set ; }
    public Action<Vector2> OnMovementInput { get; set; }

    bool playerDetectionResult;
    public Transform eyeTransform;
    Transform playerTransfrom;
    public LayerMask playerLayer;
    public float visionDistance, stopDistance = 2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerDetectionResult = DetectPlayer();
        if (playerDetectionResult)
        {
            var directionToPlayer=playerTransfrom.position - eyeTransform.position;
            directionToPlayer = Vector3.Scale(directionToPlayer, Vector3.forward + Vector3.right);
            if (directionToPlayer.magnitude > stopDistance)
            {
                directionToPlayer.Normalize();
                OnMovementInput?.Invoke(Vector2.up);
                OnMovementDirectionInput?.Invoke(directionToPlayer);
                return;
            }
        }
            OnMovementInput?.Invoke(Vector2.zero);
            OnMovementDirectionInput?.Invoke(transform.forward);
        
    }

    private bool DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(eyeTransform.position, visionDistance,playerLayer);
        foreach (var item in colliders)
        {
            playerTransfrom = item.transform;
            return true;
        }
        playerTransfrom = null;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(playerDetectionResult)
            Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(eyeTransform.position, visionDistance);
    }
}
