using UnityEngine;

namespace Assets.Game.Code.UI
{
    public class TutorialTrigger : MonoBehaviour
    {
        [SerializeField]
        private TutorialHud tutorialHud;
        [Space]
        [SerializeField]
        private string tutorialText;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                tutorialHud.Open(tutorialText);

                Destroy(gameObject);
            }
        }
    }
}