using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public static class GeneralUtility
    {
        public static bool GenerateRandomChance(float successRate)
        {
            return Random.Range(0f, 1f) < successRate;
        }

        public static void ShuffleArray<T> (ref T[] array) 
        {
            for (int n = array.Length - 1; n > 0; n--)
            {
                int k = Random.Range(0, n);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        public static void ShuffleList<T> (List<T> list)
        {
            for (int n = list.Count - 1; n > 0; n--)
            {
                int k = Random.Range(0, n);
                T temp = list[n];
                list[n] = list[k];
                list[k] = temp;
            }
        }

        public static bool IsOnScreen(GameObject obj)
        {
            Camera mainCamera = Camera.main;
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(obj.transform.position);
            return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        }

        public static string FloatToPercentString(float ratio)
        {
            return $"{Mathf.Abs((ratio - 1f) * 100):0}%";
        }
    }
}
