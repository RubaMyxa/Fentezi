using Assets.Game.Code.Character;
using UnityEngine;

namespace Assets.Game.Code.Effects
{
    public class Heart : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Player>().HealToMax();

                Destroy(gameObject);
            }
        }
    }
}