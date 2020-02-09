using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace MyJsonManager
{
    public class JsonManager
    {
        public static void SaveJson<T>(string filePath, T jsonSaveClass)
        {
            string jsonStr = JsonUtility.ToJson(jsonSaveClass, true);

            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(jsonStr);
            writer.Flush();
            writer.Close();
        }

        public static T GetJson<T>(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            string jsonStr = reader.ReadToEnd();
            reader.Close();

            T jsonData = JsonUtility.FromJson<T>(jsonStr);

            return jsonData;
        }
    }
}