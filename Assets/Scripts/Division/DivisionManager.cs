using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.math
{
    public class DivisionManager : MonoBehaviour
    {
        public static DivisionManager instance;
        public List<DivisionBaby> lsDivisionBabies = new List<DivisionBaby>();
        public Transform PanelBabyAnswer;
        public Baby baby;
        public Number number;

        //public bool isSpawn;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Start()
        {
            this.number = GameController.instance.GenerateRandomSumExpression();
            UserManager.instance.RandomIdAvar();
            for (int i = 0; i < this.lsDivisionBabies.Count; i++)
            {
                this.lsDivisionBabies[i].gameObject.SetActive(false);
            }
            PopupManager.Instance.OnUI(PopupCode.AnswerPanel);
            PopupManager.Instance.OnUI(PopupCode.MathPanel);

            //for (int i = 0; i < this.number.result; i++)
            //{
            //    this.lsDivisionBabies[i].gameObject.SetActive(true);
            //    this.lsDivisionBabies[i].SpawnBaby(this.number);
            //}

            //MyFunction.ClearChild(this.PanelBabyAnswer);
            //for (int i = 0; i < number.num1; i++)
            //{
            //    Baby baby = Instantiate(this.baby, PanelBabyAnswer);
            //    baby.transform.SetParent(PanelBabyAnswer.transform);
            //    baby.Init(UserManager.instance.IdAvar);
            //}
            SpawnBaby1();

        }
        public void SetQuestion()
        {
            this.number = GameController.instance.GenerateRandomSumExpression();
            UserManager.instance.RandomIdAvar();
            for (int i = 0; i < this.lsDivisionBabies.Count; i++)
            {
                this.lsDivisionBabies[i].gameObject.SetActive(false);
            }
            SpawnBaby1();
            PopupManager.Instance.UpdateDataUI(PopupCode.AnswerPanel);
            PopupManager.Instance.UpdateDataUI(PopupCode.MathPanel);
        }

        IEnumerator SpawnBabyNumber1()
        {
            MyFunction.ClearChild(this.PanelBabyAnswer);
            for (int i = 0; i < number.num1; i++)
            {
                Baby baby = Instantiate(this.baby, PanelBabyAnswer);
                baby.transform.SetParent(PanelBabyAnswer.transform);
                baby.Init(UserManager.instance.IdAvar);
                yield return new WaitForSeconds(0.1f);
            }
            SpawnBaby2();
        }



        Coroutine _CoSpawnBabyNumber1, _CoSpawnBabyNumber2;
        IEnumerator SpawnBabyNumber2()
        {
            for (int i = 0; i < number.result; i++)
            {
                this.lsDivisionBabies[i].gameObject.SetActive(true);
                this.lsDivisionBabies[i].SpawnBaby(this.number);
                //yield return new WaitForSeconds(0.2f);
                yield return new WaitUntil(() => isSpawn());
            }
        }

        public void SpawnBaby1()
        {
            if (this._CoSpawnBabyNumber1 != null)
            {
                StopCoroutine(this._CoSpawnBabyNumber1);
            }
            this._CoSpawnBabyNumber1 = StartCoroutine(SpawnBabyNumber1());
        }
        public void SpawnBaby2()
        {
            if (this._CoSpawnBabyNumber2 != null)
            {
                StopCoroutine(this._CoSpawnBabyNumber2);
            }
            this._CoSpawnBabyNumber2 = StartCoroutine(SpawnBabyNumber2());
        }

        public bool isSpawn()
        {
            for (int i = 0; i < this.lsDivisionBabies.Count; i++)
            {
                if (this.lsDivisionBabies[i].isSpawn)
                {
                    return false;
                }
            }
            return true;
        }

        public void BackToStartScene()
        {
            SceneController.Instance.LoadToSceneStartGame();
        }
    }
}