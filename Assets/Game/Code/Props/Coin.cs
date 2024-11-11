using UnityEngine;

namespace Assets.Game.Code.Props
{
    public class Coin : MonoBehaviour
    {
        [SerializeField]
        private GameObject coinEffect;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Instantiate(coinEffect, gameObject.transform.position, Quaternion.identity);

                Debug.Log("+1 coin");
                Destroy(gameObject);
            }
        }
    }
}