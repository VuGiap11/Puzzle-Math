using DG.Tweening;
using NTPackage.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class ClowController : MonoBehaviour
    {
        [SerializeField] private float speedHook;
        private float speedDown = 1.5f;

        public bool isDown;
        //public bool isPlay = false;
        private Tweener hookTweener;
        private Vector3 initialPosition;
        public Rigidbody2D Rclaw, LcLaw;
        public JoyStick JoyStick;
        [SerializeField] private Vector2 minPositionPlayer;
        [SerializeField] private Vector2 maxPositionPlayer;
        [SerializeField] private MoveDown moveDown;
        public List<Iteam> LsIteams = new List<Iteam>();
        private void Start()
        {
            //Rclaw = GameObject.Find("ClawsRight").GetComponent<Rigidbody2D>();
            //LcLaw = GameObject.Find("ClawsLeft").GetComponent<Rigidbody2D>();
        }

        public void MoveDown()
        {
            //if (isPlay) return;
            if (SceneController.Instance.statusGame != StatusGame.StartGame) return;
            GetComponent<CircleCollider2D>().enabled = true;
            Rclaw.GetComponent<PolygonCollider2D>().enabled = true;
            LcLaw.GetComponent<PolygonCollider2D>().enabled = true;
            LsIteams.Clear();
            //if (!isPlay)
            //{
            //    isPlay = true;
            //}
            //isOpen = true;
            this.isDown = true;
            initialPosition = transform.position;
            RotateRight();
            RotateLeft();

        }
        private void Update()
        {

            //if (ClawGameManager.Instance.statusGame == StatusGame.Store) return;
            Down();
            MoveMent();
        }

        public void MoveMent()
        {
            Vector2 temp = transform.position;
            temp.x += JoyStick.joystickVec.x * speedHook * Time.deltaTime;
            temp.x = Mathf.Clamp(temp.x, ClawGameManager.Instance.leftPos.position.x, ClawGameManager.Instance.rightPos.position.x);
            transform.position = temp;
        }
        private void Down()
        {
            //if (!isPlay || !isDown) return;
            if ( !isDown) return;
            Vector3 temp = transform.position;
            temp.y -= speedDown * Time.deltaTime;
            transform.position = temp;
        }
        public void MoveBack()
        {
            if (moveBack != null) StopCoroutine(moveBack);
            moveBack = StartCoroutine(ResetPositionAfterDelay(0.5f));
        }
        Coroutine moveBack;
        private IEnumerator ResetPositionAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            //rb.velocity = Vector3.zero; // Đặt vận tốc về 0 để tránh di chuyển không mong muốn
            //transform.position = initialPosition;
            hookTweener = transform.DOMoveY(initialPosition.y, 1.5f, false)
                        .SetEase(Ease.OutCubic)
         .SetSpeedBased(true)
         .OnUpdate(delegate
         {
         }).OnComplete(delegate
         {
            // isPlay = false;
             DoneClaw();
         });
        }

        private float rotationSpeed = 100f; // Vận tốc quay (độ trên giây)
        private float rotationBackSpeed = 60f;
        private float currentZRotation = 0f; // Góc quay hiện tại
        private float currentZRotationLeft = 0f;

        void rotate()
        {
            float currentZL = LcLaw.transform.eulerAngles.z;
            //Debug.Log("zl"+ currentZL);
            if (currentZL > 315f || currentZL < 45f)
            {
                //LcLaw.transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
                LcLaw.transform.Rotate(0f, 0f, -1f);
            }
            float currentZ = Rclaw.transform.eulerAngles.z;
            //Debug.Log("Rl" + currentZ);
            if (currentZ < 45f)
            {
                //Rclaw.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
                Rclaw.transform.Rotate(0f, 0f, 1f);
            }
        }
        private void RotateRight()
        {
            Vector3 targetRotation = new Vector3(0f, 0f, 45f);
            Rclaw.transform.DORotate(targetRotation, (45f - currentZRotation) / rotationSpeed)
                .SetEase(Ease.Linear) // Sử dụng easing tuyến tính
                .OnUpdate(() =>
                {
                    currentZRotation = Rclaw.transform.eulerAngles.z;
                });
        }
        private void RotateLeft()
        {
            Vector3 targetRotation = new Vector3(0f, 0f, -45f);
            LcLaw.transform.DORotate(targetRotation, (45f - currentZRotation) / rotationSpeed)
                .SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    currentZRotationLeft = LcLaw.transform.eulerAngles.z;
                });
        }
        private void RotateBackRight()
        {
            Rclaw.transform.DORotate(Vector3.zero, (currentZRotation) / rotationBackSpeed)
             .SetEase(Ease.Linear)
            .OnUpdate(() =>
             {
                 currentZRotation = Rclaw.transform.eulerAngles.z;
             })
            .OnComplete(() => { MoveBack(); });

        }
        private void RotateBackLeft()
        {
            LcLaw.transform.DORotate(Vector3.zero, (currentZRotation) / rotationBackSpeed)
             .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                currentZRotationLeft = LcLaw.transform.eulerAngles.z;
            });
        }
        private void DoneRotate()
        {
            float currentZL = LcLaw.transform.eulerAngles.z;
            float currentZ = Rclaw.transform.eulerAngles.z;
            //Debug.Log("zlllll" + currentZL);
            //Debug.Log("zlllll" + currentZ);

            if (currentZL >= 315f)
            {
                LcLaw.transform.Rotate(0f, 0f, 0.5f);
            }
            if (currentZ <= 315f)
            {
                Rclaw.transform.Rotate(0f, 0f, -0.5f);
                //Debug.Log("a");
            }

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("ground"))
            {
                this.isDown = false;
                //isOpen = false;
                RotateBackRight();
                RotateBackLeft();
            }
            if (collision.gameObject.CompareTag("Coin"))
            {
                this.LsIteams.Add(collision.GetComponent<Iteam>());
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Coin"))
            {
                this.LsIteams.Remove(collision.GetComponent<Iteam>());
            }
        }
        public void DoneClaw()
        {
            moveDown.DoneMove();
            //ClawGameManager.Instance.DonePress();
            //SoundController.instance.PlayContinueMusicBackGround();
            ShowGift();
        }

        public void MoveIteam(int number,Action action = null)
        {
            for (int i = 0; i < this.LsIteams.Count; i++)
            {
                this.LsIteams[i].SetIteam(number);
            }
            for (int i = 0; i < this.LsIteams.Count; i++)
            {
                this.LsIteams[i].MoveIteam(action);
            }
            GetComponent<CircleCollider2D>().enabled = false;
            Rclaw.GetComponent<PolygonCollider2D>().enabled = false;
            LcLaw.GetComponent<PolygonCollider2D>().enabled = false;
        }
        public void SetOffCollider()
        {
            ClawGameManager.Instance.ResetBabyAfterWin();
            //if (ClawGameManager.Instance.number >= ClawGameManager.Instance.numberCandyandBaby)
            //{
            //    ClawGameManager.Instance.ResetBabyAfterWin();
            //}
        }
        public void ShowGift()
        {
            if (this.LsIteams.Count <= 0)
            {
                GetComponent<CircleCollider2D>().enabled = false;
                Rclaw.GetComponent<PolygonCollider2D>().enabled = false;
                LcLaw.GetComponent<PolygonCollider2D>().enabled = false;
                SceneController.Instance.statusGame = StatusGame.StartGame;
                //ClawGameManager.Instance.canClaw = false;
                return;
            }
            //if (CheckOnOffGift())
            //{
            //    for (int i = 0; i < this.LsIteams.Count; i++)
            //    {
            //        this.LsIteams[i].SetIteam();
            //    }   
            //}
            //else
            //{
            //    for (int i = 0; i < this.LsIteams.Count; i++)
            //    {
            //        this.LsIteams[i].SetIteam();
            //        this.LsIteams[i].MoveIteam();
            //    }
            //    GetComponent<CircleCollider2D>().enabled = false;
            //    Rclaw.GetComponent<PolygonCollider2D>().enabled = false;
            //    LcLaw.GetComponent<PolygonCollider2D>().enabled = false;
            //    SceneController.Instance.statusGame = StatusGame.StartGame;
            //    return;
            //}
            if (!CheckOnOffGift())
            {
                for (int i = 0; i < this.LsIteams.Count; i++)
                {
                    this.LsIteams[i].SetIteam(0);
                    this.LsIteams[i].MoveIteam();
                }
                GetComponent<CircleCollider2D>().enabled = false;
                Rclaw.GetComponent<PolygonCollider2D>().enabled = false;
                LcLaw.GetComponent<PolygonCollider2D>().enabled = false;
                SceneController.Instance.statusGame = StatusGame.StartGame;
                return;
            }
            PopupManager.Instance.OnUI(PopupCode.GiftPanel);
        }

        public bool CheckOnOffGift()
        {
            for (int i = 0; i < this.LsIteams.Count; i++)
            {
                if (this.LsIteams[i].typeIteam != TypeIteam.coin)
                {
                    return true;
                }
            }
            return false;
        }
        //    private void OnTriggerStay2D(Collider2D collision)
        //    {

        //        if (!isPlay)
        //        {
        //            if (collision.gameObject.CompareTag("candy"))
        //            {
        //                collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        //                collision.GetComponent<BoxCollider2D>().enabled = false;
        //                string id = collision.GetComponent<Candy>().CandyData.Id;
        //                UserManager.instance.SetCandyOnStock(id);
        //                UserManager.instance.SaveHeroData();
        //                collision.gameObject.tag = "Processed";
        //                ClawGameManager.Instance.MoveObjectInGame(collision.gameObject);
        //                ClawGameManager.Instance.number++;
        //                //collision.enabled = false;
        //            }
        //            else if (collision.gameObject.CompareTag("animal"))
        //            {
        //                collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        //                collision.GetComponent<CapsuleCollider2D>().enabled = false;
        //                string id = collision.GetComponent<Animal>().animalData.Id;
        //                UserManager.instance.SetAnimalOnStock(id);
        //                UserManager.instance.SaveHeroData();
        //                collision.gameObject.tag = "Processed";
        //                ClawGameManager.Instance.MoveObjectInGame(collision.gameObject);
        //                ClawGameManager.Instance.number++;
        //                UserManager.instance.useData.gold += 2;

        //            }
        //            else if (collision.gameObject.CompareTag("Coin"))
        //            {
        //                collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        //                collision.GetComponent<CircleCollider2D>().enabled = false;
        //                collision.gameObject.tag = "Processed";
        //                ClawGameManager.Instance.MoveObjectInGame(collision.gameObject);
        //                UserManager.instance.useData.gold += 1;

        //            }
        //            ClawGameManager.Instance.InitGold(UserManager.instance.useData.gold);
        //            UserManager.instance.SaveHeroData();
        //        }
        //    }

    }
}