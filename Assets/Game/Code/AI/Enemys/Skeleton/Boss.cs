using UnityEngine;
using Assets.Game.Code.Interfaces;
using Assets.Game.Code.UI;
using System.Collections;
using Assets.Game.Code.Character;

namespace Assets.Game.Code.AI.Enemys.Skeleton
{
    public class Boss : MonoBehaviour, IDamageble, IDieble
    {
        [SerializeField]
        private Transform hitPoint1;
        [SerializeField]
        private Transform hitPoint2;
        [SerializeField]
        private LayerMask hittableLayer;
        [Header("Parameters")]
        [SerializeField]
        private Transform[] points;
        [SerializeField]
        private int hp;
        [SerializeField]
        private float movementSpeed;
        [SerializeField]
        private float attackCooldown;
        [Space]
        [SerializeField]
        private HpBar hpBar;

        private Rigidbody2D rb;
        private Animator animator;
        private Player player;

        private int currentHp;
        private int maxHp;
        private int direction = 0;
        private Transform currentTarget;
        private int currentTargetIndex = 0;
        private float waitingTimer, attackTimer;
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
            attackTimer = attackCooldown;

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

        private void Start()
        {
            player = FindFirstObjectByType<Player>();
        }

        private void Update()
        {
            Patrolling();
            Attacking();
        }

        private void Patrolling()
        {
            hpBar.StaminaBarUpdate(attackTimer, attackCooldown);

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

                CalculateDirection(player.transform);
                transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
            }
        }

        private void AttackEnd()
        {
            CalculateDirection(currentTarget);
            behaviourBossAI = BehaviourBossAI.Patrolling;
            attackTimer = attackCooldown;
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
                CalculateDirection(currentTarget);

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

        private void CalculateDirection(Transform target)
        {
            if (target.position.x - transform.position.x < 0)
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

        private void SwordHit()
        {
            Collider2D[] colliders1 = Physics2D.OverlapCircleAll(hitPoint1.position, 1.5f, hittableLayer);
            Collider2D[] colliders2 = Physics2D.OverlapCircleAll(hitPoint2.position, 1.5f, hittableLayer);

            for (int i = 0; i < colliders1.Length; i++)
            {
                colliders1[i].GetComponent<Player>().TakeDamage(60, (Vector2)transform.position);

                return;
            }

            for (int i = 0; i < colliders2.Length; i++)
            {
                colliders2[i].GetComponent<Player>().TakeDamage(60, (Vector2)transform.position);
            }
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