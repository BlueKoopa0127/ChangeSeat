using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HTUManager : MonoBehaviour
{
    [SerializeField]
    private Text htuText = null;

    [SerializeField]
    private Image htuImage = null;

    [SerializeField]
    private HTUItem[] htuList = null;

    public void HTUStart()
    {
        OpenHTU(MyScene.SceneName.StartScene);
    }
    public void CreateData()
    {
        OpenHTU(MyScene.SceneName.CreateDataScene);
    }
    public void SeatForm()
    {
        OpenHTU(MyScene.SceneName.SeatFormScene);
    }
    public void ChangeSeat()
    {
        OpenHTU(MyScene.SceneName.ChangeSeatScene);
    }
    public void Roster()
    {
        OpenHTU(MyScene.SceneName.RosterScene);
    }
    public void Config()
    {
        OpenHTU(MyScene.SceneName.ConfigScene);
    }
    public void RandomNominate()
    {
        OpenHTU(MyScene.SceneName.RandomNominateScene);
    }
    public void SeatView()
    {
        OpenHTU(MyScene.SceneName.SeatViewScene);
    }


    private void OpenHTU(MyScene.SceneName sceneName)
    {
        foreach (HTUItem htu in htuList)
        {
            if (htu.Scene == sceneName)
            {
                htuText.text = htu.htuText.text;
                htuImage.sprite = htu.screenShot;

                break;
            }
        }
    }
}
