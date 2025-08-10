using Rubik.ClawMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public List<AvaHomeMove> lsavaHomeMoves;
    Coroutine coroutineMove;
    private void Start()
    {
        SetOriStatus();
        // RandomBabyMove();
        if (coroutineMove != null)
        {
            StopCoroutine(coroutineMove);
        }
        this.coroutineMove = StartCoroutine("MoveBaby");
    }
    private void OnDisable()
    {
        if (coroutineMove != null)
        {
            StopCoroutine(coroutineMove);
        }
    }
    public void SetOriStatus()
    {
        if (this.lsavaHomeMoves == null || this.lsavaHomeMoves.Count <= 0) return;
        for (int i = 0; i < this.lsavaHomeMoves.Count; i++) 
        {
            this.lsavaHomeMoves[i].SetOriStatus();
        }
    }
    [ContextMenu("RandomBabyMove")]
    public void RandomBabyMove()
    {
        if (this.lsavaHomeMoves == null || this.lsavaHomeMoves.Count <= 0) return;
        int number = Random.Range(0, this.lsavaHomeMoves.Count);
        Debug.Log(number);
        this.lsavaHomeMoves[number].MoveToEndPos();
    }
    IEnumerator MoveBaby()
    {
        if (this.lsavaHomeMoves == null || this.lsavaHomeMoves.Count == 0)
            yield break; // hoặc Debug.LogWarning để biết danh sách rỗng

        int number = Random.Range(0, this.lsavaHomeMoves.Count);
       // Debug.Log(number);
        this.lsavaHomeMoves[number].MoveToEndPos();
        //yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => this.lsavaHomeMoves[number].isDone == false);
        //yield return new WaitForSeconds(0.5f);
        StartCoroutine(MoveBaby());
    }
}
