using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangeSeat
{
    public class SeatItemControl : BaseSeat
    {
        private GameObject Hide
        {
            get { return transform.GetChild(1).gameObject; }
        }

        public override int StudentNumber
        {
            get { return studentNumber; }
            set
            {
                studentNumber = value;

                if(value > 0)
                {
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

                    if (manager.configData.hide)
                    {
                        Hide.SetActive(!isfixed);
                    }
                    else
                    {
                        Hide.SetActive(false);
                    }

                    if (manager.rosterData[value - 1].isMan)
                    {
                        SeatColor.color = Color.blue;
                    }
                    else
                    {
                        SeatColor.color = Color.red;
                    }
                }
                else switch (value)
                {
                    case 0:
                        NameText.text = "";

                        SeatColor.color = Color.black;

                        Hide.SetActive(manager.configData.hide);
                        break;

                    case -1:
                        NameText.text = "";

                        SeatColor.color = Color.blue;

                        Hide.SetActive(manager.configData.hide);
                        break;

                    case -2:
                        NameText.text = "";

                        SeatColor.color = Color.red;

                        Hide.SetActive(manager.configData.hide);
                        break;

                    case -3:
                        NameText.text = "";

                        SeatColor.color = MyColor.HalfClear;

                        Hide.SetActive(false);
                        break;
                }
            }
        }

        [System.NonSerialized]
        public bool isfixed;
    }
}