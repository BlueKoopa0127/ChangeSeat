using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomNominate
{
    public class SeatControl : BaseSeat
    {
        public override int StudentNumber
        {
            get { return studentNumber; }
            set
            {
                studentNumber = value;

                if (value > 0)
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
                }
                else
                {
                    NameText.text = "";

                    SeatColor.color = MyColor.HalfClear;
                }
            }
        }

        public void Nominate()
        {
            SeatColor.color = Color.red;
        }

        public void AfterNominate()
        {
            SeatColor.color = MyColor.HalfClear;
        }
    }
}