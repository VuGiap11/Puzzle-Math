using DG.Tweening;
using NTPackage.UI;
using Rubik.LuckyGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Rubik.ClawMachine
{
    public class BonousPanel : PopupUI
    {
        // public List<GameObject> Golds;
        public Transform targetPos;
        // List<Vector3> GoldsPos = new List<Vector3>();
        [SerializeField] private List<Transform> goldPositions;
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private GameObject gold;
        [SerializeField] private Transform holder, starSpawn;
        public GameObject Notice;
        public bool canClick;
        //DG.Tweening.Sequence sequence;
        private void Awake()
        {
            //for (int i = 0; i < Golds.Count; i++)
            //{


            //    GoldsPos.Add(Golds[i].transform.position);
            //}
        }

        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            this.goldText.text = UserManager.instance.useData.gold.ToString();
            //ScaleBtn();
            this.Notice.SetActive(false);
            this.canClick = false;
            this.ismove = false;
        }

        public override void OffUI()
        {
            base.OffUI();
            //this.canClick = false;
        }
        public void OffBonous()
        {
            LuckyGameManager.Instance.isCanSpin = false;

            this.OffUI();
        }
        public void ClickBonousBtn()
        {
            if (canClick) return;
            if (!canClick)
            {
                canClick = true;
            }
            SoundController.instance.AudioButton();
            if (!NetworkSettingsOpener.Instance.CheckInternet())
            {
                if (!this.Notice.activeSelf)
                {
                    this.Notice.SetActive(true);
                    DOVirtual.DelayedCall(1f, delegate
                    {
                        this.Notice.SetActive(false);
                        this.canClick = false;
                    });

                }
                return;
            }
            //AdsManager.instance.ShowRewardedAd(() =>
            //{
            //    AddGold();
            //});
            if (UserManager.instance.useData.numberAds >= 3)
            {
                if (!AdsManager.instance.IsRewardAdReady())
                {
                    PopupManager.Instance.OnUI(PopupCode.NoAds);
                }
                else
                {
                    AdsManager.instance.ShowRewardedAd(() =>
                    {
                        LuckyGameManager.Instance.isCanSpin = false;
                        AddGold();
                    });
                }
            }
            else
            {
                if (!AdsManager.instance.IsRewardedInterstitialAdReady())

                {
                    PopupManager.Instance.OnUI(PopupCode.NoAds);
                }
                else
                {
                    AdsManager.instance.ShowRewardedInterstitialAd(() =>
                    {
                        LuckyGameManager.Instance.isCanSpin = false;
                        AddGold();
                    });

                }

            }
            this.canClick = false;
            //AddGold();
        }

        //private void OnDisable()
        //{
        //    sequence.Kill();
        //    transform.DOKill();
        //}
        public void AddGold()
        {
            //SoundController.instance.AudioOpenHopoe();
            UserManager.instance.useData.gold += 100;
            UserManager.instance.SaveData();
            MoveGold();
        }
        public float backwardDistance = 1f;
        public void MoveGold()
        {
            //for (int i = 0; i < this.Golds.Count; i++) 
            //{
            //    var gold = Golds[i];
            //   // var originY = GoldsPos[i];
            //    Vector3 originY = GoldsPos[i];
            //    this.Golds[i].SetActive(true);
            //    gold.transform.DOMove(targetPos.position, 0.8f).
            //        OnComplete(() =>
            //        {
            //            this.goldText.text = UserDataController.instance.dataPlayerController.gold.ToString();
            //            gold.gameObject.SetActive(false);
            //            gold.transform.position = originY;
            //        });
            //}
            LuckyGameManager.Instance.InitText();
            StartCoroutine(MoveGoldsToTarget());
        }
        IEnumerator MoveGoldsToTarget()
        {
            //foreach (GameObject gold in Golds)
            //{
            //    MoveGold(gold);
            //    yield return new WaitForSeconds(0.005f); // Chờ trước khi di chuyển gold tiếp theo
            //}
            //if (this.goldPositions.Count <= 0) yield break;
            for (int i = 0; i < 15; i++)
            {
                float x = Random.Range(this.starSpawn.position.x - 2f, this.starSpawn.position.x + 2f);
                float y = Random.Range(this.starSpawn.position.y - 1.5f, this.starSpawn.position.y + 1.5f);
                GameObject a = Instantiate(this.gold, this.holder);
                a.transform.position = new Vector3(x, y, this.starSpawn.position.z);
                a.transform.SetParent(this.holder, false);
                MoveObj(a);
                yield return new WaitForSeconds(0.005f);

            }
            yield return new WaitForSeconds(1.2f);
            if (!ismove)
            {
                ismove = true;
                this.OffUI();
            }
        }

        void MoveGold(GameObject gold)
        {
            gold.SetActive(true);
            Vector3 startPosition = gold.transform.position; // Lưu vị trí ban đầu
            Vector3 backwardPosition = startPosition - (targetPos.position - startPosition).normalized * backwardDistance;

            // Chuỗi animation: Lùi lại → Tiến lên → Ẩn object → Chờ → Quay lại & Hiện object
            gold.transform.DOMove(backwardPosition, 0.1f).SetEase(Ease.InOutSine).SetDelay(0.1f) // Lùi lại trước
            .OnComplete(() =>
                    gold.transform.DOMove(targetPos.position, 0.8f).SetEase(Ease.OutBack) // Tiến lên B
                    .OnComplete(() =>
                    {
                        this.goldText.text = UserManager.instance.useData.gold.ToString();
                        gold.transform.position = startPosition;
                        gold.SetActive(false);
                    })
                );
        }
        bool ismove;
        public void MoveObj(GameObject gold)
        {
            Vector3 startPosition = gold.transform.position;
            Vector3 backwardPosition = startPosition - (targetPos.position - startPosition).normalized * backwardDistance;
            gold.transform.DOMove(backwardPosition, 0.1f).SetEase(Ease.InOutSine).SetDelay(0.1f) // Lùi lại trước
            .OnComplete(() =>
                    gold.transform.DOMove(targetPos.position, 0.8f).SetEase(Ease.OutBack) // Tiến lên B
                    .OnComplete(() =>
                    {
                        this.goldText.text = UserManager.instance.useData.gold.ToString();
                        this.canClick = false;
                        Destroy(gold);
                        //this.OffUI();
                    })
                );
        }

        //private void ScaleBtn()
        //{
        //    sequence = DOTween.Sequence()
        //     .Append(this.btn.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).SetEase(Ease.Linear)).SetDelay(0.5f)
        //     .Append(this.btn.transform.DOScale(new Vector3(1f, 1f, 1f), 1f).SetEase(Ease.Linear))
        //     .SetLoops(-1);
        //}
    }
}