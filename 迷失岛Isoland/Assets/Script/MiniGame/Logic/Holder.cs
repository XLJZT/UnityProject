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
        Debug.Log("����յ�");
        foreach (var holder in linkHolders)
        {
            if (holder.isEmpty)
            {
                //����λ�ú͸���
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);

                //������״̬
                holder.CheckBall(currentBall);
                this.currentBall = null;

                //�޸�״̬
                this.isEmpty = true;
                holder.isEmpty = false;

                EventHandler.CallCheckGameStateEvent();
            }
        }
    }

}
