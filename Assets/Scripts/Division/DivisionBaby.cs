using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.math
{
    public class DivisionBaby : MonoBehaviour
    {
        public Transform holder;
        public Baby babypre;

        public bool isSpawn = false;

        public void SpawnBaby(Number number)
        {
            //MyFunction.ClearChild(this.holder);
            //for (int i = 0; i < number.num2; i++)
            //{
            //    Baby baby = Instantiate(this.babypre, holder);
            //    baby.transform.SetParent(holder.transform);
            //    baby.Init(UserManager.instance.IdAvar);
            //}
            if (this.spawn != null)
            {
                StopCoroutine(this.spawn);
            }
            this.spawn = StartCoroutine(SpawnBabyNumber1(number));
        }
        Coroutine spawn;
        IEnumerator SpawnBabyNumber1(Number number)
        {
            this.isSpawn = true;
            MyFunction.ClearChild(this.holder);
            for (int i = 0; i < number.num2; i++)
            {
                Baby baby = Instantiate(this.babypre, holder);
                baby.transform.SetParent(holder.transform);
                baby.Init(UserManager.instance.IdAvar);
                yield return new WaitForSeconds(0.06f);
            }
            isSpawn = false;

        }
    }
}