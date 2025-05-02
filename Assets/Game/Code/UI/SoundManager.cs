using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Game.Code.UI
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private Image foreground;
        [SerializeField]
        private AudioSource audioTrack;
        [Space]
        [SerializeField]
        private Sprite musicOn;
        [SerializeField]
        private Sprite musicOff;

        private bool musicPlaying = true;

        public void ToggleMusic()
        {
            musicPlaying = !musicPlaying;

            if (musicPlaying)
            {
                foreground.sprite = musicOn;
            }
            else
            {
                foreground.sprite = musicOff;
            }

            audioTrack.mute = !musicPlaying;

            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}