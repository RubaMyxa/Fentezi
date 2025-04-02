using UnityEngine;
using Assets.Game.Code.Interfaces;
using Assets.Game.Code.UI;

namespace Assets.Game.Code.AI.Enemys.Skeleton
{
    public class Boss : MonoBehaviour, IDamageble, IDieble
    {
        [Header("Parameters")]
        [SerializeField]
        private int hp;
        [Space]
        [SerializeField]
        private HpBar hpBar;

        private Rigidbody2D rb;
        private Animator animator;

        private int currentHp;
        private int maxHp;

        private bool IsAlive => currentHp > 0;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            currentHp = hp;
            maxHp = currentHp;
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
            currentHp = 0;
            hpBar.HpBarUpdate(currentHp, maxHp);

            animator.SetTrigger("Die");
        }
    }
}