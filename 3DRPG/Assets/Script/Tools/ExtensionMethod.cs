using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//拓展方法 
public static class ExtensionMethod 
{
    private const float dotThreshold = 0.5f;

    //拓展的是this后面的方法,后面的为方法所需要的函数
    //使用方法 :transform.IsFacingTarget(target)
    public static bool IsFacingTarget(this Transform transform,Transform target)
    {
        //向量OA-OB=BA;O为原点；
        Vector3 vectorToTarget = target.position - transform.position;
        vectorToTarget.Normalize();
        //vector3.dot 为看向的方向与vectortoTarget的方向的夹角，正对着为1，背对着为-1，左右为0；
        float dot = Vector3.Dot(transform.forward, vectorToTarget);
        return dot >= dotThreshold;
    }

}
