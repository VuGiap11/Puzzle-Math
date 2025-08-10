using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using Tool.HammerBreak;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public class OpenCandy : MonoBehaviour
    {
        public GameObject hammer;
        [SerializeField] private Image avarCloseCandy;
        [SerializeField] private Image avarOpenLeftCandy;
        [SerializeField] private Image avarOpenRightCandy;
        [SerializeField] private Transform startPosLeft, endPosLeft, startRightPos, endRightPos, endPos;
        public CandyData candy;
        [SerializeField] private GameObject candyUI;
        [SerializeField] private GameObject goldUI;
        [SerializeField] private GameObject bonous;
        [SerializeField] private Transform holder;
        [SerializeField] private GameObject hammerBtn, claimBtn;
        //public List<confetti> confettiList;
        [SerializeField] private ParticleSystem fx;
       // Coroutine confettiPlay;
        private bool winOpen;
        public ConfettiManager confettiManager;
        [SerializeField] private Transform oriConfetPos, endConfetPos;
        

        private void Awake()
        {

        }
        public void PlayConfetti()
        {
            //if (confettiPlay != null)
            //{
            //    StopCoroutine(confettiPlay);
            //}
            //confettiPlay = StartCoroutine(SetConfetti());
            this.confettiManager.MoveToTarget(this.endConfetPos);
        }

        public void Claim()
        {
            //if(this.confettiPlay != null)
            //StopCoroutine(this.confettiPlay);
            if (fx.isPlaying)
            {
                fx.Stop();
            }

            //for (int i = 0; i < confettiList.Count; i++)
            //{
            //    confettiList[i].gameObject.SetActive(false);
            //}
            this.confettiManager.BackToOriPoss(this.oriConfetPos);
            //DOTween.KillAll();
        }
        //IEnumerator SetConfetti()
        //{
        //    for (int i = 0; i < confettiList.Count; i++)
        //    {
        //        confettiList[i].gameObject.SetActive(true);
        //    }
        //    for (int i = 0; i < confettiList.Count; i++)
        //    {
        //        if (confettiList != null)
        //        {
        //            confettiList[i].StartScaling();
        //        }

        //        yield return new WaitForSeconds(0.2f);
        //    }
        //    //StartCoroutine(SetConfetti()); // Gọi lại để tiếp tục vòng lặp
        //}

        [ContextMenu("OpenBlinBad")]
        public void OpenBlinBad()
        {
            SoundController.instance.PressButtonAudio();
            SetBaby();

            this.hammerBtn.SetActive(false);
            avarCloseCandy.gameObject.SetActive(false);
            avarOpenLeftCandy.gameObject.SetActive(true);
            avarOpenRightCandy.gameObject.SetActive(true);
            this.hammer.SetActive(true);
            this.hammer.transform.position = pointA.position;
            this.hammer.GetComponent<HammerController>().MoveHammer(Move);
            //this.hammer.transform.localScale = new Vector3(0f, 0f, 0f);
            //CreateSmoothCurve();
            // Xoay vật thể quanh trục Z từ góc hiện tại đến 50 độ trong 5 giây
        }
        public void Init(CandyData candy)
        {
            this.candy = candy;
            this.avarCloseCandy.sprite = candy.Avatar;
            this.avarOpenLeftCandy.sprite = candy.AvatarLeftOff;
            this.avarOpenRightCandy.sprite = candy.AvatarRightOff;
            this.hammer.SetActive(false);
            this.hammer.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.avarOpenLeftCandy.gameObject.SetActive(false);
            this.avarOpenRightCandy.gameObject.SetActive(false);
            this.avarCloseCandy.gameObject.SetActive(true);
            this.avarOpenLeftCandy.transform.position = startPosLeft.position;
            this.avarOpenRightCandy.transform.position = startRightPos.position;
            this.claimBtn.SetActive(false);
            this.hammerBtn.SetActive(true);
            MyFunction.ClearChild(this.holder);
        }
        public void Move()
        {
            //this.hammer.SetActive(false);
            avarOpenLeftCandy.transform.DOMove(endPosLeft.position, 0.2f)
                .SetEase(Ease.InOutQuad)
                 .OnComplete(() => { BabyMove(); });
            avarOpenRightCandy.transform.DOMove(endRightPos.position, 0.2f)
                .SetEase(Ease.InOutQuad);
        }

        public void SetBaby()
        {
            MyFunction.ClearChild(this.holder);
            CandyManager.instance.candyData.number--;

            CandyData candy = DataAssets.Instance.GetCandyById(CandyManager.instance.candyData.id);
            int numberRandom = Random.Range(0, 100);
            if (numberRandom <= candy.percent)
            {
                SpawnGoldUI();
                this.winOpen = false;
            }
            else
            {
                this.winOpen = true;
                RandomCandy(candy.Rarity);
            }
            UserManager.instance.SaveData();
        }
        private RarityAnimal rarityAnimal;
        public void RandomCandy(Rarity rarity)
        {

            switch (rarity)
            {
                case Rarity.Common:
                    Common();
                    break;
                case Rarity.Prenium:
                    Prenium();
                    break;
                case Rarity.Legendary:
                    Legendary();
                    break;
                case Rarity.Mythical:
                    Mythical();
                    break;
                default:
                    Debug.Log("loi random");
                    break;
            }
            RandomAnimal(rarityAnimal);

            //GearDataCtrl.Instance.SaveDataGear();
            //UserManager.Instance.SaveData();
            //UserManager.Instance.SaveHeroData();
        }
        private void Common()
        {

            int randomValue = Random.Range(0, 100);
            if (randomValue < 5)
            {
                rarityAnimal = RarityAnimal.Mythical;
            }
            else if (randomValue < 10)
            {
                rarityAnimal = RarityAnimal.Legendary;
            }
            else if (randomValue < 15)
            {
                rarityAnimal = RarityAnimal.Prenium;
            }
            else
            {
                rarityAnimal = RarityAnimal.Common;
            }
        }
        private void Prenium()
        {
            int randomValue = Random.Range(0, 100);

            if (randomValue < 10)
            {
                rarityAnimal = RarityAnimal.Mythical;
            }
            else if (randomValue < 15)
            {
                rarityAnimal = RarityAnimal.Legendary;
            }
            else if (randomValue < 20)
            {
                rarityAnimal = RarityAnimal.Prenium;
            }
            else
            {
                rarityAnimal = RarityAnimal.Common;
            }
        }
        private void Legendary()
        {
            int randomValue = Random.Range(0, 100);

            if (randomValue < 15)
            {
                rarityAnimal = RarityAnimal.Mythical;
            }
            else if (randomValue < 20)
            {
                rarityAnimal = RarityAnimal.Legendary;
            }
            else if (randomValue < 25)
            {
                rarityAnimal = RarityAnimal.Prenium;
            }
            else
            {
                rarityAnimal = RarityAnimal.Common;
            }
        }
        private void Mythical()
        {
            int randomValue = Random.Range(0, 100);

            if (randomValue < 20)
            {
                rarityAnimal = RarityAnimal.Mythical;
            }
            else if (randomValue < 25)
            {
                rarityAnimal = RarityAnimal.Legendary;
            }
            else if (randomValue < 30)
            {
                rarityAnimal = RarityAnimal.Prenium;
            }
            else
            {
                rarityAnimal = RarityAnimal.Common;
            }
        }
        public List<AnimalData> ListAnimalRandom;
        public void RandomAnimal(RarityAnimal RarityAnimal)
        {
            this.ListAnimalRandom = new List<AnimalData>();
            switch (RarityAnimal)
            {
                case RarityAnimal.Common:
                    this.ListAnimalRandom.AddRange(DataAssets.Instance.ListAnimalCommons);
                    break;
                case RarityAnimal.Prenium:
                    this.ListAnimalRandom.AddRange(DataAssets.Instance.ListAnimalPreniums);
                    break;
                case RarityAnimal.Legendary:
                    this.ListAnimalRandom.AddRange(DataAssets.Instance.ListAnimalLegendarys);
                    break;
                case RarityAnimal.Mythical:
                    this.ListAnimalRandom.AddRange(DataAssets.Instance.ListAnimalMythicals);
                    break;
                default:
                    Debug.Log("loi random animal");
                    break;
            }
            SpawnCandyUi();
            UserManager.instance.SaveData();
        }
        //public CandyUI spwan;
        private void SpawnCandyUi()
        {
            this.bonous = Instantiate(this.candyUI, this.holder.transform);
            bonous.transform.SetParent(this.holder, false);
            int randomValue = Random.Range(0, this.ListAnimalRandom.Count);
            AnimalData animal = this.ListAnimalRandom[randomValue];
            bonous.GetComponent<CandyUI>().Init(animal);
            UserManager.instance.SetAnimalOnStock(animal.Id,1);
        }
        private void SpawnGoldUI()
        {
            this.bonous= Instantiate(this.goldUI, this.holder.transform);
            this.bonous.transform.SetParent(this.holder, false);
            int gold = Random.Range(10, 20);
            UserManager.instance.useData.gold += gold;
            this.bonous.GetComponent<Gold>().Init(gold);
            HomeController.instance.InitText();
            UserManager.instance.SaveData();
        }
        
        private float duration = 0.4f;

        private void BabyMove()
        {
            this.claimBtn.SetActive(true);
            this.bonous.gameObject.transform.DOMove(endPos.position, duration).SetEase(Ease.Linear);
            this.bonous.gameObject.transform.DOScale(new Vector3(3f, 3f, 3f), duration).SetEase(Ease.Linear).
                    OnComplete(() =>
                    {
                        fx.Play();
                        if (this.winOpen)
                        {
                            PlayConfetti();
                        }
                    });
        }

        public PathType pathType = PathType.CatmullRom;
        public float durationhamerMove = 0.5f;
        [SerializeField] Transform pointA, pointB;
        public void CreateSmoothCurve()
        {
            Vector3 controlPoint1 = pointA.position + (pointB.position - pointA.position) / 3 + Vector3.up * 2;
            Vector3 controlPoint2 = pointA.position + 2 * (pointB.position - pointA.position) / 3 + Vector3.up * 2;

            Vector3[] pathPoints = new Vector3[] { pointA.position, controlPoint1, controlPoint2, pointB.position };
            this.hammer.transform.DOPath(pathPoints, durationhamerMove, pathType)
                     .SetEase(Ease.Linear)
                     .OnComplete(() =>
                     {
                         this.hammer.transform.DORotate(new Vector3(0, 0, 50), 0.4f, RotateMode.FastBeyond360)
                     .SetEase(Ease.Linear)
                     .OnComplete(() => Move());
                     });
            this.hammer.transform.DOScale(new Vector3(3.5f, 3.5f, 3.5f), durationhamerMove)
            .SetEase(Ease.Linear);
        }



    }
}