using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyScene : MonoBehaviour
{
    public enum SceneName
    {
        StartScene,
        ChangeSeatScene,
        CreateDataScene,
        ConfigScene,

        RosterScene,
        SeatFormScene,
        RandomNominateScene,
        SeatViewScene
    }

    public static void OpenScene(SceneName sc)
    {
        SceneManager.LoadScene(sc.ToString());
    }
}
