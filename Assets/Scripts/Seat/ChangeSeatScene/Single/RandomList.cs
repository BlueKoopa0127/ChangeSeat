using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomList : MonoBehaviour
{
    public static List<int> StudentList;

    public static void CreateStudentList(int num)
    {
        StudentList = new List<int>();

        for (int n = 1; n <= num; n++) 
        {
            StudentList.Add(n);
        }
    }

    public static void DeleteStudentNumber(int num)
    {
        if (num > 0)
        {
            StudentList.Remove(num);
        }
    }

    public static int GetRandomStudent()
    {
        if (StudentList.Count > 0)
        {
            int max = StudentList.Count;

            int randomNumber = Random.Range(0, max);

            int student = StudentList[randomNumber];

            StudentList.RemoveAt(randomNumber);

            return student;
        }

        return -1;
    }
}
