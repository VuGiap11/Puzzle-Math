using DG.Tweening;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class MoveObject : MonoBehaviour
    {
        [SerializeField] private GameObject _Object;
        [SerializeField] Transform target;
        [SerializeField] Transform startPos;
        [SerializeField] private float timeMove = 1f;
        public void MoveDown()
        {
            _Object.transform.transform.DOMove(target.position, timeMove)
            .SetEase(Ease.Linear)
            .SetDelay(0f)
            .OnComplete(() =>
            {
            });
        }
        public void MoveUp()
        {
            _Object.transform.transform.DOMove(startPos.position, timeMove)
            .SetEase(Ease.Linear)
            .SetDelay(0f)
            .OnComplete(() =>
            {
            });
        }
    }
}
