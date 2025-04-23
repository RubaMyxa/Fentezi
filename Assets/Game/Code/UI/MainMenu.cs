using Assets.Game.Code.Effects;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Game.Code.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup creditsPanel;
        [SerializeField]
        private FadeInOut fade;
        [SerializeField]
        private string firstLevel;

        private Coroutine creditsCoroutine;

        private void Awake()
        {
            fade.FadeOut();

            creditsPanel.alpha = 0f;
            creditsPanel.blocksRaycasts = false;
        }

        public void Play()
        {
            fade.FadeIn(() =>
            {
                SceneManager.LoadScene(firstLevel);
            });
        }

        public void Credits()
        {
            if (creditsCoroutine != null)
            {
                StopCoroutine(creditsCoroutine);
            }

            creditsCoroutine = StartCoroutine(CreditsShowCoroutine());
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void CloseCredits()
        {
            if (creditsCoroutine != null)
            {
                StopCoroutine(creditsCoroutine);
            }

            creditsCoroutine = StartCoroutine(CreditsHideCoroutine());
        }

        private IEnumerator CreditsShowCoroutine()
        {
            creditsPanel.blocksRaycasts = true;

            while (creditsPanel.alpha < 1f)
            {
                yield return new WaitForSeconds(0.01f);
                creditsPanel.alpha += 0.04f;
            }
        }

        private IEnumerator CreditsHideCoroutine()
        {
            creditsPanel.blocksRaycasts = false;

            while (creditsPanel.alpha > 0f)
            {
                yield return new WaitForSeconds(0.01f);
                creditsPanel.alpha -= 0.04f;
            }
        }
    }
}