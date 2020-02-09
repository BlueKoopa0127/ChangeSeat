using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace SeatForm
{
    public class SeatFormManager : BaseSeatManager
    {
        public enum SelectMode
        {
            Use,
            NoUse,
            Man,
            Woman,
            SelectNumber
        }

        private SelectMode mode;
        public SelectMode Mode
        {
            get { return mode; }
            set
            {
                mode = value;
                switch (value)
                {
                    case SelectMode.Use:
                        modeButtonText.text = "使用する席";
                        break;

                    case SelectMode.NoUse:
                        modeButtonText.text = "使用しない席";
                        break;

                    case SelectMode.Man:
                        modeButtonText.text = "男の席";
                        break;

                    case SelectMode.Woman:
                        modeButtonText.text = "女の席";
                        break;

                    case SelectMode.SelectNumber:
                        modeButtonText.text = "選択モード";
                        break;
                }
            }
        }

        [SerializeField]
        private Text horiText = null, verText = null;

        public NumberSelect numberSelect = null;

        [SerializeField]
        private GameObject errorObj = null;

        [SerializeField]
        private Text errorText = null;

        [SerializeField]
        private Text modeButtonText = null;

        private int horiNum, verNum;
        private int HoriNum
        {
            get { return horiNum; }
            set
            {
                if (value >= 0)
                {
                    horiNum = value;
                    horiText.text = value.ToString();
                }
            }
        }
        private int VerNum
        {
            get { return verNum; }
            set
            {
                if (value >= 0)
                {
                    verNum = value;
                    verText.text = value.ToString();
                }
            }
        }

        public static string SeatFormPath
        {
            get
            {
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    return Application.persistentDataPath + "/SaveData/SeatForm.csv";
                }
                else
                {
                    return Application.dataPath + "/SaveData/SeatForm.csv";
                }
            }
        }

        public static List<string[]> SeatFormCSVData
        {
            get
            {
                if (Config.ConfigManager.GetConfig().reverse)
                {
                    List<string[]> csvData = CSVManager.CSVReader.CSVRead(SeatFormPath);

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
                    return CSVManager.CSVReader.CSVRead(SeatFormPath);
                }
            }
        }




        private void Awake()
        {
            BaseAwake();

            Mode = SelectMode.Use;

            if (File.Exists(SeatFormPath))
            {
                List<SeatControl[]> scList = CreateSeats<SeatControl>(SeatFormCSVData, this);

                ContentSizeChange();

                foreach(SeatControl[] horizontalData in scList)
                {
                    foreach(SeatControl scData in horizontalData)
                    {
                        scData.ns = numberSelect;
                    }
                }

                HoriNum = transform.childCount;
                VerNum = transform.GetChild(0).childCount;
            }
            else
            {
                HoriNum = 0;
                VerNum = 0;
            }
        }

        public void AddHorizontalObject()
        {
                Transform hori = Instantiate(horizontalObject, transform).transform;

                int count = 1;

                while (count <= VerNum)
                {
                    CreateSeat(hori);

                    count++;
                }

                ChangeHorizontal(true);
        }

        public void DeleteHorizontal()
        {
            if (HoriNum > 0)
            {
                Transform lastChild = transform.GetChild(transform.childCount - 1);
                Destroy(lastChild.gameObject);

                ChangeHorizontal(false);
            }
        }

        private void ChangeHorizontal(bool plus)
        {
            if (plus)
            {
                HoriNum++;
            }
            else
            {
                HoriNum--;
            }

            ContentSizeChange();
        }



        public void AddVertical()
        {
                ChangeVertical(true);

                foreach (Transform hori in transform)
                {
                    CreateSeat(hori);
                }
        }

        public void DeleteVertical()
        {
            if (verNum > 0)
            {
                ChangeVertical(false);

                foreach (Transform hori in transform)
                {
                    Transform lastChild = hori.GetChild(hori.childCount - 1);
                    Destroy(lastChild.gameObject);
                }
            }
        }

        private void ChangeVertical(bool plus)
        {
            if (plus)
            {
                VerNum++;
            }
            else
            {
                VerNum--;
            }

            ContentSizeChange(HoriNum, VerNum);
        }

        private Transform CreateSeat(Transform hori, int num = 0)
        {
            GameObject obj = Instantiate(seatObject, hori);
            SeatControl sc = obj.AddComponent<SeatControl>();
            sc.manager = this;
            sc.StudentNumber = num;
            sc.ns = numberSelect;

            return obj.transform;
        }

        public void SaveSeatForm()
        {
            List<string[]> csvSeatForm = new List<string[]>();

            List<SeatControl[]> scList = TransformToList<SeatControl>();

            List<int> errorCheckList = new List<int>();

            int seatCount = 0;

            foreach(SeatControl[] horizontalSeats in scList)
            {
                int count = 0;

                string[] horizontalData = new string[horizontalSeats.Length];

                foreach (SeatControl seat in horizontalSeats)
                {
                    if (seat.StudentNumber >= -2)
                    {
                        seatCount++;
                    }

                    if (seat.StudentNumber > 0)
                    {
                        errorCheckList.Add(seat.StudentNumber);
                    }

                    horizontalData[count] = seat.StudentNumber.ToString();

                    count++;
                }

                csvSeatForm.Add(horizontalData);
            }

            bool error = false;

            foreach(int n in errorCheckList)
            {
                int count = 0;

                foreach(int nn in errorCheckList)
                {
                    if(n == nn)
                    {
                        count++;
                    }
                }

                if(count >= 2)
                {
                    error = true;
                }
            }

            if (error)
            {
                errorObj.SetActive(true);
                errorText.text = "保存できませんでした\n\n" + "同じ人を二箇所に固定することはできません";
            }
            else if (seatCount != rosterData.Count)
            {
                errorObj.SetActive(true);
                if (seatCount > rosterData.Count)
                {
                    CSVManager.CSVWriter.CSVWrite(SeatFormPath, csvSeatForm);
                    errorText.text = "保存しました\n\n" + "席の数が" + (seatCount - rosterData.Count).ToString() + "個多いです";
                }
                else
                {
                    errorText.text = "保存できませんでした\n\n" + "席の数が" + (rosterData.Count - seatCount).ToString() + "個少ないです";
                }
            }
            else
            {
                CSVManager.CSVWriter.CSVWrite(SeatFormPath, csvSeatForm);
                MyScene.OpenScene(MyScene.SceneName.CreateDataScene);
            }
        }

        public void ChangeMode()
        {
            switch (Mode)
            {
                case SelectMode.Use:
                    Mode = SelectMode.NoUse;
                    break;

                case SelectMode.NoUse:
                    Mode = SelectMode.Man;
                    break;

                case SelectMode.Man:
                    Mode = SelectMode.Woman;
                    break;

                case SelectMode.Woman:
                    Mode = SelectMode.SelectNumber;
                    break;

                case SelectMode.SelectNumber:
                    Mode = SelectMode.Use;
                    break;
            }
        }
    }
}