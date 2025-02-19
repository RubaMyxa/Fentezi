using Assets.Game.Code.Character;
using Assets.Game.Code.Interfaces;
using Assets.Game.Code.UI;
using UnityEngine;

namespace Assets.Game.Code.AI.Enemys.Skeleton
{
    public class SkeletonBase : MonoBehaviour, IDamageble, IDieble
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
        private GameObject dangerousZone;
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
        private BehaviourAI behaviourAI = BehaviourAI.Patrolling;
        protected Vector3 currentVelocity = Vector3.zero;
        protected int direction = 0;

        private int currentHp;
        private int maxHp;

        protected BehaviourAI GetSetBehaviourAI
        {
            get => behaviourAI;
            set
            {
                if (behaviourAI == BehaviourAI.Die)
                    return;

                behaviourAI = value;
                print(GetSetBehaviourAI);
            }
        }

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
                    GetSetBehaviourAI = BehaviourAI.Attack;
                }
                else
                {
                    player = null;
                    GetSetBehaviourAI = BehaviourAI.Patrolling;
                }
            }
        }

        protected virtual void CalculateDirection() { }

        protected void AttackEnd()
        {
            if (GetSetBehaviourAI == BehaviourAI.Die) return;

            Collider2D playerCollider = Physics2D.OverlapBox(attackPoint.position, new Vector2(1.5f, 1.5f), 0f, playerLayer);

            if (playerCollider)
            {
                animator.SetTrigger("Attack");
            }
            else
            {
                GetSetBehaviourAI = BehaviourAI.Patrolling;
            }

            print("AttackEnd");
        }

        public void TakeDamage(int damage)
        {
            if (GetSetBehaviourAI == BehaviourAI.Die) return;

            currentHp -= damage;
            if(currentHp < 0)
            {
                currentHp = 0;
                Die();
            }

            hpBar.HpBarUpdate(currentHp, maxHp);
        }

        public void Die()
        {
            if (GetSetBehaviourAI == BehaviourAI.Die) return;

            GetSetBehaviourAI = BehaviourAI.Die;
            animator.SetTrigger("Die");

            currentHp = 0;
            hpBar.HpBarUpdate(currentHp, maxHp);

            Destroy(dangerousZone);
        }
    }
}