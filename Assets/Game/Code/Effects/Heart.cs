using Assets.Game.Code.Character;
using UnityEngine;

namespace Assets.Game.Code.Effects
{
    public class Heart : MonoBehaviour
    {
        [SerializeField]
        private HeartType heartType;
        [SerializeField]
        [Range(5f, 20f)]
        private float respawnRate = 5f;

        private bool canBeTaken = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && canBeTaken)
            {
                collision.GetComponent<Player>().HealToMax();

                if (heartType == HeartType.Single)
                {
                    Destroy(gameObject);
                }
                else // Multiple
                {
                    canBeTaken = false;
                    GetComponent<SpriteRenderer>().enabled = false;

                    Invoke("ResetHeart", respawnRate);
                }
            }
        }

        private void ResetHeart()
        {
            canBeTaken = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}