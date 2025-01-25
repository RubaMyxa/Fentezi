using Assets.Game.Code.Test.Initialize;
using UnityEngine;

namespace Assets.Game.Code.Test.DI
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private GameObject enemySpawnerPrefab;
        [SerializeField]
        private Transform enemySpawnerPoint;
        [Space]
        [SerializeField]
        private Crystal crystal;

        public GameObject EnemySpawner { get; private set; }

        private void Awake()
        {
            EnemySpawner = Instantiate(enemySpawnerPrefab, enemySpawnerPoint.position, Quaternion.identity);

            // Инициализация
            crystal.Construct(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SpawnEnemys();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                RemoveEnemys();
            }
        }

        public void SpawnEnemys()
        {
            EnemySpawner.GetComponent<EnemySpawner>().SpawnEnemys();
        }

        public void RemoveEnemys()
        {
            EnemySpawner.GetComponent<EnemySpawner>().RemoveEnemys();
        }
    }
}