
using UnityEngine;

namespace Utils.Generic
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
                    T[] found = Object.FindObjectsOfType<T>();
                    Debug.AssertFormat(found.Length <= 1, string.Format("Multiple singletons of type '{0}' found", typeof(T).Name));
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

