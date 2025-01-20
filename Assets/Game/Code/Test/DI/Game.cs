using UnityEngine;

namespace Assets.Game.Code.Test.DI
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private GameObject enemySpawnerPrefab;
        [SerializeField]
        private Transform enemySpawnerPoint;

        public GameObject EnemySpawner { get; private set; }

        private void Awake()
        {
            EnemySpawner = Instantiate(enemySpawnerPrefab, enemySpawnerPoint.position, Quaternion.identity);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                EnemySpawner.GetComponent<EnemySpawner>().SpawnEnemys();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                EnemySpawner.GetComponent<EnemySpawner>().RemoveEnemys();
            }
        }
    }
}