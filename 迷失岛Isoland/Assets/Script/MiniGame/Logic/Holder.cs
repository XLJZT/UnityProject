using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : Interactive
{
    public BallName matchBall;
    private Ball currentBall;
    public HashSet<Holder> linkHolders = new HashSet<Holder>();
    public bool isEmpty;
    

    public void CheckBall(Ball ball)
    {
        currentBall = ball;
        if (ball.ballDetails.ballName == matchBall)
        {
            currentBall.isMatch = true;
            currentBall.SetRightSprite();
        }
        else
        {
            currentBall.isMatch = false;
            currentBall.SetWrongSprite();
        }
    }

    public override void EmptyClick()
    {
        Debug.Log("进入空点");
        foreach (var holder in linkHolders)
        {
            if (holder.isEmpty)
            {
                //设置位置和父级
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);

                //检查球的状态
                holder.CheckBall(currentBall);
                this.currentBall = null;

                //修改状态
                this.isEmpty = true;
                holder.isEmpty = false;

                EventHandler.CallCheckGameStateEvent();
            }
        }
    }

}
