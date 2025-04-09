using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Code.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField]
        private Image hpBar;
        [SerializeField]
        private Image staminaBar;

        public void HpBarUpdate(float currentHp, float maxHp)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }

        public void StaminaBarUpdate(float currentStaminaAmount, float maxStaminaAmount)
        {
            staminaBar.fillAmount = 1 - currentStaminaAmount / maxStaminaAmount;
        }
    }
}