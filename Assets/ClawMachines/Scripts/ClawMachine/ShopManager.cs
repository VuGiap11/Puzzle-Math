using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class ShopManager : MonoBehaviour
    {
        public static ShopManager Instance;
        [SerializeField] private Transform Holder;
        [SerializeField] private AnimaLUI animaLUI;
        private void Awake()
        {
            if (Instance == null)
            Instance = this;
        }
        private void OnEnable()
        {
            //SpawnAnimalUI();
        }
        private void SpawnAnimalUI()
        {
            MyFunction.ClearChild(this.Holder);
            if (UserManager.instance.Animaldatas.animaldatas == null || UserManager.instance.Animaldatas.animaldatas.Count <= 0) return;
            
            for (int i = 0; i < UserManager.instance.Animaldatas.animaldatas.Count; i++)
            {
                if (!UserManager.instance.Animaldatas.animaldatas[i].isDone) continue;
                AnimaLUI animaLUI = Instantiate(this.animaLUI);
                animaLUI.transform.SetParent(this.Holder, false);
                animaLUI.Init(UserManager.instance.Animaldatas.animaldatas[i]);
            }
        }
    }
}