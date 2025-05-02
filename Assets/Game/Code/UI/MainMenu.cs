using Assets.Game.Code.Effects;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Assets.Game.Code.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject soundManager;
        [Space]
        [SerializeField]
        private Image toggleTutorialBackground;
        [SerializeField]
        private TextMeshProUGUI toggleTutorialText;
        [SerializeField]
        private Sprite tutorialOnBackground;
        [SerializeField]
        private Sprite tutorialOffBackground;
        [Space]
        [SerializeField]
        private CanvasGroup creditsPanel;
        [SerializeField]
        private FadeInOut fade;
        [SerializeField]
        private string firstLevel;

        private Coroutine creditsCoroutine;

        public static bool ToggleTutorial = true;

        private void Awake()
        {
            fade.FadeOut();

            creditsPanel.alpha = 0f;
            creditsPanel.blocksRaycasts = false;

            UpdateButtonData();

            GameObject sm = Instantiate(soundManager);
            DontDestroyOnLoad(sm);
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

        public void SwitchTutorial()
        {
            ToggleTutorial = !ToggleTutorial;

            UpdateButtonData();

            EventSystem.current.SetSelectedGameObject(null);
        }

        private void UpdateButtonData()
        {
            if (ToggleTutorial)
            {
                toggleTutorialBackground.sprite = tutorialOnBackground;
                toggleTutorialText.text = "Tutorial ON";
            }
            else
            {
                toggleTutorialBackground.sprite = tutorialOffBackground;
                toggleTutorialText.text = "Tutorial OFF";
            }
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