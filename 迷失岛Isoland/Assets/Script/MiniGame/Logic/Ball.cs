using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public BallDetails ballDetails;
    public  bool isMatch;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetupBall(BallDetails ball)
    {
        ballDetails = ball;
        if (isMatch)
        {
            SetRightSprite();
        }
        else
            SetWrongSprite();
    }
    public void SetRightSprite()
    {
        spriteRenderer.sprite = ballDetails.rightSprite;
    }
    public void SetWrongSprite()
    {
        spriteRenderer.sprite = ballDetails.wrongSprite;
    }
}
