using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��չ���� 
public static class ExtensionMethod 
{
    private const float dotThreshold = 0.5f;

    //��չ����this����ķ���,�����Ϊ��������Ҫ�ĺ���
    //ʹ�÷��� :transform.IsFacingTarget(target)
    public static bool IsFacingTarget(this Transform transform,Transform target)
    {
        //����OA-OB=BA;OΪԭ�㣻
        Vector3 vectorToTarget = target.position - transform.position;
        vectorToTarget.Normalize();
        //vector3.dot Ϊ����ķ�����vectortoTarget�ķ���ļнǣ�������Ϊ1��������Ϊ-1������Ϊ0��
        float dot = Vector3.Dot(transform.forward, vectorToTarget);
        return dot >= dotThreshold;
    }

}
