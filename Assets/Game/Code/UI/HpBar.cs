using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Code.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField]
        private Image bar;

        public void HpBarUpdate(float currentHp, float maxHp)
        {
            bar.fillAmount = currentHp / maxHp;
        }
    }
}