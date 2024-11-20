using Assets.Game.Code.Interfaces;
using Assets.Game.Code.UI;
using UnityEngine;

namespace Assets.Game.Code.Props
{
    public class Burrel : MonoBehaviour, IDamageble
    {
        [SerializeField]
        private ProgressBar hpBar;

        private Animator animator;

        private int hp = 100;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage()
        {
            hp -= 30;

            if (hp <= 0)
            {
                animator.SetTrigger("TakeDamage");
                Destroy(GetComponent<BoxCollider2D>());
            }

            hpBar.SetProgressBarAmount(1f / 100f * hp);
        }
    }
}