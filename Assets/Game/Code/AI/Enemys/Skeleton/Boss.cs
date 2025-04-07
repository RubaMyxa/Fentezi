using UnityEngine;
using Assets.Game.Code.Interfaces;
using Assets.Game.Code.UI;
using System.Collections;

namespace Assets.Game.Code.AI.Enemys.Skeleton
{
    public class Boss : MonoBehaviour, IDamageble, IDieble
    {
        [Header("Parameters")]
        [SerializeField]
        private Transform[] points;
        [SerializeField]
        private int hp;
        [SerializeField]
        protected float movementSpeed;
        [Space]
        [SerializeField]
        private HpBar hpBar;

        private Rigidbody2D rb;
        private Animator animator;

        private int currentHp;
        private int maxHp;
        private int direction = 0;
        private Transform currentTarget;
        private int currentTargetIndex = 0;
        private float waitingTimer, attackTimer = 4f;
        private Coroutine waitingCoroutine;

        private Vector3 currentVelocity = Vector3.zero;
        private BehaviourBossAI behaviourBossAI = BehaviourBossAI.Patrolling;

        private bool IsAlive => currentHp > 0;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            currentHp = hp;
            maxHp = currentHp;

            // Target init
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

        private void Update()
        {
            Patrolling();
            Attacking();
        }

        private void Patrolling()
        {
            if (behaviourBossAI != BehaviourBossAI.Patrolling)
            {
                return;
            }

            Move(direction);
            TargetDetector();
        }

        private void Attacking()
        {
            if (behaviourBossAI == BehaviourBossAI.Die)
            {
                return;
            }

            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attackTimer = 100f; // For bug fix. Dont do it.

                if (waitingCoroutine != null)
                {
                    StopCoroutine(waitingCoroutine);
                }

                StopMovement();
                behaviourBossAI = BehaviourBossAI.Attack;
                animator.SetTrigger("Attack");
            }
        }

        private void AttackEnd()
        {
            behaviourBossAI = BehaviourBossAI.Patrolling;
            attackTimer = 4f;
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

            animator.SetFloat("horizontal", Mathf.Abs(direction));
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

                // Waiting timer
                waitingCoroutine = StartCoroutine(WaitingTimer());
            }
        }

        private IEnumerator WaitingTimer()
        {
            StopMovement();
            behaviourBossAI = BehaviourBossAI.Waiting;

            yield return new WaitForSeconds(2f);

            behaviourBossAI = BehaviourBossAI.Patrolling;
        }

        private void CalculateDirection()
        {
            if (currentTarget.position.x - transform.position.x < 0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
        }

        private void StopMovement()
        {
            Move(0);
            rb.linearVelocity = Vector2.zero;
        }

        public DefeatedObject TakeDamage(int damage)
        {
            if (!IsAlive) return DefeatedObject.None;

            currentHp -= damage;
            hpBar.HpBarUpdate(currentHp, maxHp);

            if (currentHp < 0)
            {
                currentHp = 0;
                Die();

                return DefeatedObject.Enemy;
            }
            
            return DefeatedObject.None;
        }

        public void Die()
        {
            StopMovement();
            behaviourBossAI = BehaviourBossAI.Die;
            currentHp = 0;
            hpBar.HpBarUpdate(currentHp, maxHp);

            animator.SetTrigger("Die");
        }
    }
}