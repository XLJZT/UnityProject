using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    CharacterController controller;
    public float rotationSpeed, movementSpeed, gravity = 20f;
    Vector3 movementVector = Vector3.zero;
    private float desiredRotationAngle = 0;

    Animator animator;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            //获取向量到原点的长度
            if (movementVector.magnitude > 0)
            {
                var animationSpeedMultiplier = SetCorrectAnimation();
                RotateAgent();
                //用currentAnimatorSpeed限制移动速度，使旋转时速度慢
                movementVector *= animationSpeedMultiplier;
            }
        }
        //重力作用
        movementVector.y -= gravity;
        Debug.Log(movementVector);
        controller.Move(movementVector*Time.deltaTime);
    }

    public void HandleMovement(Vector2 input)
    {
        if (controller.isGrounded)
        {
            if (input.y > 0)
            {
                movementVector = transform.forward * movementSpeed;
            }
            else
            {
                movementVector = Vector3.zero;
                animator.SetFloat("move", 0);
            }
        }
    }

    public void HandleMovementDirection(Vector3 direction)
    {
        desiredRotationAngle = Vector3.Angle(transform.forward, direction);
        //判断旋转左右
        var crossProduct = Vector3.Cross(transform.forward, direction).y;
        if (crossProduct < 0)
        {
            desiredRotationAngle *= -1;
        }
    }

    void RotateAgent()
    {
        //出现一个情况 当角度小于10的时候并不会发生旋转
        if (desiredRotationAngle > 10 || desiredRotationAngle < -10)
        {
            transform.Rotate(Vector3.up*desiredRotationAngle*rotationSpeed*Time.deltaTime);
        }
    }
    public float SetCorrectAnimation()
    {
        float currentAnimationSpeed = animator.GetFloat("move");
        
        if (desiredRotationAngle > 10f || desiredRotationAngle < -10f)
        {
            //判断是不是起步状态
            if (currentAnimationSpeed < 0.2f)
            {
                currentAnimationSpeed += Time.deltaTime * 2;
                currentAnimationSpeed = Mathf.Clamp(currentAnimationSpeed, 0, 0.2f);
            }
            animator.SetFloat("move", currentAnimationSpeed);
        }
        else
        {
            if (currentAnimationSpeed < 1f)
            {
                currentAnimationSpeed += Time.deltaTime * 2;

            }
            else
            {
                currentAnimationSpeed = 1;
            }
            animator.SetFloat("move", currentAnimationSpeed);
        }
        return currentAnimationSpeed;
    }

}
