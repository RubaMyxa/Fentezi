using UnityEngine;
using TMPro;
using Assets.Game.Code.Character;
using System.Collections;

namespace Assets.Game.Code.UI
{
    public class TutorialHud : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup curtain;
        [Space]
        [SerializeField]
        private TextMeshProUGUI tutorialTextBox;

        private Coroutine coroutine;

        public void Open(string tutorialText)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            coroutine = StartCoroutine(OpenCoroutine(tutorialText));
        }

        public void Hide()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            coroutine = StartCoroutine(HideCoroutine());
        }

        private IEnumerator OpenCoroutine(string tutorialText)
        {
            Player.TutorialOnOff(true);
            tutorialTextBox.text = tutorialText;
            curtain.alpha = 0f;

            while (curtain.alpha < 1)
            {
                yield return new WaitForSeconds(0.01f);
                curtain.alpha += 0.02f;
            }

            curtain.blocksRaycasts = true;
        }

        private IEnumerator HideCoroutine()
        {
            Player.TutorialOnOff(false);
            curtain.alpha = 1.0f;

            while (curtain.alpha > 0)
            {
                yield return new WaitForSeconds(0.01f);
                curtain.alpha -= 0.02f;
            }

            curtain.blocksRaycasts = false;
        }
    }
}