
using UnityEngine.UI;
using UnityEngine;

namespace Rubik.LuckyGame
{

    public class TimeBar : MonoBehaviour
    {
        [SerializeField] private Image _hpBarSprite;
        public void UpdateHpBar(float maxHp, float curHp)
        {
            if (maxHp <= 0) return;
            //_hpBarSprite.fillAmount = curHp / maxHp;

            _hpBarSprite.fillAmount = Mathf.Round(curHp / maxHp * 1000f) / 1000f;

            //Debug.Log((float)curHp / (float)maxHp);
        }
    }
}
