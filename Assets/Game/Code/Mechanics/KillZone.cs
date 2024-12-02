using Assets.Game.Code.Character;
using UnityEngine;

namespace Assets.Game.Code.Mechanics
{
    public class KillZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Player>().Die();
            }
        }
    }
}