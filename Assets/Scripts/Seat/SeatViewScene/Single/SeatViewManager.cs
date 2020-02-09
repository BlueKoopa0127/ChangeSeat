using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace SeatView
{
    public class SeatViewManager : BaseSeatManager
    {
        private List<string[]> seatCSVData;

        private void Awake()
        {
            BaseAwake();

            if (File.Exists(ChangeSeat.ChangeSeatManager.SeatFilePath))
            {
                seatCSVData = ChangeSeat.ChangeSeatManager.SeatCSVData;

                CreateSeats<SeatControl>(seatCSVData, this);

                ContentSizeChange();
            }
        }

        public void CopySeatsData()
        {
            string AllData = "";

            foreach(string[] horizontalData in seatCSVData)
            {
                int count = 0;
                foreach(string seatData in horizontalData)
                {
                    int num = int.Parse(seatData);

                    if(num <= 0)
                    {
                        horizontalData[count] = "";
                    }
                    count++;
                }

                string joinHorizontal = string.Join("	", horizontalData);
                AllData += joinHorizontal + "\n";
            }

            GUIUtility.systemCopyBuffer = AllData;
        }
    }
}