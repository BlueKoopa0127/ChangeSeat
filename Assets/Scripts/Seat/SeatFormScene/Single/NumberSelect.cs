using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace SeatForm
{
    public class NumberSelect : MonoBehaviour
    {
        [SerializeField]
        private Dropdown dropdown = null;

        private List<string> options = new List<string>();

        public int seatNumber
        {
            set { dropdown.value = value; }
        }

        public SeatControl sc;

        private void Awake()
        {
            options.Add("使用する");

            bool configDisplay = Config.ConfigManager.GetConfig().display;

            if (File.Exists(Roster.RosterManager.RosterFilePath))
            {
                List<StudentData> rosterData = Roster.RosterManager.RosterCSVData;

                if (configDisplay)
                {
                    for (int count = 1; count <= rosterData.Count; count++)
                    {
                        options.Add(rosterData[count - 1].number.ToString());
                    }
                }
                else
                {
                    for (int count = 1; count <= rosterData.Count; count++)
                    {
                        options.Add(rosterData[count - 1].studentName);
                    }
                }
            }

            dropdown.AddOptions(options);
        }

        public void Close()
        {
            sc.StudentNumber = dropdown.value;
        }
    }
}