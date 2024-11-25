using Assets.Game.Code.Interfaces;
using UnityEngine;

namespace Assets.Game.Code.Props
{
    public class Box : MonoBehaviour, IDamageble
    {
        [SerializeField]
        private GameObject coinPickup;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage()
        {
            Die();
        }

        public void Die()
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject coin = Instantiate(coinPickup);
                coin.transform.position = transform.position;
            }

            animator.SetTrigger("TakeDamage");
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}