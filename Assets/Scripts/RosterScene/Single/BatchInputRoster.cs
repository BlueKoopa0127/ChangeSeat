using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Roster
{
    public class BatchInputRoster : MonoBehaviour
    {
        [SerializeField]
        private InputField AllName = null;

        public void Save()
        {
            string[] studentNames = AllName.text.Split('\n');

            List<string[]> saveData = new List<string[]>();

            int count = 1;

            foreach(string student in studentNames)
            {
                string[] data = new string[4];

                data[0] = count.ToString();
                data[1] = student;
                data[2] = "true";
                data[3] = "0";

                saveData.Add(data);

                count++;
            }

            CSVManager.CSVWriter.CSVWrite(RosterManager.RosterFilePath, saveData);
        }
    }
}