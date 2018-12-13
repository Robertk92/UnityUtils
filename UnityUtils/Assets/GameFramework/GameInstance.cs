using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public class GameInstance : Singleton<GameInstance>
    {
        private void Awake()
        {
            TestCls test = FindObjectOfType<TestCls>();
            Debug.Log(test.name);
        }
    }
}
