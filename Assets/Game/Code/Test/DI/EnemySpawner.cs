using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Code.Test.DI
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform[] points;
        [SerializeField]
        private GameObject enemyPrefab;

        private List<GameObject> enemys = new();

        public void SpawnEnemys()
        {
            foreach (Transform point in points)
            {
                GameObject enemy = Instantiate(enemyPrefab, point.position, Quaternion.identity);

                enemys.Add(enemy);
            }

            print("Enemys spawned");
        }

        public void RemoveEnemys()
        {
            foreach (GameObject enemy in enemys)
            {
                Destroy(enemy);
            }

            enemys.Clear();
            print("Enemys removed");
        }
    }
}