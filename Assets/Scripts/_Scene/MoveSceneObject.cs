using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSceneObject : MonoBehaviour
{
    public MyScene.SceneName sc;

    public void Open()
    {
        MyScene.OpenScene(sc);
    }
}
