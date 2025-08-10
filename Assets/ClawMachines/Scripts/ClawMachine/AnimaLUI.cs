using NTPackage.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public class AnimaLUI : MonoBehaviour
    {
        [SerializeField] private Image avar;
        public Animaldata Animaldata;
        public BabyThreeUI babyThreeUI;
        public AnimaLUI animaLUI;
        public BabyThreeUIStorePanel BabyThreeUIStorePanel;

        public void Init(Animaldata animaldata)
        {
            this.Animaldata = animaldata;
            AnimalData animal = DataAssets.Instance.GetAnimalById(animaldata.id);
            avar.sprite = animal.Avatar;
        }
        //public void OneClick()
        //{
        //    SoundController.instance.PressButtonAudio();
        //    int indexOnStore = SetIndexBabyOnStore();
        //    Debug.Log("index" + indexOnStore);
        //    if (indexOnStore < 0) return;
        //    UserManager.instance.useData.slots[indexOnStore] = Animaldata.id;
        //    BabyThreeUI _babyThreeUI = Instantiate(this.babyThreeUI, HomeController.instance.BabyThreeUIStorePanel.lsBabyPos[indexOnStore].transform);
        //    _babyThreeUI.transform.SetParent(HomeController.instance.BabyThreeUIStorePanel.lsBabyPos[indexOnStore].transform, false);
        //    HomeController.instance.BabyThreeUIStorePanel.lsBabyPos[indexOnStore].babyThreeUI = _babyThreeUI;
        //    _babyThreeUI.Init(Animaldata.id);
        //    _babyThreeUI.babyThreeUI = _babyThreeUI;
        //    _babyThreeUI.BabyPos = HomeController.instance.BabyThreeUIStorePanel.lsBabyPos[indexOnStore];
        //    _babyThreeUI.index = indexOnStore;
        //    _babyThreeUI.AnimaLUI = animaLUI;

        //    this.animaLUI.gameObject.SetActive(false);
        //    this.Animaldata.slot = 1;
        //    HomeController.instance.BabyThreeUIStorePanel.SetPanel();
        //    UserManager.instance.SaveHeroData();
        //    Debug.Log(_babyThreeUI.gameObject.name);
        //}
        //public int SetIndexBabyOnStore()
        //{
        //    for (int i = 0; i < HomeController.instance.BabyThreeUIStorePanel.lsBabyPos.Count; i++)
        //    {
        //        if (HomeController.instance.BabyThreeUIStorePanel.lsBabyPos[i].babyThreeUI != null) continue;
        //        return i;
        //    }
        //    return -1;
        //}
        public void OneClick()
        {
            SoundController.instance.PressButtonAudio();
            int indexOnStore = SetIndexBabyOnStore();
            Debug.Log("index" + indexOnStore);
            if (indexOnStore < 0) return;
            UserManager.instance.useData.slots[indexOnStore] = Animaldata.id;
            BabyThreeUI _babyThreeUI = Instantiate(this.babyThreeUI, this.BabyThreeUIStorePanel.lsBabyPos[indexOnStore].transform);
            _babyThreeUI.BabyThreeUIStorePanel = this.BabyThreeUIStorePanel;
            _babyThreeUI.transform.SetParent(this.BabyThreeUIStorePanel.lsBabyPos[indexOnStore].transform, false);
            this.BabyThreeUIStorePanel.lsBabyPos[indexOnStore].babyThreeUI = _babyThreeUI;
            _babyThreeUI.Init(Animaldata.id);
            _babyThreeUI.babyThreeUI = _babyThreeUI;
            _babyThreeUI.BabyPos = this.BabyThreeUIStorePanel.lsBabyPos[indexOnStore];
            _babyThreeUI.index = indexOnStore;
            _babyThreeUI.AnimaLUI = animaLUI;

            this.animaLUI.gameObject.SetActive(false);
            this.Animaldata.slot = 1;
            this.BabyThreeUIStorePanel.SetPanel();
            UserManager.instance.SaveData();
            Debug.Log(_babyThreeUI.gameObject.name);
        }
        public int SetIndexBabyOnStore()
        {
            for (int i = 0; i < this.BabyThreeUIStorePanel.lsBabyPos.Count; i++)
            {
                if (this.BabyThreeUIStorePanel.lsBabyPos[i].babyThreeUI != null) continue;
                return i;
            }
            return -1;
        }
    }
}