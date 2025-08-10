using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

namespace Rubik.ClawMachine
{
    public class BuildingButtonController : MonoBehaviour
    {
        [SerializeField] List<GameObject> listButton, ListBtnBonous;
        [SerializeField] private GameObject avartar,btnPlay, icoinAdd;
        private List<Vector3> listOriginButtonPos = new List<Vector3>();
        private List<Vector3> listOriginButtonRankPos = new List<Vector3>();
        private Vector3 originAvarPos, originPlayBtnPos, origisBtnAddPos;
        [SerializeField] private Image imageIconSetting;
        //[SerializeField] private GameObject icounBonous;
        [SerializeField] GameObject kimdongho;
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
            ScaleButton();
           // ScaleAvar();
            ScaleBtnAdd();
            ScaleButtonPlay();
            RoteIcoinSetting();
            //ScaleIcoinBonous();
            Rotate360();
            //RotateIcoinBonous();
        }

        public void Init()
        {
            for (int i = 0; i < listButton.Count; i++)
            {
                listOriginButtonPos.Add(listButton[i].transform.localScale);
            }
            for (int i = 0; i < ListBtnBonous.Count; i++)
            {
                listOriginButtonRankPos.Add(ListBtnBonous[i].transform.localScale);
            }
            this.originAvarPos = this.avartar.transform.localScale;
            this.originPlayBtnPos =  this.btnPlay.transform.localScale;
            this.origisBtnAddPos = this.icoinAdd.transform.localScale;
        }
        private void Start()
        {
            //ScaleButton();
        }
        public void ScaleButton()
        {
            //if (listButton.Count == 0) return;
            //ShuffleList(listButton);
            //for (int i = 0; i < listButton.Count; i++)
            //{
            //    var cloud = listButton[i];
            //    var origin = listOriginButtonPos[i];
            //    float time = Random.Range(0.08f, 0.2f);

            //    cloud.transform.localScale = Vector3.zero;
            //    cloud.gameObject.SetActive(true);
            //    DOTween.Sequence()
            //      .Append(cloud.transform.DOScale(new Vector3(origin.x + 0.2f, origin.y + 0.2f, origin.z + 0.2f), 0.5f).SetEase(Ease.Linear))
            //    .Append(cloud.transform.DOScale(new Vector3(origin.x, origin.y, origin.z), 0.2f).SetEase(Ease.Linear));
            //}

            if (scaleButton != null) StopCoroutine(scaleButton);
            scaleButton = StartCoroutine(StartScaleButton());
        }

        Coroutine scaleButton;
        IEnumerator StartScaleButton()
        {
            if (listButton.Count == 0) yield break;
            ShuffleList(listButton);
            for (int i = 0; i < listButton.Count; i++)
            {
                var cloud = listButton[i];
                var origin = listOriginButtonPos[i];
                //float time = Random.Range(0.08f, 0.2f);

                cloud.transform.localScale = Vector3.zero;
                cloud.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.05f);
                DOTween.Sequence()
                  .Append(cloud.transform.DOScale(new Vector3(origin.x + 0.2f, origin.y + 0.2f, origin.z + 0.2f), 0.5f).SetEase(Ease.Linear))
                .Append(cloud.transform.DOScale(new Vector3(origin.x, origin.y, origin.z), 0.2f).SetEase(Ease.Linear));
            }

        }
        void ShuffleList<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                T temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
        private void ScaleAvar()
        {
            DOTween.Sequence()
            .Append(this.avartar.transform.DOScale(new Vector3(originAvarPos.x + 0.2f, originAvarPos.y + 0.2f, originAvarPos.z + 0.2f), 1f).SetEase(Ease.Linear)).SetDelay(0.5f)
            .Append(this.avartar.transform.DOScale(originAvarPos, 1f).SetEase(Ease.Linear))
            .SetLoops(-1);

        }
        private void ScaleBtnAdd()
        {
            //DOTween.Sequence()
            //.Append(this.icoinAdd.transform.DOScale(new Vector3(origisBtnAddPos.x + 0.1f, origisBtnAddPos.y + 0.1f, origisBtnAddPos.z + 0.1f), 1f).SetEase(Ease.Linear)).SetDelay(0.5f)
            //.Append(this.icoinAdd.transform.DOScale(originAvarPos, 1f).SetEase(Ease.Linear))
            //.SetLoops(-1);
            DOTween.Sequence()
            .Append(this.icoinAdd.transform.DOScale(new Vector3(origisBtnAddPos.x + 0.08f, origisBtnAddPos.y + 0.08f, origisBtnAddPos.z + 0.08f), 0.8f).SetEase(Ease.Linear))
            .Append(this.icoinAdd.transform.DOScale(origisBtnAddPos, 0.8f).SetEase(Ease.Linear))
            .SetLoops(-1);
        }
        public void ScaleButtonPlay()
        {
            DOTween.Sequence()
           .Append(this.btnPlay.transform.DOScale(new Vector3(originAvarPos.x + 0.2f, originAvarPos.y + 0.2f, originAvarPos.z + 0.2f), 1f).SetEase(Ease.Linear)).SetDelay(0.5f)
           .Append(this.btnPlay.transform.DOScale(originAvarPos, 1f).SetEase(Ease.Linear))
           .SetLoops(-1);
        }
        private void RoteIcoinSetting()
        {

            Vector3 rotate = (Random.Range(0, 2) == 0) ? ((Random.Range(0, 2) == 0) ? new Vector3(0, 0, 360) : new Vector3(0, 0, -360)) : ((Random.Range(0, 2) == 0) ? new Vector3(0, 0, 360) : new Vector3(0, 0, -360));
            this.imageIconSetting.transform.DORotate(rotate, 5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        }
        private void ScaleIcoinBonous()
        {
            if (ListBtnBonous.Count == 0) return;
            for (int i = 0; i < ListBtnBonous.Count; i++)
            {
                var cloud = ListBtnBonous[i];
                var originY = listOriginButtonRankPos[i];
                float time = Random.Range(0.8f, 1.2f);
                cloud.gameObject.SetActive(true);
                DOTween.Sequence()
                    .Append(cloud.transform.DOScale(new Vector3(originY.x + 0.2f, originY.y + 0.2f, originY.z + 0.2f), time).SetEase(Ease.Linear))
                    .Append(cloud.transform.DOScale(originY, time).SetEase(Ease.Linear))
            .SetLoops(-1);
            }
        }
        //private void RotateIcoinBonous()
        //{
        //    DOTween.Sequence()
        //           .Append(this.icounBonous.transform.DORotate(new Vector3(0, 0, 5f), 0.5f, RotateMode.LocalAxisAdd)
        //               .SetEase(Ease.Linear)
        //                .SetRelative(true))
        //           .Append(this.icounBonous.transform.DORotate(new Vector3(0, 0, -5f), 0.5f, RotateMode.LocalAxisAdd)
        //               .SetEase(Ease.Linear)
        //                .SetRelative(true))
        //               .SetLoops(-1);

        //}
        void Rotate360()
        {
            kimdongho.transform.rotation = Quaternion.Euler(0, 0, 0);
            kimdongho.transform.DORotate(new Vector3(0, 0, -360), 6f, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart);
        }
    }


}