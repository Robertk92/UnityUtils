
using UnityEngine;

namespace GameFramework
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Get
        {
            get
            {
                
                if (instance == null)
                {
                    T[] found = FindObjectsOfType<T>();
                    Debug.AssertFormat(found.Length <= 1, 
                        string.Format("Multiple singletons of type '{0}' found", typeof(T).Name));

                    if (found.Length == 1)
                    {
                        instance = found[0];
                    }
                    else
                    {
                        GameObject singletonGo = new GameObject(typeof(T).Name);
                        DontDestroyOnLoad(singletonGo);
                        instance = singletonGo.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

    }
}

