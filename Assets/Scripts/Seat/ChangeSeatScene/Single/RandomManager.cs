using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangeSeat
{
    public class RandomManager
    {
        public static int GetRandomNumber<T>(List<T> list)
        {
            int count = list.Count;

            int randomNumber = Random.Range(0, count);

            return randomNumber;
        }
    }
}