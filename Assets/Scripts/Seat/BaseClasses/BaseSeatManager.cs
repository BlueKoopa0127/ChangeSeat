using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseSeatManager : MonoBehaviour
{
    [SerializeField]
    protected GameObject horizontalObject = null, seatObject = null;

    [NonSerialized]
    public List<StudentData> rosterData;

    [NonSerialized]
    public Config.ConfigJson configData;

    protected void BaseAwake()
    {
        rosterData = Roster.RosterManager.RosterCSVData;
        configData = Config.ConfigManager.GetConfig();
    }

    protected List<SeatControlClass[]> TransformToList<SeatControlClass>() where SeatControlClass : BaseSeat
    {
        List<SeatControlClass[]> AllSeatData = new List<SeatControlClass[]>();

        if (Config.ConfigManager.GetConfig().reverse)
        {
            for (int horiCount = transform.childCount - 1; horiCount >= 0; horiCount--)
            {
                Transform horiTra = transform.GetChild(horiCount);

                SeatControlClass[] horiData = new SeatControlClass[horiTra.childCount];

                int count = 0;

                for (int verCount = horiTra.childCount - 1; verCount >= 0; verCount--)
                {
                    Transform seat = horiTra.GetChild(verCount);
                    SeatControlClass sc = seat.GetComponent<SeatControlClass>();

                    horiData[count] = sc;

                    count++;
                }

                AllSeatData.Add(horiData);
            }
        }
        else
        {
            foreach (Transform horiTra in transform)
            {
                SeatControlClass[] horiData = new SeatControlClass[transform.GetChild(0).childCount];

                int count = 0;

                foreach (Transform seatTra in horiTra)
                {
                    SeatControlClass sc = seatTra.GetComponent<SeatControlClass>();

                    horiData[count] = sc;

                    count++;
                }

                AllSeatData.Add(horiData);
            }
        }

        return AllSeatData;
    }


    protected List<AddClass[]> CreateSeats<AddClass>(List<string[]> seatFormData, BaseSeatManager manager) where AddClass : BaseSeat
    {
        Type type = typeof(AddClass);

        List<AddClass[]> scList = new List<AddClass[]>();

        foreach (string[] horizontalData in seatFormData)
        {
            Transform horiTra = Instantiate(horizontalObject, transform).transform;

            AddClass[] adds = new AddClass[horizontalData.Length];

            int count = 0;
            foreach (string seatData in horizontalData)
            {
                GameObject seat = Instantiate(seatObject, horiTra);

                Component component = seat.AddComponent(type);

                BaseSeat seatControl = component.GetComponent<AddClass>();

                seatControl.manager = manager;

                seatControl.StudentNumber = int.Parse(seatData);

                adds[count] = (AddClass)seatControl;

                count++;
            }

            scList.Add(adds);
        }

        return scList;
    }


    protected void ContentSizeChange(int horiCount = -1, int verCount = -1)
    {
        if(horiCount == -1)
        {
            horiCount = transform.childCount;
        }

        if(verCount == -1)
        {
            verCount = transform.GetChild(0).childCount;
        }

        RectTransform contentRect = transform.parent.GetComponent<RectTransform>();

        float space = 20f;

        Vector2 size = new Vector2(-space, -space);

        size.x += (verCount * (100f + space));
        size.y += (horiCount * (100f + space));

        contentRect.sizeDelta = size;

        Vector2 horiSize = new Vector2(size.x, 100f);

        foreach (Transform horiTra in transform)
        {
            RectTransform horiRect = horiTra.GetComponent<RectTransform>();

            horiRect.sizeDelta = horiSize;
        }
    }
}
