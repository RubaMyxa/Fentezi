using Assets.Game.Code.Character;
using Assets.Game.Code.Effects;
using Assets.Game.Code.Mechanics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Game.Code.Infrastacture
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private FollowCamera followCamera;
        [SerializeField]
        private FadeInOut fadeInOut;

        [Space]
        [SerializeField]
        private GameObject mineStart;
        [SerializeField]
        private MineEnd mineEnd;

        [Space]
        [SerializeField]
        private string nextLevel;

        private Player activePlayer;

        private int keysToNextLevel = 3;

        private void Awake()
        {
            PlayerSpawn();

            fadeInOut.FadeOut();
            mineEnd.Construct(this);
        }

        private void PlayerSpawn()
        {
            GameObject spawnedPlayer = Instantiate(player, mineStart.transform.position, Quaternion.identity);
            followCamera.SetTarget(spawnedPlayer.transform);

            activePlayer = spawnedPlayer.GetComponent<Player>();
        }

        public void OpenNextLevel()
        {
            if (activePlayer.GetKeys() == keysToNextLevel)
            {
                fadeInOut.FadeIn(() => SceneManager.LoadScene(nextLevel));
            }
        }
    }
}