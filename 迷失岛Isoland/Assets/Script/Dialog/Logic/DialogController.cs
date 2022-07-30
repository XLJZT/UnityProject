using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public Dialogue_SO dialogEmpty;
    public Dialogue_SO dialogFinish;

    private Stack<string> dialogEmptyStack;
    private Stack<string> dialogFinishStack;

    private bool isTalking;

    private void Awake()
    {
        FillDialogStack();
    }
    private void FillDialogStack()
    {
        dialogEmptyStack = new Stack<string>();
        dialogFinishStack = new Stack<string>();

        for (int i = dialogEmpty.Dialog.Count - 1; i >-1; i--)
        {
            dialogEmptyStack.Push(dialogEmpty.Dialog[i]);
        }
        for (int i = dialogFinish.Dialog.Count - 1; i >-1; i--)
        {
            dialogFinishStack.Push(dialogFinish.Dialog[i]);
        }
    }

    public void ShowDialogEmpty()
    {
        StartCoroutine(DialogRoutine(dialogEmptyStack));
    }

    public void ShowDialogFinish()
    {
        StartCoroutine(DialogRoutine(dialogFinishStack));

    }

    IEnumerator DialogRoutine(Stack<string> data)
    {
        isTalking = true;
        if(data.TryPop(out string result))
        {
            EventHandler.CallShowDialogEvent(result);
            yield return null;
            EventHandler.CallGameStateChangeEvent(GameState.Pause);
            isTalking = false;
        }
        else
        {
            //��ʱջ�еĻ���û���� Ȼ��رնԻ�����ʾ
            EventHandler.CallShowDialogEvent(string.Empty);
            //���еĻ�˵��һ��� ������ջ
            FillDialogStack();
            isTalking = false;
            EventHandler.CallGameStateChangeEvent(GameState.Play);

        }
    }
}
