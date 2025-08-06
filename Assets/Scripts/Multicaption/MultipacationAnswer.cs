using Rubik.math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipacationAnswer : MonoBehaviour
{
    [SerializeField] private Baby babypre;
    [SerializeField] private Transform holder;
   // public int numberBaby;
    Coroutine spawnBaby;
    public void ClearBaby()
    {
        MyFunction.ClearChild(this.holder);
    }

    public void SpawnBaby(int numberBaby)
    {
        MyFunction.ClearChild(this.holder);
        //if (this.spawnBaby != null)
        //{
        //    StopCoroutine(Spawn(numberBaby));
        //}
        //this.spawnBaby = StartCoroutine(Spawn(numberBaby));
        for (int i = 0; i < numberBaby; i++)
        {
            Baby baby = Instantiate(babypre);
            baby.transform.localScale = Vector3.one;
            baby.transform.SetParent(this.holder, false);
            baby.Init(UserManager.instance.IdAvar);
            //yield return new WaitForSeconds(0.01f);
        }

    }

    IEnumerator Spawn(int numberBaby)
    {
        for (int i = 0; i < numberBaby; i++)
        {
            Baby baby = Instantiate(babypre);
            baby.transform.localScale = Vector3.one;
            baby.transform.SetParent(this.holder, false);
            baby.Init(UserManager.instance.IdAvar);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
