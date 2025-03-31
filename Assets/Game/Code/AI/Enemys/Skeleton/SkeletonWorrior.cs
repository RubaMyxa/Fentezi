using Assets.Game.Code.Character;
using UnityEngine;

namespace Assets.Game.Code.AI.Enemys.Skeleton
{
    public class SkeletonWorrior : SkeletonBase
    {
        [SerializeField]
        private Transform[] points;

        private Transform currentTarget;
        private int currentTargetIndex = 0;

        private int HorizontalHash = Animator.StringToHash("horizontal");

        protected override void Awake()
        {
            base.Awake();

            currentTarget = points[currentTargetIndex];
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

        protected override void Update()
        {
            base.Update();

            Patrolling(direction, GetSetBehaviourAI);
        }

        private void Patrolling(int direction, BehaviourAI behaviourAI)
        {
            if (behaviourAI != BehaviourAI.Patrolling)
            {
                return;
            }

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
            if (Vector2.Distance(rb.position, currentTarget.position) < 0.25f)
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
                CalculateDirection();
            }
        }

        protected override void CalculateDirection()
        {
            base.CalculateDirection();

            if (currentTarget.position.x - transform.position.x < 0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
        }

        private void Attack()
        {
            Collider2D playerCollider = Physics2D.OverlapBox(attackPoint.position, new Vector2(1.5f, 1.5f), 0f, playerLayer);

            if (playerCollider)
            {
                playerCollider.GetComponent<Player>().TakeDamage(20, transform.position);
            }

            print("Attack");
            Invoke("AttackEnd", attackCooldown);
        }

        private void StopMovement()
        {
            Move(0);
        }
    }
}