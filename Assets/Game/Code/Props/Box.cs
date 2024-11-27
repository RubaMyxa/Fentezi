using Assets.Game.Code.Containers;
using Assets.Game.Code.Interfaces;
using UnityEngine;

namespace Assets.Game.Code.Props
{
    public class Box : MonoBehaviour, IDamageble
    {
        [SerializeField]
        private PropsContainer propsContainer;

        [SerializeField]
        private Loot loot;
        [SerializeField]
        [Range(1, 10)]
        private int count;

        private Animator animator;
        private GameObject spawnPickup;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            spawnPickup = propsContainer.GetPickup(loot);
        }

        public void TakeDamage()
        {
            Die();
        }

        public void Die()
        {
            for (int i = 0; i < count; i++)
            {
                GameObject coin = Instantiate(spawnPickup);
                coin.transform.position = transform.position;
            }

            animator.SetTrigger("TakeDamage");
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}