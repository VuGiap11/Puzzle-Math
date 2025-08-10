
using DG.Tweening;
using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rubik.ClawMachine;
//using Rubik.MergeGame;

namespace Rubik.ClawMachine
{
    public class CardsController : MonoBehaviour
    {
        public static CardsController instance;
        [SerializeField] Card cardPrefab;
        [SerializeField] Transform gridTranform;
        [SerializeField] List<Sprite> sprites;
        private List<Sprite> spritePairs;
        Card firstSelected;
        Card secendSelected;
        int matchCounts = 0;
        public List<PosCard> lsposCards = new List<PosCard>();
        private List<PosCard> lsPosCardsOnGame;
        public List<Card> lsCardChoose;
        private List<Card> lsCard;
        public bool canSelect;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        private void Start()
        {
            //PreparSprites();
            //CreatCards();

            //Tween.Delay(1.5f, () =>
            //{
            //    Init();

            //});
            DOVirtual.DelayedCall(2f, () =>
            {
                Init();
            });
        }


        public void Init()
        {
            //GameMemoryController.instance.goldMemory = 0;
            DeleteAllCard();
            PreparSprites();
            CreatCards();
            ShowCard();
            GameMemoryController.instance.StopTime();
            //Tween.Delay(1.5f, () =>
            //{
            //    HideCard();
            //    GameController.instance.ResetTime();

            //});
            DOVirtual.DelayedCall(2.5f, () =>
            {
                HideCard();
                GameMemoryController.instance.ResetTime();
            });

        }
        public void DeleteAllCard()
        {
            for (int i = 0; i < this.lsposCards.Count; i++)
            {
                MyFunction.ClearChild(this.lsposCards[i].holder);
            }
            this.lsCanOpen.Clear();
        }
        public void HideCard()
        {
            this.canSelect = false;
            for (int i = 0; i < lsCard.Count; i++)
            {
                lsCard[i].Hide();
            }
        }
        public void ShowCard()
        {
            this.canSelect = true;
            for (int i = 0; i < lsCard.Count; i++)
            {
                lsCard[i].Show();
            }
        }
        private void PreparSprites()
        {
            //ok
            int level = 0;
            this.spritePairs = new List<Sprite>();
            //for (int i = 0; i < this.sprites.Count; i++)
            //{
            //    this.spritePairs.Add(this.sprites[i]);
            //    this.spritePairs.Add(this.sprites[i]);
            //}
            //ShufffeSprites(this.spritePairs);
            //ok
            level = GameMemoryController.instance.level;
            if (level >= 7)
            {
                level = 7;
            }
            ShufffeSprites(this.sprites);

            SetPosition(DataAssets.Instance.idPositioncfs.idPositionDatas[level].id);
            if (lsPosCardsOnGame.Count % 2 == 0)
            {
                //FunctionB(); // gọi hàm B nếu a chia hết cho 2
                for (int i = 0; i < lsPosCardsOnGame.Count / 2; i++)
                {
                    this.spritePairs.Add(this.sprites[i]);
                    this.spritePairs.Add(this.sprites[i]);
                }
                ShufffeSprites(this.spritePairs);
            }
            else
            {
                int result = lsPosCardsOnGame.Count / 2; // lấy phần nguyên của phép chia
                Debug.Log("Phần nguyên của a / 2 là: " + result);
                for (int i = 0; i < lsPosCardsOnGame.Count / 2; i++)
                {
                    this.spritePairs.Add(this.sprites[i]);
                    this.spritePairs.Add(this.sprites[i]);
                }
                ShufffeSprites(this.spritePairs);
            }
        }

        public void ShufffeSprites(List<Sprite> spriteList)
        {
            for (int i = spriteList.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                Sprite temp = spriteList[i];
                spriteList[i] = spriteList[randomIndex];
                spriteList[randomIndex] = temp;
            }
        }


        void CreatCards()
        {
            this.lsCard = new List<Card>();
            //MyFunction.ClearChild(this.gridTranform);
            for (int i = 0; i < this.spritePairs.Count; i++)
            {
                //Card card = Instantiate(cardPrefab, gridTranform);
                //card.SetIconSprite(this.spritePairs[i]);
                //card.cardsController = this;
                MyFunction.ClearChild(lsPosCardsOnGame[i].holder);
                Card card = Instantiate(cardPrefab, lsPosCardsOnGame[i].transform);
                this.lsCard.Add(card);
                card.gameObject.transform.SetParent(lsPosCardsOnGame[i].holder, false);
                card.Show();
                card.posCard = lsPosCardsOnGame[i];
                lsPosCardsOnGame[i].card = card;
                card.SetIconSprite(this.spritePairs[i]);
            }
        }

        public void SetSelected(Card card)
        {
            
            //mo ban dau
            if (this.canSelect) return;
            SoundController.instance.AudioButton();
            if (card.isSeclected == false)
            {
                Debug.Log("show");
                card.Show();
                if (this.firstSelected == null)
                {
                    this.lsCardChoose.Clear();
                    this.firstSelected = card;
                    this.lsCardChoose.Add(card);
                    return;
                }
                if (this.secendSelected == null)
                {
                    this.canSelect = true;
                    this.secendSelected = card;
                    this.lsCardChoose.Add(card);
                    StartCoroutine(CheckMatching(firstSelected, secendSelected));
                   
                  
                }
            }
        }
        IEnumerator CheckMatching(Card a, Card b)
        {
            yield return new WaitForSeconds(0.5f);
            if (a.iconSprite == b.iconSprite)
            {

               // GameMemoryController.instance.goldMemory +=2;
                UserManager.instance.useData.gold += 2;
                UserManager.instance.SaveData();
                GameMemoryController.instance.InitText();
                SoundController.instance.DoneMemoryAudio();
                this.matchCounts++;
                //for (int i = 0; i < this.lsCardChoose.Count; i++)
                //{
                //    this.lsCardChoose[i].isSeclected = true;
                //    this.lsCardChoose[i].posCard.particleSystem.Play();
                //    PrimeTween.Sequence.Create()
                //       .Chain(PrimeTween.Tween.Scale(this.lsCardChoose[i].posCard.holder, Vector3.one * 1.2f, 0.2f, ease: PrimeTween.Ease.OutBack))
                //       .Chain(PrimeTween.Tween.Scale(this.lsCardChoose[i].posCard.holder, Vector3.one, 0.1f));
                //    this.lsCardChoose[i].gameObject.SetActive(false);

                //}
                for (int i = 0; i < this.lsCardChoose.Count; i++)
                {
                    var card = this.lsCardChoose[i];
                    card.isSeclected = true;
                    card.posCard.particleSystem.Play();

                    // Tạo DOTween sequence tương tự
                    Sequence seq = DOTween.Sequence();
                    seq.Append(card.posCard.holder.transform.DOScale(Vector3.one * 1.2f, 0.2f).SetEase(Ease.OutBack));
                    seq.Append(card.posCard.holder.transform.DOScale(Vector3.one, 0.1f));

                    // Ẩn gameObject sau tween (nếu muốn delay, nên cho vào OnComplete)
                    card.gameObject.SetActive(false);
                }
                if (this.matchCounts >= spritePairs.Count / 2)
                {
                    //Tween.Delay(0.2f, () =>
                    //{
                    //    NextTurn();
                    //});
                    DOVirtual.DelayedCall(0.2f, NextTurn);

                    //PrimeTween.Sequence.Create()
                    //    .Chain(PrimeTween.Tween.Scale(gridTranform, Vector3.one * 1.2f, 0.2f, ease: PrimeTween.Ease.OutBack))
                    //    .Chain(PrimeTween.Tween.Scale(gridTranform, Vector3.one, 0.1f));

                }
            }
            else
            {
                a.Hide();
                b.Hide();
            }
            this.firstSelected = null;
            this.secendSelected = null;
            this.canSelect = false;
        }
        public void NextTurn()
        {
            GameMemoryController.instance.level++;
            PopupManager.Instance.OnUI(PopupCode.LevelComleted);
            GameMemoryController.instance.InitText();
            if (GameMemoryController.instance.level > UserManager.instance.useData.highLevel)
            {
                UserManager.instance.useData.highLevel = GameMemoryController.instance.level;
                UserManager.instance.SaveData();
            }
            this.matchCounts = 0;
            //Init();
            // GameController.instance.ResetTime();
        }

        public void SetPosition(List<int> ls)
        {
            this.lsPosCardsOnGame = new List<PosCard>();
            if (ls.Count <= 0) return;
            for (int i = 0; i < ls.Count; i++)
            {
                for (int j = 0; j < lsposCards.Count; j++)
                {
                    if (lsposCards[j].id == ls[i])
                    {
                        this.lsPosCardsOnGame.Add(lsposCards[j]);
                    }
                }
            }

        }
        private List<Card> lsCanOpen = new List<Card>();

        public void SetCardCanOpen()
        {
            this.lsCanOpen = new List<Card>();
            for (int i = 0; i < this.lsCard.Count; i++)
            {
                if (this.lsCard[i].isSeclected) continue;
                this.lsCanOpen.Add(this.lsCard[i]);
            }
        }
        public Card SetCardToHammer()
        {
            SetCardCanOpen();
            if (this.lsCanOpen == null)
            {
                return null;
            }
            else
            {
                if (this.firstSelected == null)
                {
                    int number = Random.Range(0, this.lsCanOpen.Count);
                    return this.lsCanOpen[number];
                }
                else
                {
                    for (int i = 0; i < this.lsCanOpen.Count; i++)
                    {
                        if (this.lsCanOpen[i].iconSprite == firstSelected.iconSprite && !this.lsCanOpen[i].isSeclected)
                        {
                            return this.lsCanOpen[i];
                        }
                    }
                }
            }

            return null;
        }

    }
}