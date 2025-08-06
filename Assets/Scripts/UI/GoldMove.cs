using DG.Tweening;
using Rubik.math;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.math
{
    public class GoldMove : MonoBehaviour
    {
        public PathType pathType = PathType.CatmullRom; 
        public Transform pointA; // Điểm đầu
       // public Transform pointB; // Điểm cuối (có thể thay đổi)
        private float duration = 1.5f; // Thời gian di chuyển
         public void CreateSmoothCurve(Transform pointB, Action action = null)
        {
            //Vector3 controlPoint1 = pointA.position + (pointB.position - pointA.position) / 3 + Vector3.up * 2;
            //Vector3 controlPoint2 = pointA.position + 2 * (pointB.position - pointA.position) / 3 + Vector3.up * 2;

            //Vector3[] pathPoints = new Vector3[] { pointA.position, controlPoint1, controlPoint2, pointB.position };
            //transform.DOPath(pathPoints, duration, pathType)
            //         .SetEase(Ease.Linear)
            //         //.SetSpeedBased()
            //         .OnComplete(() =>
            //         { 
            //         });
            Vector3 controlPoint1 = pointA.position + (pointB.position - pointA.position) / 3 + Vector3.up * 2;
            Vector3 controlPoint2 = pointA.position + 2 * (pointB.position - pointA.position) / 3 + Vector3.up * 2;

            Vector3[] pathPoints = new Vector3[] { pointA.position, controlPoint1, controlPoint2, pointB.position };
            transform.DOPath(pathPoints, duration, pathType)
                     .SetEase(Ease.Linear)
                     //.SetSpeedBased()
                     .OnComplete(() =>
                     {
                         if (action != null)
                         {
                             action.Invoke();
                         }
                         Destroy(gameObject);
                     });
        }
    }
}