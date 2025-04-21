using Assets.Game.Code.Effects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Game.Code.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private FadeInOut fade;
        [SerializeField]
        private string firstLevel;

        private void Awake()
        {
            fade.FadeOut();
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
    }
}