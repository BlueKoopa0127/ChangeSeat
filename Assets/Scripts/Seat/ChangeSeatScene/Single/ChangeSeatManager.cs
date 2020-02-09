using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace ChangeSeat
{
    public class ChangeSeatManager : BaseSeatManager
    {
        [SerializeField]
        private GameObject errorArea = null;
        [SerializeField]
        private Text errorText = null;

        [SerializeField]
        private GameObject randomOpenButton = null;

        [SerializeField]
        private ErrorChecker errorChecker = null;

        private List<Transform> freeStudents = new List<Transform>();

        public static string SeatFilePath
        {
            get
            {
                if(Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    return Application.persistentDataPath + "/SaveData/SeatData.csv";
                }
                else
                {
                    return Application.dataPath + "/SaveData/SeatData.csv";
                }
            }
        }

        public static List<string[]> SeatCSVData
        {
            get
            {
                if (Config.ConfigManager.GetConfig().reverse)
                {
                    List<string[]> csvData = CSVManager.CSVReader.CSVRead(SeatFilePath);

                    List<string[]> reverseData = new List<string[]>();


                    for (int horiCount = csvData.Count - 1; horiCount >= 0; horiCount--)
                    {
                        string[] horiReverse = new string[csvData[0].Length];

                        int count = 0;

                        for (int verCount = csvData[0].Length - 1; verCount >= 0; verCount--)
                        {
                            horiReverse[count] = csvData[horiCount][verCount];
                            count++;
                        }

                        reverseData.Add(horiReverse);
                    }

                    return reverseData;
                }
                else
                {
                    return CSVManager.CSVReader.CSVRead(SeatFilePath);
                }
            }
        }

        private List<SeatItemControl[]> AllSeatData;



        private void Awake()
        {
            BaseAwake();

            if (File.Exists(SeatForm.SeatFormManager.SeatFormPath))
            {
                List<string[]> seatFormData = SeatForm.SeatFormManager.SeatFormCSVData;

                string errorString = errorChecker.ErrorCheck();

                if (errorString == "")
                {
                    RandomList.CreateStudentList(rosterData.Count);

                    AllSeatData = CreateSeats<SeatItemControl>(seatFormData, this);

                    ContentSizeChange();

                    foreach(SeatItemControl[] horizontalSeats in AllSeatData)
                    {
                        foreach(SeatItemControl seatData in horizontalSeats)
                        {
                            int seatNum = seatData.StudentNumber;
                            if (seatNum > 0)
                            {
                                seatData.isfixed = true;
                                seatData.transform.GetChild(1).gameObject.SetActive(false);
                            }

                            RandomList.DeleteStudentNumber(seatNum);
                        }
                    }
                }
                else
                {
                    errorText.text = errorString;
                    errorArea.SetActive(true);
                }
            }
            else
            {
                errorArea.SetActive(true);
            }
        }


        public void StartChangeSeat()
        {
            int max = AllSeatData.Count;

            int count = 1;

            while(count <= max)
            {
                ChangeSeat(count);

                count++;
            }

            ChangeSeat(0);

            if (configData.hide)
            {
                ActiveHide();
            }
        }

        public void ChangeSeat(int horizontalNumber)
        {
            List<StudentData> changeRoster = new List<StudentData>();

            foreach(StudentData sd in rosterData)
            {
                int studentRestNumber = sd.restrictionNumber;

                if (studentRestNumber <= horizontalNumber && studentRestNumber != 0)
                {
                    changeRoster.Add(sd);
                }

                if (horizontalNumber == 0) 
                {
                    changeRoster.Add(sd);
                }
            }

            List<SeatItemControl> ManOnlySeatList = new List<SeatItemControl>();
            List<SeatItemControl> WomanOnlySeatList = new List<SeatItemControl>();
            List<SeatItemControl> AllOKSeatList = new List<SeatItemControl>();

            List<int> FixedStudentList = new List<int>();

            int seatHoriCount = 1;

            List<SeatItemControl[]> SeatLists = TransformToList<SeatItemControl>();

            foreach (SeatItemControl[] horizontalSeats in SeatLists)
            {
                foreach (SeatItemControl seat in horizontalSeats)
                {
                    if(seat.StudentNumber > 0)
                    {
                        FixedStudentList.Add(seat.StudentNumber);
                    }
                    switch (seat.StudentNumber)
                    {
                        case 0:
                            AllOKSeatList.Add(seat);
                            break;

                        case -1:
                            ManOnlySeatList.Add(seat);
                            break;

                        case -2:
                            WomanOnlySeatList.Add(seat);
                            break;
                    }
                }
                if (seatHoriCount == horizontalNumber)
                {
                    break;
                }
                seatHoriCount++;
            }

            int count = 0;
            int rosterCount = changeRoster.Count;

            while (count < rosterCount)
            {
                //名簿からランダムに生徒を選択
                int rosterRandom = RandomManager.GetRandomNumber(changeRoster);
                StudentData sd = changeRoster[rosterRandom];
                changeRoster.Remove(sd);

                int studentNumber = sd.number;
                bool studentIsMan = sd.isMan;

                bool isFixedStudent = false;

                foreach (int fixedStudent in FixedStudentList)
                {
                    if (studentNumber == fixedStudent)
                    {
                        isFixedStudent = true;
                        break;
                    }
                }

                if (!isFixedStudent)
                {
                    if (studentIsMan)
                    {
                        if (ManOnlySeatList.Count > 0)
                        {
                            int randomNumber = RandomManager.GetRandomNumber(ManOnlySeatList);

                            SeatItemControl sic = ManOnlySeatList[randomNumber];
                            ManOnlySeatList.Remove(sic);

                            sic.StudentNumber = studentNumber;
                        }
                        else
                        {
                            int randomNumber = RandomManager.GetRandomNumber(AllOKSeatList);

                            SeatItemControl sic = AllOKSeatList[randomNumber];
                            AllOKSeatList.Remove(sic);

                            sic.StudentNumber = studentNumber;
                        }
                    }
                    else
                    {
                        if (WomanOnlySeatList.Count > 0)
                        {
                            int randomNumber = RandomManager.GetRandomNumber(WomanOnlySeatList);

                            SeatItemControl sic = WomanOnlySeatList[randomNumber];
                            WomanOnlySeatList.Remove(sic);

                            sic.StudentNumber = studentNumber;
                        }
                        else
                        {
                            int randomNumber = RandomManager.GetRandomNumber(AllOKSeatList);

                            SeatItemControl sic = AllOKSeatList[randomNumber];
                            AllOKSeatList.Remove(sic);

                            sic.StudentNumber = studentNumber;
                        }
                    }
                }

                count++;
            }
        }

        private void ActiveHide()
        {
            randomOpenButton.SetActive(true);

            foreach(SeatItemControl[] horizontal in AllSeatData)
            {
                foreach(SeatItemControl sc in horizontal)
                {
                    if (sc.StudentNumber > 0 && !sc.isfixed)
                    {
                        freeStudents.Add(sc.transform.GetChild(1));
                    }
                }
            }

            RandomList.CreateStudentList(freeStudents.Count);

            if (configData.autoOpen)
            {
                float speed = configData.autoSpeed;
                randomOpenButton.SetActive(false);
                StartCoroutine(AutoRandomOpen(speed));
            }
        }

        public void RandomOpen()
        {
            int random = RandomList.GetRandomStudent();

            if (random != -1)
            {
                freeStudents[random - 1].gameObject.SetActive(false);
            }
            else
            {
                randomOpenButton.SetActive(false);
            }
        }

        public IEnumerator AutoRandomOpen(float speed)
        {
            int random = 0;

            while (random != -1) 
            {
                yield return new WaitForSeconds(speed);

                random = RandomList.GetRandomStudent();

                AutoOpen(random);
            }
        }

        private void AutoOpen(int random)
        {
            if (random != -1)
            {
                freeStudents[random - 1].gameObject.SetActive(false);
            }
            else
            {
                randomOpenButton.SetActive(false);
            }
        }

        public void SaveSeat()
        {
            List<string[]> CSVSeat = new List<string[]>();

            List<SeatItemControl[]> sicList = TransformToList<SeatItemControl>();

            foreach(SeatItemControl[] horizontalSeats in sicList)
            {
                string[] horiData = new string[horizontalSeats.Length];

                int count = 0;
                foreach (SeatItemControl seat in horizontalSeats)
                {
                    if (seat.StudentNumber > 0)
                    {
                        horiData[count] = seat.StudentNumber.ToString();
                    }
                    else
                    {
                        horiData[count] = "0";
                    }

                    count++;
                }

                CSVSeat.Add(horiData);
            }

            CSVManager.CSVWriter.CSVWrite(SeatFilePath, CSVSeat);
        }
    }
}