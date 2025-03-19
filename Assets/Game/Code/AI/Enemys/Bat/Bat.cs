using Assets.Game.Code.Interfaces;
using UnityEngine;
using UnityEngine.Splines;

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
        [SerializeField]
        private SplineContainer spline;
        [SerializeField]
        private float speed;

        private Rigidbody2D rb;
        private Animator animator;

        private Transform currentTarget;
        private int currentTargetIndex = 0;

        private float alpha = 0;
        private Vector2 direction;
        private Vector2 startPos;

        private bool isAlive => hp > 0;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            startPos = transform.position;

            currentTarget = points[currentTargetIndex];

            foreach (Transform point in points)
            {
                point.parent = null;
            }

            spline.transform.parent = null;
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void SplineMovement()
        {
            alpha += Time.deltaTime / 10 * speed;
            if (alpha >= 1)
            {
                alpha = 0;
            }

            Vector3 pos = spline.EvaluatePosition(alpha);

            transform.position = pos;
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
            }
        }

        public DefeatedObject TakeDamage(int damage)
        {
            if (!isAlive) return DefeatedObject.None;

            hp -= damage;
            if (hp < 0)
            {
                hp = 0;
                Die();

                return DefeatedObject.Enemy;
            }

            return DefeatedObject.None;
        }

        public void Die()
        {
            hp = 0;
            rb.bodyType = RigidbodyType2D.Dynamic;
            animator.SetTrigger("Die");
            Destroy(dangerousZone);
        }
    }
}