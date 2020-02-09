using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileChecker : MonoBehaviour
{
    private string saveData = "/SaveData";

    private void Awake()
    {
        string app;

        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            app = Application.persistentDataPath;
        }
        else
        {
            app = Application.dataPath;
        }

        if (!Directory.Exists(app + saveData))
        {
            Directory.CreateDirectory(app + saveData);
        }
    }
}
