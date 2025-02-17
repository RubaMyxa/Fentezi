using Assets.Game.Code.Interfaces;
using Assets.Game.Code.UI;
using UnityEngine;

namespace Assets.Game.Code.Props
{
    public class Burrel : MonoBehaviour, IDamageble
    {
        [SerializeField]
        private GameObject coinPickup;
        [SerializeField]
        private ProgressBar hpBar;

        private Animator animator;

        private int hp = 100;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;

            if (hp <= 0)
            {
                Die();
            }

            hpBar.SetProgressBarAmount(1f / 100f * hp);
        }

        public void Die()
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject coin = Instantiate(coinPickup);
                coin.transform.position = transform.position;
            }

            animator.SetTrigger("TakeDamage");
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}