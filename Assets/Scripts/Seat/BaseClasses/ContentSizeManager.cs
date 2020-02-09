using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ContentSizeManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform pointer = null, zoomBar = null;

    private float barMax, centerPos, zoomBarY;
    private float max, min;

    private RectTransform contentRect;

    private string ContentFilePath
    {
        get
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return Application.persistentDataPath + "/SaveData/ContentData.json";
            }
            else
            {
                return Application.dataPath + "/SaveData/ContentData.json";
            }
        }
    }

    private float pointerPos;


    private void Start()
    {
        centerPos = Screen.width / 2f;

        float canvasScale = transform.root.localScale.x;

        barMax = ((zoomBar.sizeDelta.x * canvasScale) / 2f);
        zoomBarY = zoomBar.position.y;

        max = barMax + centerPos;
        min = -barMax + centerPos;

        contentRect = GetComponent<RectTransform>();

        if (File.Exists(ContentFilePath))
        {
            pointerPos = MyJsonManager.JsonManager.GetJson<JsonContentSize>(ContentFilePath).pointerPos;

            FirstContentSize();
        }
    }

    public void Draging()
    {
        Vector2 mousePos = Input.mousePosition;

        mousePos.y = zoomBarY;

        if (mousePos.x > max)
        {
            mousePos.x = max;
        }
        else if (mousePos.x < min) 
        {
            mousePos.x = min;
        }

        float scale = (mousePos.x - centerPos) / (max - min);

        ContentScaleChange(1.0f + scale);

        pointer.position = mousePos;
        pointerPos = mousePos.x;
    }

    private void FirstContentSize()
    {
        float scale = (pointerPos - centerPos) / (max - min);

        ContentScaleChange(1.0f + scale);

        pointer.position = new Vector2(pointerPos, zoomBarY);
    }

    private void ContentScaleChange(float scale)
    {
        contentRect.localScale = new Vector3(scale, scale, 1f);
    }

    public void SavePointerPos()
    {
        MyJsonManager.JsonManager.SaveJson(ContentFilePath, new JsonContentSize(pointerPos));
    }
}


public class JsonContentSize
{
    public float pointerPos;

    public JsonContentSize(float pos)
    {
        pointerPos = pos;
    }
}