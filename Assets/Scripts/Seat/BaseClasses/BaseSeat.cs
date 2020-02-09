using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSeat : MonoBehaviour
{
    protected Text NameText
    {
        get { return transform.GetChild(0).GetComponent<Text>(); }
    }

    protected Image SeatColor
    {
        get { return GetComponent<Image>(); }
    }

    public BaseSeatManager manager;

    protected int studentNumber;
    public virtual int StudentNumber
    {
        get { return studentNumber; }
        set
        {
            studentNumber = value;
        }
    }
}
