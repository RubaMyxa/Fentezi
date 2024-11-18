using Assets.Game.Code.Props;
using UnityEngine;

namespace Assets.Game.Code.Character
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Transform hitPoint;
        [SerializeField]
        private LayerMask hittableLayer;

        private CharacterController characterController;
        private Animator animator;

        private int HorizontalHash = Animator.StringToHash("horizontal");
        private int AttackHash = Animator.StringToHash("Attack");

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Movement();
            Attack();
        }

        private void Movement()
        {
            float horizonal = Input.GetAxis("Horizontal"); // -1 to 1
            bool jump = Input.GetKeyDown("space");

            animator.SetFloat(HorizontalHash, Mathf.Abs(horizonal));

            characterController.Move(horizonal, jump);
        }

        private void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash != AttackHash)
            {
                animator.SetTrigger(AttackHash);
            }
        }

        private void Hit()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPoint.position, 0.5f, hittableLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    colliders[i].GetComponent<Box>().TakeDamage();
                }
            }
        }
    }
}