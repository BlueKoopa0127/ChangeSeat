using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Roster
{
    public class RosterEdit : MonoBehaviour
    {
        [SerializeField]
        private InputField studentNameField = null;

        [SerializeField]
        private Text restNumberText = null;

        [SerializeField]
        private GameObject restObj = null, noRestObj = null;

        [SerializeField]
        private Image changeSexButton = null;


        [System.NonSerialized]
        public NameItemControl selectStudentNIC;

        private int editRestNumber;
        private int EditRestNumber
        {
            get { return editRestNumber; }
            set
            {
                if (value >= 1 && value <= 10) 
                {
                    editRestNumber = value;
                    restNumberText.text = value.ToString();
                }
            }
        }

        private StudentData editData;

        public GameObject editArea;

        public void EditStart()
        {
            editData = selectStudentNIC.StudentData;

            studentNameField.text = editData.studentName;

            SexColorChange();

            if (editData.restrictionNumber != 0)
            {
                ChangeRest(true);
                EditRestNumber = editData.restrictionNumber;
            }
            else
            {
                ChangeRest(false);
            }
        }

        public void ChangeSex()
        {
            editData.isMan = !editData.isMan;
            SexColorChange();
        }
        private void SexColorChange()
        {
            if (editData.isMan)
            {
                changeSexButton.color = MyColor.MyBlue;
            }
            else
            {
                changeSexButton.color = MyColor.MyRed;
            }
        }

        public void ChangeRestriction()
        {
            ChangeRest(!restObj.activeSelf);
        }

        private void ChangeRest(bool toRest)
        {
            restObj.SetActive(toRest);

            if (!toRest)
            {
                editRestNumber = 0;
            }
            else
            {
                EditRestNumber = 1;
            }

            noRestObj.SetActive(!toRest);
        }

        public void ChangeRestNumber(bool plus)
        {
            if (plus)
            {
                EditRestNumber++;
            }
            else
            {
                EditRestNumber--;
            }
        }

        public void CancelEdit()
        {
            Close();
        }

        public void EditName()
        {
            editData.studentName = studentNameField.text;
            editData.restrictionNumber = EditRestNumber;

            selectStudentNIC.StudentData = editData;

            Close();
        }

        private void Close()
        {
            studentNameField.text = "";
            EditRestNumber = 1;

            editArea.SetActive(false);
        }
    }
}