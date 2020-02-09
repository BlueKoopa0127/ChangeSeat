using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUI
{
    public class RandomNominateUI : MonoBehaviour
    {
        [SerializeField]
        private RectTransform numberRect = null, nameRect = null;

        private void Start()
        {
            Vector2 parentSize = transform.root.GetComponent<RectTransform>().rect.size;
            float harfSize = parentSize.x / 2f;

            Vector2 numberPos = numberRect.localPosition;
            numberPos.x = -harfSize;
            numberRect.localPosition = numberPos;

            Vector2 numberSize = numberRect.rect.size;
            numberSize.x = harfSize;
            numberRect.sizeDelta = numberSize;


            Vector2 namePos = nameRect.localPosition;
            namePos.x = 0;
            nameRect.localPosition = namePos;

            Vector2 nameSize = nameRect.rect.size;
            nameSize.x = harfSize;
            nameRect.sizeDelta = nameSize;
        }
    }
}