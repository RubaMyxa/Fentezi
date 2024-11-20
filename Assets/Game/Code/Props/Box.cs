using Assets.Game.Code.Interfaces;
using UnityEngine;

namespace Assets.Game.Code.Props
{
    public class Box : MonoBehaviour, IDamageble
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage()
        {
            animator.SetTrigger("TakeDamage");
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}