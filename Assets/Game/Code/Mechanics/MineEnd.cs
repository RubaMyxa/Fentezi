using Assets.Game.Code.Infrastacture;
using UnityEngine;

namespace Assets.Game.Code.Mechanics
{
    public class MineEnd : MonoBehaviour
    {
        private LevelManager levelManager;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                levelManager.OpenNextLevel();
            }
        }

        public void Construct(LevelManager levelManager)
        {
            this.levelManager = levelManager;
        }
    }
}