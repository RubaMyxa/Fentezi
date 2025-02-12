using Assets.Game.Code.Character;
using Assets.Game.Code.UI;
using UnityEngine;

namespace Assets.Game.Code.AI.Enemys.Skeleton
{
    public class SkeletonBase : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField]
        protected int hp;
        [SerializeField]
        protected float attackCooldown;
        [SerializeField]
        protected float movementSpeed;
        [Space]
        [SerializeField]
        private HpBar hpBar;
        [SerializeField]
        protected Transform attackPoint;
        [SerializeField]
        private Vector2 attackZone;
        [SerializeField]
        protected LayerMask playerLayer;

        protected Rigidbody2D rb;
        protected Animator animator;

        protected Player player;
        protected BehaviourAI behaviourAI = BehaviourAI.Patrolling;
        protected Vector3 currentVelocity = Vector3.zero;
        protected int direction = 0;

        private int currentHp;
        private int maxHp;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            currentHp = hp;
            maxHp = hp;
        }

        protected virtual void Update()
        {
            AttackZoneDetector();

            if(Input.GetKeyDown(KeyCode.R))
            {
                TakeDamage(20);
            }
        }

        private void AttackZoneDetector()
        {
            if (behaviourAI == BehaviourAI.Patrolling)
            {
                Collider2D playerCollider = Physics2D.OverlapBox(attackPoint.position, attackZone, 0f, playerLayer);

                if (playerCollider)
                {
                    player = playerCollider.GetComponent<Player>();
                    animator.SetTrigger("Attack");
                    behaviourAI = BehaviourAI.Attack;
                }
                else
                {
                    player = null;
                    behaviourAI = BehaviourAI.Patrolling;
                }
            }
        }

        protected virtual void CalculateDirection() { }

        protected void AttackEnd()
        {
            Collider2D playerCollider = Physics2D.OverlapBox(attackPoint.position, new Vector2(1.5f, 1.5f), 0f, playerLayer);

            if (playerCollider)
            {
                animator.SetTrigger("Attack");
            }
            else
            {
                behaviourAI = BehaviourAI.Patrolling;
            }

            print("AttackEnd");
        }

        public void TakeDamage(int damage)
        {
            currentHp -= damage;
            if(currentHp < 0)
            {
                currentHp = 0;
                Die();
            }

            hpBar.HpBarUpdate(currentHp, maxHp);
        }

        private void Die()
        {

        }
    }
}