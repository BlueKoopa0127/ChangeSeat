using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SeatForm
{
    public class SeatControl : BaseSeat
    {
        public override int StudentNumber
        {
            get { return studentNumber; }
            set
            {
                studentNumber = value;

                if(value > 0)
                {
                    SeatColor.color = Color.black;

                    if (manager.configData.display)
                    {
                        NameText.text = value.ToString();
                        NameText.fontSize = 40;
                    }
                    else
                    {
                        NameText.text = manager.rosterData[value - 1].studentName;
                        NameText.fontSize = 30;
                    }
                }

                switch (value)
                {
                    case 0:
                        NameText.text = "";
                        SeatColor.color = Color.black;
                        break;

                    case -1:
                        NameText.text = "";
                        SeatColor.color = Color.blue;
                        break;

                    case -2:
                        NameText.text = "";
                        SeatColor.color = Color.red;
                        break;

                    case -3:
                        NameText.text = "";
                        SeatColor.color = MyColor.HalfClear;
                        break;
                }
            }
        }

        private SeatFormManager sfm;

        [System.NonSerialized]
        public NumberSelect ns;

        private void Start()
        {
            sfm = (SeatFormManager)manager;

            Button button = gameObject.AddComponent<Button>();
            button.onClick.AddListener(Clicked);
        }


        public void Clicked()
        {
            switch (sfm.Mode)
            {
                case SeatFormManager.SelectMode.NoUse:
                    StudentNumber = -3;
                    break;

                case SeatFormManager.SelectMode.Use:
                    StudentNumber = 0;
                    break;

                case SeatFormManager.SelectMode.Man:
                    StudentNumber = -1;
                    break;

                case SeatFormManager.SelectMode.Woman:
                    StudentNumber = -2;
                    break;

                case SeatFormManager.SelectMode.SelectNumber:
                    ns.gameObject.SetActive(true);

                    ns.seatNumber = StudentNumber;

                    ns.sc = this;
                    break;
            }
        }
    }
}