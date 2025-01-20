using UnityEngine;

namespace Assets.Game.Code.Test
{
    public class Portal : MonoBehaviour
    {
        [SerializeField]
        private Color color;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }
}