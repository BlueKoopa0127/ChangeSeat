using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace Roster
{
    public class RosterManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject nameItemObj = null;

        [SerializeField]
        private Text countText = null;

        public GameObject editArea;

        [System.NonSerialized]
        public NameItemControl selectStudentNIC;

        private bool defaultMan;

        private RectTransform content;
        private float canvasSize;

        [SerializeField]
        private RosterEdit editer = null;

        private int studentCount;
        private int StudentCount
        {
            get { return studentCount; }
            set
            {
                if (value >= 0)
                {
                    studentCount = value;
                    countText.text = value.ToString();
                }
            }
        }

        public static string RosterFilePath
        {
            get
            {
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    return Application.persistentDataPath + "/SaveData/RosterData.csv";
                }
                else
                {
                    return Application.dataPath + "/SaveData/RosterData.csv";
                }
            }
        }

        public static List<StudentData> RosterCSVData
        {
            get
            {
                List<string[]> CSVData = CSVManager.CSVReader.CSVRead(RosterFilePath);
                List<StudentData> StudentList = new List<StudentData>();

                foreach(string[] student in CSVData)
                {
                    StudentData sd = new StudentData(student);
                    StudentList.Add(sd);
                }


                return StudentList;
            }
        }



        private void Awake()
        {
            content = transform.parent.GetComponent<RectTransform>();
            canvasSize = transform.root.GetComponent<RectTransform>().rect.size.x;

            defaultMan = Config.ConfigManager.GetConfig().defaultMan;

            if (File.Exists(RosterFilePath))
            {
                List<StudentData> RosterData = RosterCSVData;

                CSVStart(RosterData);
            }
            else
            {
                StudentCount = 0;
            }
        }

        public void AddNameItem()
        {
            GameObject instance = Instantiate(nameItemObj, transform);

            NameItemControl nic = instance.GetComponent<NameItemControl>();
            nic.StudentData = new StudentData(StudentCount + 1, "", defaultMan, 0);
            nic.editer = editer;

            ChangeNumber(true);
        }

        private void AddName(StudentData nameData)
        {
            GameObject nameObj = Instantiate(nameItemObj, transform);

            RectTransform nameRect = nameObj.GetComponent<RectTransform>();
            Vector2 size = nameRect.rect.size;
            size.x = canvasSize - 20;
            nameRect.sizeDelta = size;

            NameItemControl nic = nameObj.GetComponent<NameItemControl>();
            nic.StudentData = nameData;
            nic.editer = editer;

            ChangeNumber(true);
        }

        public void DeleteName()
        {
            if (transform.childCount >= 1)
            {
                Transform lastChild = transform.GetChild(transform.childCount - 1);
                Destroy(lastChild.gameObject);

                ChangeNumber(false);
            }
        }

        private void ChangeNumber(bool plus)
        {
            if (plus)
            {
                StudentCount++;
            }
            else
            {
                StudentCount--;
            }

            Vector2 size = content.sizeDelta;
            size.y = 100 + (90 * (StudentCount - 1));
            content.sizeDelta = size;
        }

        public void SaveRoster()
        {
            List<string[]> RosterCSV = new List<string[]>();

            foreach (Transform nameItem in transform)
            {
                NameItemControl nic = nameItem.GetComponent<NameItemControl>();

                string[] nameData = nic.StudentData.saveData;

                RosterCSV.Add(nameData);
            }

            CSVManager.CSVWriter.CSVWrite(RosterFilePath, RosterCSV);
        }

        private void CSVStart(List<StudentData> CSVData)
        {
            foreach (StudentData data in CSVData)
            {
                AddName(data);
            }
        }
    }
}

public struct StudentData
{
    public int number;
    public string studentName;
    public bool isMan;
    public int restrictionNumber;

    public StudentData(int Number, string StudentName, bool IsMan, int RestrictionNumber)
    {
        number = Number;
        studentName = StudentName;
        isMan = IsMan;
        restrictionNumber = RestrictionNumber;
    }
    public StudentData(string[] data)
    {
        number = int.Parse(data[0]);
        studentName = data[1];
        isMan = bool.Parse(data[2]);
        restrictionNumber = int.Parse(data[3]);
    }


    public string[] saveData
    {
        get
        {
            string[] allData = new string[4];

            allData[0] = number.ToString();
            allData[1] = studentName;
            allData[2] = isMan.ToString();
            allData[3] = restrictionNumber.ToString();

            return allData;
        }
    }
}