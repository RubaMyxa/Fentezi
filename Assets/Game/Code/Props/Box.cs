using Assets.Game.Code.Containers;
using Assets.Game.Code.Interfaces;
using UnityEngine;

namespace Assets.Game.Code.Props
{
    public class Box : MonoBehaviour, IDamageble, IDieble
    {
        [SerializeField]
        private PropsContainer propsContainer;

        [SerializeField]
        private LootData[] lootData;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            Init();
        }

        private void Init()
        {
            foreach (var loot in lootData)
            {
                loot.Init(propsContainer);
            }
        }

        public void TakeDamage(int damage)
        {
            Die();
        }

        public void Die()
        {
            foreach (var loot in lootData)
            {
                for (var i = 0; i < loot.count; i++)
                {
                    GameObject coin = Instantiate(loot.SpawnPickup);
                    coin.transform.position = transform.position;
                }
            }

            animator.SetTrigger("TakeDamage");
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}