using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeatView
{
    public class SeatControl : BaseSeat
    {
        [System.NonSerialized]
        public SeatViewManager svm;

        public override int StudentNumber
        {
            get { return studentNumber; }
            set
            {
                studentNumber = value;

                if (value > 0)
                {
                    StudentData sd = manager.rosterData[value - 1];

                    if (manager.configData.display)
                    {
                        NameText.text = value.ToString();
                        NameText.fontSize = 40;
                    }
                    else
                    {
                        NameText.text = sd.studentName;
                        NameText.fontSize = 30;
                    }

                    if (sd.isMan)
                    {
                        SeatColor.color = Color.blue;
                    }
                    else
                    {
                        SeatColor.color = Color.red;
                    }
                }
                else
                {
                    NameText.text = "";
                    SeatColor.color = MyColor.HalfClear;
                }
            }
        }
    }
}