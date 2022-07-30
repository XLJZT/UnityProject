using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SceneName]
    public string sceneFrom,sceneToGo;

    public void TeleportToScene()
    {
        TransitionManager.Instance.Transition(sceneFrom, sceneToGo);
    }
    
}
