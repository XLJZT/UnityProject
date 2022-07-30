using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    IInput input;
    AgentMovement agentMovement;
    private void OnEnable()
    {
        input = GetComponent<IInput>();
        agentMovement = GetComponent<AgentMovement>();
        input.OnMovementInput += agentMovement.HandleMovement;
        input.OnMovementDirectionInput += agentMovement.HandleMovementDirection;
    }
    private void OnDisable()
    {
        input.OnMovementInput -= agentMovement.HandleMovement;
        input.OnMovementDirectionInput -= agentMovement.HandleMovementDirection;
    }
}
