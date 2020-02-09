using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "How to Use/HTUItem",fileName ="HTUItem")]
public class HTUItem : ScriptableObject
{
    public MyScene.SceneName Scene;
    public TextAsset htuText;
    public Sprite screenShot;
}
