using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangeSeat
{
    public class ErrorChecker : MonoBehaviour
    {
        [SerializeField]
        private ChangeSeatManager manager = null;

        public string ErrorCheck()
        {
            int manCount = 0, womanCount = 0, seatManCount = 0, seatWomanCount = 0, seatFreeCount = 0;

            List<string[]> seatData = CSVManager.CSVReader.CSVRead(SeatForm.SeatFormManager.SeatFormPath);

            List<List<StudentData>> restList = new List<List<StudentData>>();
            for (int n = 1; n <= seatData.Count; n++)
            {
                restList.Add(new List<StudentData>());
            }

            List<StudentData> restManList = new List<StudentData>();
            List<StudentData> restWomanList = new List<StudentData>();


            foreach (StudentData sd in manager.rosterData)
            {
                if (sd.isMan)
                {
                    manCount++;
                }
                else
                {
                    womanCount++;
                }


                if (restList.Count >= sd.restrictionNumber && sd.restrictionNumber >= 1)  
                {
                    restList[sd.restrictionNumber - 1].Add(sd);
                    
                    if (sd.isMan)
                    {
                        restManList.Add(sd);
                    }
                    else
                    {
                        restWomanList.Add(sd);
                    }
                }
                else
                {
                    if (sd.restrictionNumber != 0)
                    {
                        return "席の列が" + restList.Count + "列に対して制限の列が" + sd.restrictionNumber + "なので席替えできません";
                    }
                }
            }

            int count = 1;
            foreach (string[] hori in seatData)
            {
                int horiManCount = 0, horiWomanCount = 0, horiSeatManCount = 0, horiSeatWomanCount = 0, horiSeatFreeCount = 0;
                for (int n = 1; n <= count; n++)
                {
                    foreach (StudentData sd in restList[n - 1]) 
                    {
                        if (sd.isMan)
                        {
                            horiManCount++;
                        }
                        else
                        {
                            horiWomanCount++;
                        }
                    }

                    foreach (string seat in seatData[n - 1]) 
                    {
                        switch (int.Parse(seat))
                        {
                            case 0:
                                horiSeatFreeCount++;
                                break;

                            case -1:
                                horiSeatManCount++;
                                break;

                            case -2:
                                horiSeatWomanCount++;
                                break;
                        }
                    }
                }

                if (horiManCount > horiSeatFreeCount + horiSeatManCount) 
                {
                    return count + "列目の制限された男性を減らしてください";
                }
                if (horiWomanCount > horiSeatFreeCount + horiSeatWomanCount) 
                {
                    return count + "列目の制限された女性を減らしてください";
                }
                if ((horiManCount + horiWomanCount) > (horiSeatFreeCount + horiSeatManCount + horiSeatWomanCount)) 
                {
                    return count + "列目の制限された男性または女性を減らしてください";
                }


                foreach (string seat in hori)
                {
                    switch (int.Parse(seat))
                    {
                        case 0:
                            seatFreeCount++;
                            break;

                        case -1:
                            seatManCount++;
                            break;

                        case -2:
                            seatWomanCount++;
                            break;
                    }
                }

                count++;
            }

            if (manCount > seatFreeCount + seatManCount) 
            {
                return "男性の数" + manCount + "に対して男性が座れる席が" + (seatFreeCount + seatManCount) + "なので席替えできません";
            }

            if(womanCount > seatFreeCount + seatWomanCount)
            {
                return "女性の数" + womanCount + "に対して女性が座れる席が" + (seatFreeCount + seatWomanCount) + "なので席替えできません";
            }


            if (seatManCount > manCount - restManList.Count)
            {
                int overCount = seatManCount - (manCount - restManList.Count);

                Debug.Log(overCount);

                int[] restNumbers = new int[seatData.Count];
                foreach (StudentData sd in restManList)
                {
                    restNumbers[sd.restrictionNumber - 1]++;
                }

                int horiCount = 0;
                foreach (string[] hori in seatData)
                {
                    int horiManCount = 0;
                    foreach (string seat in hori)
                    {
                        if(seat == "-1")
                        {
                            horiManCount++;
                        }
                    }

                    if (restNumbers[horiCount] > horiManCount)
                    {
                        return (horiCount + 1) + "列目の制限を" + (restNumbers[horiCount] - horiManCount) + "人減らす、または男性指定席を減らしてください";
                    }

                    horiCount++;
                }
            }
            if (seatWomanCount > womanCount - restWomanList.Count)
            {
                int[] restNumbers = new int[seatData.Count];
                foreach (StudentData sd in restWomanList)
                {
                    restNumbers[sd.restrictionNumber - 1]++;
                }

                int horiCount = 0;
                foreach (string[] hori in seatData)
                {
                    int horiWomanCount = 0;
                    foreach (string seat in hori)
                    {
                        if (seat == "-2")
                        {
                            horiWomanCount++;
                        }
                    }

                    if (restNumbers[horiCount] > horiWomanCount)
                    {
                        return (horiCount + 1) + "列目の制限を" + (restNumbers[horiCount] - horiWomanCount) + "人減らす、または女性指定席を減らしてください";
                    }

                    horiCount++;
                }
            }


            return "";
        }
    }
}