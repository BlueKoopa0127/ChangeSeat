using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace MyEditor
{
    public class DirectoryCreate : MonoBehaviour
    {
        [MenuItem("DirectoryCreate/AllDirectory")]
        private static void All()
        {
            Scripts();
            Prefabs();
        }

        [MenuItem("DirectoryCreate/Scripts")]
        private static void Scripts()
        {
            string directoryName = "/Scripts";

            CheckAndCreate(directoryName);
        }

        [MenuItem("DirectoryCreate/Prefabs")]
        private static void Prefabs()
        {
            string directoryName = "/Prefabs";

            CheckAndCreate(directoryName);
        }

        private static void CheckAndCreate(string dirName)
        {
            dirName = Application.dataPath + dirName;

            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
        }
    }
}