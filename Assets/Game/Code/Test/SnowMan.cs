using System;
using UnityEngine;

namespace Assets.Game.Code.Test
{
    public class SnowMan : MonoBehaviour
    {
        [SerializeField]
        private Transform snowManSpawn;
        [SerializeField]
        private GameObject snowmanPrefab;

        public void Print()
        {
            print(transform.position);
        }

        public void SnowManDestroy()
        {
            Destroy(gameObject);
        }

        public void Teleport(Vector2 target)
        {
            transform.position = target;
        }

        public void Scale(Vector2 target)
        {
            transform.localScale = target;
        }

        public void SnowManDestroyTrigger()
        {
            Destroy(GetComponent<BoxCollider2D>());
        }

        public void Spawn()
        {
            Instantiate(snowmanPrefab, snowManSpawn.position, Quaternion.identity);
        }
    }
}