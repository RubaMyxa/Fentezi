using Assets.Game.Code.Interfaces;
using UnityEngine;

namespace Assets.Game.Code.Mechanics
{
    public class KillZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if (collision.CompareTag("Player"))
            //{
            //    collision.GetComponent<Player>().Die();
            //}

            IDieble handler;

            collision.TryGetComponent(out handler);

            if (handler != null)
            {
                handler.Die();
            }
            //handler?.Die();
        }
    }
}