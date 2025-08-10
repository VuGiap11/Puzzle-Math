using NTPackage.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class BabyThreeUIStorePanel : PopupUI
    {
        public BabyThreeUI BabyThreeUI;
        public List<BabyPos> lsBabyPos;
        public Transform swapPos;
        public Transform Holder;
        [SerializeField] private AnimaLUI animaLUI;
        public List<AnimaLUI> animaLUIList;
        public GameObject noticeObj;
        public Transform panel;
        //public void OnEnable()
        //{  
        //    SpawnAnimalUI();
        //    SpawnBabyThreeUI();
        //    SetPanel();

        //}
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            SpawnAnimalUI();
            SpawnBabyThreeUI();
            SetPanel();
        }
        public void SetPanelWhenDrag()
        {
            for (int i = 0; i < this.lsBabyPos.Count; i++)
            {
                this.lsBabyPos[i].CheckPosition();
            }
        }
        public void SetPanel()
        {
            for (int i = 0; i < this.lsBabyPos.Count; i++)
            {
                this.lsBabyPos[i].Init();
            }
        }
        [ContextMenu("SpawnBabyThreeUI")]
        public void SpawnBabyThreeUI()
        {
            for (int i = 0; i < UserManager.instance.useData.slots.Count; i++)
            {
                MyFunction.ClearChild(lsBabyPos[i].transform);
                if (UserManager.instance.useData.slots[i] == "A0000") continue;
                BabyThreeUI BabyThreeUI = Instantiate(this.BabyThreeUI);
                BabyThreeUI.transform.SetParent(this.lsBabyPos[i].transform, false);
                BabyThreeUI.babyThreeUI = BabyThreeUI;
                this.lsBabyPos[i].babyThreeUI = BabyThreeUI;
                BabyThreeUI.index = i;

                BabyThreeUI.BabyPos = this.lsBabyPos[i];
                BabyThreeUI.AnimaLUI = GetAnimaLUIbyID(UserManager.instance.useData.slots[i]);
                BabyThreeUI.Init(UserManager.instance.useData.slots[i]);

                BabyThreeUI.BabyThreeUIStorePanel = this;
                //animaLUI.Init(ClawUserManager.instance.Animaldatas.animaldatas[i]);
            }
        }

        private void SpawnAnimalUI()
        {
            animaLUIList = new List<AnimaLUI>();
            MyFunction.ClearChild(this.Holder);
            if (UserManager.instance.Animaldatas.animaldatas == null || UserManager.instance.Animaldatas.animaldatas.Count <= 0) return;
            for (int i = 0; i < UserManager.instance.Animaldatas.animaldatas.Count; i++)
            {
                if (!UserManager.instance.Animaldatas.animaldatas[i].isDone) continue;
                AnimaLUI animaLUI = Instantiate(this.animaLUI);
                this.animaLUIList.Add(animaLUI);
                animaLUI.transform.SetParent(this.Holder, false);
                animaLUI.Init(UserManager.instance.Animaldatas.animaldatas[i]);
                animaLUI.animaLUI = animaLUI;
                animaLUI.BabyThreeUIStorePanel = this;
                if (animaLUI.Animaldata.slot != -1)
                {
                    animaLUI.gameObject.SetActive(false);
                }
            }
        }

        public AnimaLUI GetAnimaLUIbyID(string id)
        {
            return animaLUIList.Find(a => { return a.Animaldata.id == id; });
        }
        public void swap(BabyThreeUI hero, int oriPos, int targetPos)
        {

            Debug.Log("aaaaaa" + oriPos + "_" + targetPos);

            if (oriPos < 0)
            {
                this.lsBabyPos[targetPos].babyThreeUI = hero;
                hero.transform.SetParent(this.lsBabyPos[targetPos].transform);
                hero.transform.localPosition = Vector3.zero;
                Debug.Log("lôi game rồi bạn ơi");
            }
            else
            {
                if (this.lsBabyPos[targetPos].babyThreeUI == null)
                {
                    this.lsBabyPos[targetPos].babyThreeUI = hero;
                    hero.transform.SetParent(this.lsBabyPos[targetPos].transform);
                    hero.transform.localPosition = Vector3.zero;
                    hero.BabyPos = this.lsBabyPos[targetPos];
                    this.lsBabyPos[oriPos].babyThreeUI = null;
                }
                else
                {
                    BabyThreeUI temp = this.lsBabyPos[oriPos].babyThreeUI;
                    this.lsBabyPos[oriPos].babyThreeUI = this.lsBabyPos[targetPos].babyThreeUI;
                    this.lsBabyPos[targetPos].babyThreeUI = temp;

                    hero.transform.SetParent(this.lsBabyPos[targetPos].transform);
                    hero.transform.localPosition = Vector3.zero;
                    hero.BabyPos = this.lsBabyPos[targetPos];
                    this.lsBabyPos[oriPos].babyThreeUI.transform.SetParent(this.lsBabyPos[oriPos].transform);
                    this.lsBabyPos[oriPos].babyThreeUI.transform.localPosition = Vector3.zero;

                    this.lsBabyPos[oriPos].babyThreeUI.index = this.lsBabyPos[targetPos].babyThreeUI.index;
                    this.lsBabyPos[oriPos].babyThreeUI.BabyPos = this.lsBabyPos[targetPos].babyThreeUI.BabyPos;
                }

                SwapElements(UserManager.instance.useData.slots, oriPos, targetPos);
                UserManager.instance.SaveData();
            }
            
        }
        public int CheckNearPos(Vector2 pos)
        {
            int index = 0;
            foreach (BabyPos lsBabyPos in this.lsBabyPos)
            {
                //Debug.Log("avatarheropos  +  " + avatarhero.transform.position);

                //Debug.Log("heropos + " + pos);
                if (pos.x < lsBabyPos.transform.position.x + 0.2f && pos.x > lsBabyPos.transform.position.x - 0.2f && pos.y < lsBabyPos.transform.position.y + 0.2f && pos.y > lsBabyPos.transform.position.y - 0.2f)
                {

                    //Debug.Log(index);
                    return index;
                }
                index++;
            }
            return -1;
        }

        public bool swapOnPanel(Vector2 pos)
        {
            if (pos.x < this.panel.position.x + 10f && pos.x > this.panel.position.x - 10f && pos.y < this.panel.position.y + 0.2f && pos.y > this.panel.position.y - 0.2f)
            {
                return true;
            }
            return false;
        }
        public static void SwapElements(List<string> list, int index1, int index2)
        {
            // Hoán đổi các phần tử
            string temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
}