using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Code.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image fillImage;

        public void SetProgressBarAmount(float fillAmount)
        {
            fillImage.fillAmount = fillAmount;
        }
    }
}