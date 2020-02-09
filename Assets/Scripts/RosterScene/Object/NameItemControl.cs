using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Roster
{
    public class NameItemControl : MonoBehaviour
    {
        [SerializeField]
        private Text numText = null, nameText = null, restText = null;

        [SerializeField]
        private Image isManImage = null;

        private StudentData studentData;
        public StudentData StudentData
        {
            get { return studentData; }
            set
            {
                studentData = value;

                int num = value.number;
                string stu = value.studentName;
                bool isMan = value.isMan;
                int rest = value.restrictionNumber;


                numText.text = num.ToString() + "番";
                nameText.text = stu;

                if (isMan)
                {
                    isManImage.color = MyColor.MyBlue;
                }
                else
                {
                    isManImage.color = MyColor.MyRed;
                }

                if (rest != 0)
                {
                    restText.text = rest.ToString() + "列目";
                }
                else
                {
                    restText.text = "制限なし";
                }
            }
        }

        [System.NonSerialized]
        public RosterEdit editer;

        public void EditItem()
        {
            editer.editArea.SetActive(true);

            editer.selectStudentNIC = this;

            editer.EditStart();
        }
    }
}