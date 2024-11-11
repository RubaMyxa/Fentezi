using UnityEngine;

namespace Assets.Game.Code.Props
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("+1 coin");
                Destroy(gameObject);
            }
        }
    }
}