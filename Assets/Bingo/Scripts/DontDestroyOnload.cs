using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Rubik.BinGo
{
    public class DontDestroyOnload : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

    }
}
