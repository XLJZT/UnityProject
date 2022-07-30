using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogController))]
public class CharacterH2 : Interactive
{
    public DialogController dialogController;

    private void Awake()
    {
        dialogController = GetComponent<DialogController>();

    }

    public override void EmptyClick()
    {
        if(isDone)
            dialogController.ShowDialogFinish();
        else
            dialogController.ShowDialogEmpty();
    }

    protected override void OnClickAction()
    {
        dialogController.ShowDialogFinish();
    }
}
