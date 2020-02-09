using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using MyJsonManager;

namespace Config
{
    public class ConfigManager : MonoBehaviour
    {
        [SerializeField]
        private Text[] outputTexts = null;

        private bool reverse, hide, display, autoOpen, defaultMan;
        private float autoSpeed;


        private bool Reverse
        {
            get { return reverse; }
            set
            {
                reverse = value;

                if (value)
                {
                    outputTexts[0].text = "On";
                }
                else
                {
                    outputTexts[0].text = "Off";
                }
            }
        }
        private bool Hide
        {
            get { return hide; }
            set
            {
                hide = value;

                if (value)
                {
                    outputTexts[1].text = "On";
                }
                else
                {
                    outputTexts[1].text = "Off";
                }
            }
        }
        private bool Display
        {
            get { return display; }
            set
            {
                display = value;

                if (value)
                {
                    outputTexts[2].text = "番号";
                }
                else
                {
                    outputTexts[2].text = "名前";
                }
            }
        }
        private bool AutoOpen
        {
            get { return autoOpen; }
            set
            {
                autoOpen = value;

                if (value)
                {
                    outputTexts[3].text = "On";
                }
                else
                {
                    outputTexts[3].text = "Off";
                }
            }
        }
        private float AutoSpeed
        {
            get { return autoSpeed; }
            set
            {
                if (value >= 0.1 && value <= 1)
                {
                    float a = (value + 0.01f) * 10f;
                    float b = Mathf.Floor(a);
                    float c = b / 10f;
                    autoSpeed = c;
                    outputTexts[4].text = c.ToString();
                }
            }
        }
        private bool DefaultMan
        {
            get { return defaultMan; }
            set
            {
                defaultMan = value;

                if (value)
                {
                    outputTexts[5].text = "男";
                }
                else
                {
                    outputTexts[5].text = "女";
                }
            }
        }





        public static string ConfigFilePath
        {
            get
            {
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    return Application.persistentDataPath + "/SaveData/ConfigData.json";
                }
                else
                {
                    return Application.dataPath + "/SaveData/ConfigData.json";
                }
            }
        }

        public static ConfigJson GetConfig()
        {
            if (File.Exists(ConfigFilePath))
            {
                ConfigJson jsonData = JsonManager.GetJson<ConfigJson>(ConfigFilePath);
                return jsonData;
            }
            else
            {
                ConfigJson jsonData = new ConfigJson();
                return jsonData;
            }
        }




        private void Awake()
        {
            ConfigJson jsonData = GetConfig();
            Reverse = jsonData.reverse;
            Hide = jsonData.hide;
            Display = jsonData.display;
            AutoOpen = jsonData.autoOpen;
            AutoSpeed = jsonData.autoSpeed;
            DefaultMan = jsonData.defaultMan;
        }

        public void ChangeReverse()
        {
            Reverse = !Reverse;
        }
        public void ChangeHide()
        {
            Hide = !Hide;
        }
        public void ChangeDisplay()
        {
            Display = !Display;
        }
        public void ChangeAutoOpen()
        {
            AutoOpen = !AutoOpen;
        }
        public void ChangeAutoSpeed(bool plus)
        {
            if (plus)
            {
                AutoSpeed += 0.1f;
            }
            else
            {
                AutoSpeed -= 0.1f;
            }
        }
        public void ChangeDefaultMan()
        {
            DefaultMan = !DefaultMan;
        }

        public void SaveConfigData()
        {
            ConfigJson jsonConfig = new ConfigJson(Reverse, Hide, Display, AutoOpen, AutoSpeed, DefaultMan);

            JsonManager.SaveJson(ConfigFilePath, jsonConfig);
        }
    }




    [System.Serializable]
    public class ConfigJson
    {
        public bool reverse, hide, display, autoOpen, defaultMan;
        public float autoSpeed;

        public ConfigJson
            (
            bool Jreverse = false,
            bool Jhide = true,
            bool Jdisplay = true,
            bool JautoOpen = true,
            float JautoSpeed = 0.5f,
            bool JdefaultMan = true
            )

        {
            reverse = Jreverse;
            hide = Jhide;
            display = Jdisplay;
            autoOpen = JautoOpen;
            autoSpeed = JautoSpeed;
            defaultMan = JdefaultMan;
        }
    }
}