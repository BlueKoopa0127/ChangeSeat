using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangeSeat
{
    public class SeatExportManager : MonoBehaviour
    {
        public void Export()
        {
            List<string[]> SeatData = new List<string[]>();

            foreach(Transform hori in transform)
            {
                string[] horiData = new string[hori.childCount];

                int count = 0;

                foreach(Transform seat in hori)
                {
                    SeatItemControl sic = seat.GetComponent<SeatItemControl>();
                    string num = "";

                    if(sic.StudentNumber <= 0)
                    {
                        num = "";
                    }
                    else
                    {
                        num = sic.StudentNumber.ToString();
                    }

                    horiData[count] = num;
                    count++;
                }

                SeatData.Add(horiData);
            }

            string AllData = "";

            foreach(string[] horiData in SeatData)
            {
                string joinData = string.Join("	", horiData);

                AllData += joinData + "\n";
            }

            GUIUtility.systemCopyBuffer = AllData;
        }
    }
}