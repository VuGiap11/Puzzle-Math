using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
     public Transform obj; // Đối tượng cần di chuyển và scale
    public Transform pointA; // Vị trí bắt đầu
    public Transform pointB; // Vị trí kết thúc
    public float duration = 1f; // Thời gian thực hiện hiệu ứng

    void Start()
    {
        //MoveAndScaleObject();
    }

    [ContextMenu("MoveAndScaleObject")]
    void MoveAndScaleObject()
    {
        obj.position = pointA.position; // Đặt vật thể ở vị trí ban đầu
        obj.localScale = Vector3.zero; // Đặt scale về (0,0,0)

        // Tạo một Sequence để thực hiện 2 hiệu ứng đồng thời
        DOTween.Sequence()
            .Append(obj.DOMove(pointB.position, duration).SetEase(Ease.InOutQuad)) // Di chuyển từ A đến B
            .Join(obj.DOScale(Vector3.one, duration).SetEase(Ease.OutBack)); // Scale từ (0,0,0) đến (1,1,1)
    }
}
