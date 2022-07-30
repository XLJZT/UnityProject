using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class H2AReset : Interactive
{
    public Transform gearSprite;
    void Start()
    {
        gearSprite = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void EmptyClick()
    {
        //÷ÿ÷√”Œœ∑
        GameController.Instance.ResetGame();
        gearSprite.DOPunchRotation(Vector3.forward * 180, 1, 1, 0);
    }
}
