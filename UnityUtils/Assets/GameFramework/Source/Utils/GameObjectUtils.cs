using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameFramework
{
    public static class GameObjectUtils
    {
        public static void EnsureSingleInstance<T>() where T : MonoBehaviour
        {
            EnsureMaxNumInstances<T>(1);
        }

        public static void EnsureNoInstance<T>() where T : MonoBehaviour
        {
            EnsureMaxNumInstances<T>(0);
        }

        public static void EnsureMaxNumInstances<T>(int maxNum) where T : MonoBehaviour
        {
            T[] instances = Object.FindObjectsOfType<T>();
            if (instances.Length > maxNum)
            {
                Debug.LogError($"{instances.Length - maxNum} Illegal instance(s) found of type {typeof(T).Name} " +
                               $"while trying to ensure max {maxNum} instances.");
                Debug.Break();
            }
        }
    }
}
