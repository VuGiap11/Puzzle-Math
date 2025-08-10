//using PrimeTween;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        public Sprite hiddenIconSprite;
        public Sprite iconSprite;
        public PosCard posCard;
        public bool isSeclected = false;
        //public CardsController cardsController;

        public void SetIconSprite(Sprite sp)
        {
            this.iconSprite = sp;
        }
        public void Show()
        {
            //Tween.Rotation(transform, new Vector3(0f, 180f, 0f), 0.2f);

            //Tween.Delay(0.1f, () => this.iconImage.sprite = iconSprite);
            //isSeclected = true;
            transform.DORotate(new Vector3(0f, 180f, 0f), 0.2f, RotateMode.Fast);

            // Thực hiện hành động đổi sprite sau 0.1 giây
            DOVirtual.DelayedCall(0.1f, () => this.iconImage.sprite = iconSprite);

            isSeclected = true;
        }
        public void Hide()
        {
            //Tween.Rotation(transform, new Vector3(0f, 0f, 0f), 0.2f);
            //Tween.Delay(0.1f, () =>
            //{
            //    this.iconImage.sprite = this.hiddenIconSprite;
            //    this.isSeclected = false;
            //});
            // Quay ngược lại về (0,0,0)
            transform.DORotate(new Vector3(0f, 0f, 0f), 0.2f, RotateMode.Fast);

            // Đổi sprite về hidden sau 0.1 giây
            DOVirtual.DelayedCall(0.1f, () =>
            {
                this.iconImage.sprite = this.hiddenIconSprite;
                this.isSeclected = false;
            });


        }
        public void _OneClick()
        {
            Debug.Log("oneClick");
            CardsController.instance.SetSelected(this);
           
            //cardsController.SetSelected(this);
        }

    }
}