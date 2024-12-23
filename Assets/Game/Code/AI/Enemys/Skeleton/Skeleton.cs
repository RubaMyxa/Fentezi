using UnityEngine;

namespace Assets.Game.Code.AI.Enemys.Skeleton
{
    public class Skeleton : MonoBehaviour
    {
        [SerializeField]
        private Transform[] points;
        [Space]
        [SerializeField]
        private float movementSpeed;

        private Rigidbody2D rb;
        private Animator animator;

        private Vector3 currentVelocity = Vector3.zero;
        private Transform currentTarget;
        private int currentTargetIndex = 0;
        private int direction = 0;

        private int HorizontalHash = Animator.StringToHash("horizontal");

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            currentTarget = points[currentTargetIndex];
            //direction = (currentTarget.position.x - transform.position.x < 0) ? -1 : 1;
            if (currentTarget.position.x - transform.position.x < 0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            foreach (Transform point in points)
            {
                point.parent = null;
            }
        }

        private void Update()
        {
            Move(direction);
            TargetDetector();
        }

        private void Move(int direction)
        {
            Vector3 targetVelocity = new Vector2(direction * movementSpeed, rb.linearVelocityY);
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref currentVelocity, 0.05f);

            if (direction < 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
            else if (direction > 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }

            animator.SetFloat(HorizontalHash, Mathf.Abs(direction));
        }

        private void TargetDetector()
        {
            if (Vector2.Distance(rb.position, currentTarget.position) < 0.1f)
            {
                // Next target index
                currentTargetIndex += 1;
                if (currentTargetIndex == points.Length)
                {
                    currentTargetIndex = 0;
                }

                // Next target
                currentTarget = points[currentTargetIndex];

                // Calculate direction
                if (currentTarget.position.x - transform.position.x < 0)
                {
                    direction = -1;
                }
                else
                {
                    direction = 1;
                }
            }
        }
    }
}