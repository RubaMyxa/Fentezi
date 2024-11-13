using UnityEngine;

namespace Assets.Game.Code.Props
{
    public class Key : MonoBehaviour
    {
        [SerializeField]
        private GameObject keyEffect;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Instantiate(keyEffect, gameObject.transform.position, Quaternion.identity);

                Debug.Log("+1 key");
                Destroy(gameObject);
            }
        }
    }
}