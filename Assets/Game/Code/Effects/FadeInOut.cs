using System;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Code.Effects
{
    public class FadeInOut : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup fade;

        public void FadeIn(Action OnBlack = null)
        {
            StartCoroutine(FadeInCoroutine(OnBlack));
        }

        public void FadeOut()
        {
            StartCoroutine(FadeOutCoroutine());
        }

        private IEnumerator FadeInCoroutine(Action OnBlack)
        {
            fade.alpha = 0f;

            while (fade.alpha < 1) {
                yield return new WaitForSeconds(0.01f);
                fade.alpha += 0.02f;
            }

            OnBlack?.Invoke();
        }

        private IEnumerator FadeOutCoroutine()
        {
            fade.alpha = 1.0f;

            while (fade.alpha > 0)
            {
                yield return new WaitForSeconds(0.01f);
                fade.alpha -= 0.02f;
            }
        }
    }
}