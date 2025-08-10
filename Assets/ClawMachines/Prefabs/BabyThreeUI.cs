using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{

    public class BabyThreeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI numberText;
        [SerializeField] private Image avar;
        [SerializeField] private bool isDragged = false;
        [SerializeField] private Vector2 mouseDragStartPosition;
        [SerializeField] private Vector2 startPos;
        public BabyThreeUIStorePanel BabyThreeUIStorePanel;
        public int index; // ví trí mà hero đứng trong đội hình 
        private const float clickThreshold = 0.2f;
        //private float clickStartTime;
        public Animaldata Animaldata;
        public BabyPos BabyPos;
        public AnimaLUI AnimaLUI;
        //public Drag drag;
        public BabyThreeUI babyThreeUI;
        public void Init(string id)
        {
            AnimalData animal = DataAssets.Instance.GetAnimalById(id);
            //Animaldata Animaldata = ClawUserManager.instance.GetAnimalDatabyID(id);
            this.Animaldata = UserManager.instance.GetAnimalDatabyID(id);
            numberText.text = Animaldata.number.ToString();
            avar.sprite = animal.Avatar;
        }
        public void _OnMouseDown()
        {
            this.BabyThreeUIStorePanel.noticeObj.SetActive(true);
           this.BabyThreeUIStorePanel.SetPanelWhenDrag();
            isDragged = true;
            mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPos = transform.position;
            Debug.Log(startPos);
            ////IncreaseOrderInlayer(5);

            //transform.SetParent(ClawGameManager.Instance.BabyThreeUIStorePanel.swapPos);
            //clickStartTime = Time.time;
        }

        public void _OnMouseDrag()
        {
            if (isDragged)
            {
                transform.SetParent(this.BabyThreeUIStorePanel.swapPos);
                //transform.localPosition = spriteDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPosition);
                mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = mouseDragStartPosition;

                //Debug.Log(transform.position);
            }
        }
        //public void _OnMouseUp()
        //{
        //    isDragged = false;
        //    HomeController.instance.BabyThreeUIStorePanel.noticeObj.SetActive(false);
        //    //float clickDuration = Time.time - clickStartTime; // Tính thời gian giữ nút

        //    //if (clickDuration < clickThreshold)
        //    //{
        //    //    // Nếu thời gian giữ nhỏ hơn 0.2s, thì xử lý là click
        //    //    //OneClick();
        //    //}
        //    //else
        //    //{
        //        mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        int pos = HomeController.instance.BabyThreeUIStorePanel.CheckNearPos(mouseDragStartPosition);

        //        Debug.Log(pos);
        //        if (pos == -1)
        //        {
        //            transform.SetParent(this.BabyPos.transform,false);
        //            transform.position = startPos;
        //            //Debug.Log(pos);
        //        }
        //        else if(pos == HomeController.instance.BabyThreeUIStorePanel.lsBabyPos.Count -1)
        //        {
        //            Debug.Log("delete");
        //            BabyThreeUI babyThreeUI = HomeController.instance.BabyThreeUIStorePanel.lsBabyPos[this.index].babyThreeUI;
        //            UserManager.instance.useData.slots[this.index] = "A0000";
        //            this.AnimaLUI.gameObject.SetActive(true);
        //            this.AnimaLUI.Animaldata.slot = -1;
        //            HomeController.instance.BabyThreeUIStorePanel.lsBabyPos[this.index].babyThreeUI = null;
        //            Destroy(babyThreeUI.gameObject);
        //            //IncreaseOrderInlayer(1);
        //        }
        //        else
        //        {
        //            HomeController.instance.BabyThreeUIStorePanel.swap(this.babyThreeUI, index, pos);
        //            index = pos;
        //        }
        //    //}

        //    UserManager.instance.SaveHeroData();
        //}
        public void _OnMouseUp()
        {
            isDragged = false;
            this.BabyThreeUIStorePanel.noticeObj.SetActive(false);
            mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (this.BabyThreeUIStorePanel.swapOnPanel(mouseDragStartPosition))
            {
                Debug.Log("delete");
                BabyThreeUI babyThreeUI = this.BabyThreeUIStorePanel.lsBabyPos[this.index].babyThreeUI;
                UserManager.instance.useData.slots[this.index] = "A0000";
                this.AnimaLUI.gameObject.SetActive(true);
                this.AnimaLUI.Animaldata.slot = -1;
                this.BabyThreeUIStorePanel.lsBabyPos[this.index].babyThreeUI = null;
                Destroy(babyThreeUI.gameObject);
            }
            else
            {
                int pos = this.BabyThreeUIStorePanel.CheckNearPos(mouseDragStartPosition);

                Debug.Log(pos);
                if (pos == -1)
                {
                    transform.SetParent(this.BabyPos.transform, false);
                    transform.position = startPos;
                    //Debug.Log(pos);
                }
                else
                {
                    this.BabyThreeUIStorePanel.swap(this.babyThreeUI, index, pos);
                    index = pos;
                }
              
               
            }
            this.BabyThreeUIStorePanel.SetPanel();
            UserManager.instance.SaveData();
        }
    }
}
