using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace RandomNominate
{
    public class RandomNominateManager : BaseSeatManager
    {
        [SerializeField]
        private GameObject errorArea = null, nominateButton = null;

        [SerializeField]
        private Text nominateNumberText = null, nominateNameText = null;

        private List<string[]> seatData;

        private List<SeatControl> scList = new List<SeatControl>();


        private void Awake()
        {
            BaseAwake();

            if (File.Exists(ChangeSeat.ChangeSeatManager.SeatFilePath))
            {
                seatData = ChangeSeat.ChangeSeatManager.SeatCSVData;

                List<SeatControl[]> seatControlList = CreateSeats<SeatControl>(seatData, this);

                ContentSizeChange();

                foreach(SeatControl[] horizontal in seatControlList)
                {
                    foreach(SeatControl seatData in horizontal)
                    {
                        if(seatData.StudentNumber > 0)
                        {
                            scList.Add(seatData);
                        }
                    }
                }

                RandomList.CreateStudentList(rosterData.Count);
            }
            else
            {
                nominateButton.SetActive(false);
                errorArea.SetActive(true);
            }
        }

        private int student;

        public void Nominate()
        {
            if (student != 0)
            {
                SeatControl beforesc = scList[student - 1];
                beforesc.AfterNominate();
            }

            student = RandomList.GetRandomStudent();

            if (student != -1)
            {
                SeatControl sc = scList[student - 1];
                sc.Nominate();

                StudentData sd = rosterData[sc.StudentNumber - 1];

                nominateNumberText.text = sd.number.ToString() + "番";
                nominateNameText.text = sd.studentName;
            }
            else
            {
                MyScene.OpenScene(MyScene.SceneName.RandomNominateScene);
            }
        }
    }
}