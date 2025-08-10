using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingBackGroundController : MonoBehaviour
{
    [SerializeField] List<Image> listCloud;
    List<Vector3> listOriCloudPos = new List<Vector3>();
   // [SerializeField] GameObject ava;
    private Vector3 originAvarPos;
    void Awake()
    {
        DOTween.SetTweensCapacity(500, 200);
        StartCoroutine(DelayStart());

    }
    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(0.01f);
        Init();
        StartAnimation();
    }
    void StartAnimation()
    { 
        MoveCloudOnSky();
        //ScaleAva();
    }
        public void Init()
    {
        for (int i = 0; i < listCloud.Count; i++)
        {


            listOriCloudPos.Add(listCloud[i].transform.position);
        }
        //this.originAvarPos = this.ava.transform.localScale;
    }
    void MoveCloudOnSky()
    {
        if (listCloud.Count == 0) return;
        for (int i = 0; i < listCloud.Count; i++)
        {
            var cloud = listCloud[i];
            var originY = listOriCloudPos[i].x;
            float valueYRandom = Random.Range(15f, 18f);
            float time = Random.Range(30f, 50f);
            cloud.gameObject.SetActive(true);
            DOTween.Sequence()
                .Append(cloud.transform.DOMoveX(originY + valueYRandom, time).SetEase(Ease.Linear))
                .SetLoops(-1);
        }
    }
    //void ScaleAva()
    //{
    //    DOTween.Sequence()
    //         .Append(this.ava.transform.DOScale(new Vector3(originAvarPos.x + 0.1f, originAvarPos.y + 0.1f, originAvarPos.z + 0.1f), 1f).SetEase(Ease.Linear)).SetDelay(0.5f)
    //         .Append(this.ava.transform.DOScale(originAvarPos, 1f).SetEase(Ease.Linear))
    //         .SetLoops(-1);
    //}
}
