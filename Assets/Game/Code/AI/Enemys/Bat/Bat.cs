using Assets.Game.Code.Character;
using Assets.Game.Code.Interfaces;
using UnityEngine;

namespace Assets.Game.Code.AI.Enemys.Bat
{
    public class Bat : MonoBehaviour, IDamageble, IDieble
    {
        [SerializeField]
        private int hp;
        [SerializeField]
        private Transform[] points;
        [SerializeField]
        private GameObject dangerousZone;

        private Rigidbody2D rb;
        private Animator animator;

        private Transform currentTarget;
        private int currentTargetIndex = 0;

        private float speed = 2;
        private Vector2 direction;

        private bool isAlive => hp > 0;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            currentTarget = points[currentTargetIndex];

            foreach (Transform point in points)
            {
                point.parent = null;
            }
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            if (!isAlive) return;

            Vector2 newPosition = Vector2.MoveTowards(rb.position, currentTarget.position, speed * Time.fixedDeltaTime);

            rb.MovePosition(newPosition);

            if (Vector2.Distance(rb.position, currentTarget.position) < 0.1f)
            {
                // Next target index
                currentTargetIndex += 1;
                if(currentTargetIndex == points.Length)
                {
                    currentTargetIndex = 0;
                }

                // Next target
                currentTarget = points[currentTargetIndex];

                // Watch direction
                direction = (Vector2)points[currentTargetIndex].position - rb.position;
                transform.localScale = new Vector3((direction.x < 0) ? -1 : 1, 1, 1);

                //if (direction.x < 0)
                //{
                //    transform.localScale = new Vector3(-1, 1, 1);
                //}
                //else
                //{
                //    transform.localScale = new Vector3(1, 1, 1);
                //}
            }
        }

        public void TakeDamage(int damage)
        {
            if (!isAlive) return;

            hp -= damage;
            if (hp < 0)
            {
                hp = 0;
                Die();
            }
        }

        public void Die()
        {
            hp = 0;
            animator.SetTrigger("Die");
            Destroy(dangerousZone);
        }
    }
}