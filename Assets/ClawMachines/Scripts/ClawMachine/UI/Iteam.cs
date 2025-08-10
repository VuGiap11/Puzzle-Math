using DG.Tweening;
using System;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public enum TypeIteam
    {
        animal,
        candy,
        coin
    }
    public class Iteam : MonoBehaviour
    {
        private float timeMove = 1f;
        public TypeIteam typeIteam;
        public void MoveIteam(Action action = null)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            this.transform.transform.DOMove(ClawGameManager.Instance.targetPosMove.position, timeMove)
            .SetEase(Ease.Linear)
            .SetDelay(0f)
            .OnComplete(() =>
            {
                // SetIteam(action);
                action?.Invoke();
                Destroy(gameObject);
            });
        }

        public void SetIteam(int number)
        {
            if (this.typeIteam == TypeIteam.animal)
            {
                string id = GetComponent<Animal>().animalData.Id;
                UserManager.instance.SetAnimalOnStock(id,number);
                //UserManager.instance.useData.gold += 2;

                ClawGameManager.Instance.number += 1;
            }
            else if (this.typeIteam == TypeIteam.candy)
            {
                string id = GetComponent<Candy>().CandyData.Id;
                UserManager.instance.SetCandyOnStock(id, number);
                //UserManager.instance.useData.gold += 2;
                ClawGameManager.Instance.number +=1;
            }
            else if (this.typeIteam == TypeIteam.coin)
            {
                UserManager.instance.useData.gold += 2;
            }
            // ClawGameManager.Instance.InitGold(UserManager.instance.useData.gold);
            UserManager.instance.SaveData();

        }
    }
}