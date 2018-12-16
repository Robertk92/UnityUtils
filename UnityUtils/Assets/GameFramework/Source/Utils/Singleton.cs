
using UnityEngine;

namespace GameFramework
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                
                if (_instance == null)
                {
                    T[] found = FindObjectsOfType<T>();
                    Debug.AssertFormat(found.Length <= 1, $"Multiple singletons of type '{typeof(T).Name}' found");

                    if (found.Length == 1)
                    {
                        _instance = found[0];
                    }
                    else
                    {
                        GameObject singletonGo = new GameObject(typeof(T).Name);
                        DontDestroyOnLoad(singletonGo);
                        _instance = singletonGo.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

    }
}

