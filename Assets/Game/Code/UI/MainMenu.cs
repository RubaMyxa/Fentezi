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
            print("Credits");
        }

        public void Exit()
        {
            Application.Quit();
        }

        private IEnumerator CreditsShowCoroutine()
        {
            yield return new WaitForSeconds(0.01f);
        }

        private IEnumerator CreditsHideCoroutine()
        {
            yield return new WaitForSeconds(0.01f);
        }
    }
}