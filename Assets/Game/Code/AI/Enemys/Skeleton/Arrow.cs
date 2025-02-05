using Assets.Game.Code.Character;
using UnityEngine;

namespace Assets.Game.Code.AI.Enemys.Skeleton
{
    public class Arrow : MonoBehaviour
    {
        private float speed = 4f;

        private void Update()
        {
            Movement();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                collision.GetComponent<Player>().TakeDamage(25, transform.position);
                Destroy(gameObject);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Destroy(gameObject);
            }
        }

        private void Movement()
        {
            transform.position = new Vector3(
                transform.position.x + speed * Time.deltaTime * transform.localScale.x,
                transform.position.y,
                transform.position.z);
        }
    }
}