using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using DG.Tweening;
namespace NailSalonGame
{
    public class UnlockPanel : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public List<CanvasGroup> canvasGroup2;
        [SerializeField]
        private Image shadow;
        [SerializeField]
        public Image imageColor;
        public Image imagePattern;
        public Image imageSticker;
        [SerializeField]
        private Transform _posBegin;
        [SerializeField]
        private List<Transform> _posShow;
        public Transform posColorBtn;
        public Transform posPatternBtn;
        public Transform posStickerBtn;
        public ParticleSystem[] effect;
        public Ease easeType;
        public Ease easeType2;
        public float time;
        public float time2;
        public float time3;
        public ParticleSystem explosision;

        private void Awake()
        {
            //explosision.gameObject.SetActive(false);

        }
        private void Start()
        {
            canvasGroup.DOFade(0, 0);
            shadow.enabled = false;
            //UnlockGift.Instance.Hide();
            imageColor.gameObject.SetActive(false);
            imagePattern.gameObject.SetActive(false);
            imageSticker.gameObject.SetActive(false);
            imageColor.transform.position = _posBegin.transform.position;
            imagePattern.transform.position = _posBegin.transform.position;
            imageSticker.transform.position = _posBegin.transform.position;

            //if (GameManager.instance.unlockItem)
            //{
            //    UnlockItem();
            //    GameManager.instance.unlockItem = false;
            //}
        }
        // Start is called before the first frame update
        private void Update()
        {
          
            if(Input.GetKeyDown(KeyCode.E))
            {

                UnlockItem();
            }
        }
        public void UnlockNotEffect(int index)
        {

        }
        public void LockAll()
        {
            //int patterns = GameData.instance.nailGameDefault.patternAllSprites.Count;
            //int paints = GameData.instance.nailGameDefault.paintAllTools.Count;
            //int stickers = GameData.instance.nailGameDefault.stickerAllSprites.Count;
            //for (int j = 0; j < patterns; j++)
            //{
            //    DataItems.Instance.LockItem(ItemType.Patern, j);
            //}
            //for(int j=0;j<paints;j++)
            //{
            //    DataItems.Instance.LockItem(ItemType.NailColor, j);
            //}
            //for (int j = 0; j < stickers; j++)
            //{
            //    DataItems.Instance.LockItem(ItemType.Sticker, j);
            //}
        }
        public async void UnlockItem()
        {
            //GameData.instance.IncreaseItemUnlock();
            //int items = GameData.instance.itemUnlocks;
            //await ApplyColorSpriteToImage(items); // nap image vao
            //await ApplyPatternSpriteToImage(items);
            //await ApplyStickerSpriteToImage(items);            
            await PlayEffectUnlock();
        }
        public async void PlayEffect()
        {
            await PlayEffectUnlock();
        }

        private async Task PlayEffectUnlock()
        {
            shadow.enabled = true;
            canvasGroup.alpha = 1;
            foreach(var child in canvasGroup2)
            {
                child.interactable = true;
                child.blocksRaycasts = false;
            }
            
            //await UnlockGift.Instance.Play();
            explosision.gameObject.SetActive(true);
            imageColor.gameObject.SetActive(true);
            imagePattern.gameObject.SetActive(true);
            imageSticker.gameObject.SetActive(true);
            imageColor.transform.DOMove(_posShow[0].position, time).SetEase(easeType);
            imagePattern.transform.DOMove(_posShow[1].position, time).SetEase(easeType);
            imageSticker.transform.DOMove(_posShow[2].position, time).SetEase(easeType);
           
           // AudioManager.Instance.PlayDone(SfxName.unlockGift);
            await Task.Delay(2000);
            shadow.enabled = false;
          //  canvasGroup.DOFade(0, 0.5f);
           // AudioManager.Instance.PlayDone(SfxName.donePattern);
            imageColor.transform.DOMove(posColorBtn.position, time3).SetEase(easeType2);
            imageColor.transform.DOScale(0, time3).OnComplete(() =>
            {
                effect[0].gameObject.SetActive(true);
                
            });
            await Task.Delay((int)time2 * 1000);
          //  AudioManager.Instance.PlayDone(SfxName.donePattern);
            imagePattern.transform.DOMove(posPatternBtn.position, time3).SetEase(easeType2);
            imagePattern.transform.DOScale(0, time3).OnComplete(() =>
            {
                effect[1].gameObject.SetActive(true);
            });
            await Task.Delay((int)time2 * 1000);
           // AudioManager.Instance.PlayDone(SfxName.donePattern);
            imageSticker.transform.DOMove(posStickerBtn.position, time3).SetEase(easeType2);
            imageSticker.transform.DOScale(0, time3).OnComplete(() =>
            {
                effect[2].gameObject.SetActive(true);
            });
            await Task.Delay((int)time2 * 1000);
            foreach (var child in canvasGroup2)
            {
                child.interactable = true;
                child.blocksRaycasts = true;
            }

        }


    }
}

