using Assets.Game.Code.Interfaces;
using Assets.Game.Code.Props;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private int DieHash = Animator.StringToHash("Die");

        private int hp = 5;
        private bool isAlive = true;
        private int keys = 0;

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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("PickupUnlock"))
            {
                return;
            }

            if (collision.CompareTag("Coin"))
            {
                collision.GetComponent<Coin>().Collect();
            }
            else if (collision.CompareTag("Key"))
            {
                keys += 1;

                collision.GetComponent<Key>().Collect();
            }
        }

        private void Movement()
        {
            if (!isAlive)
            {
                return;
            }

            float horizonal = Input.GetAxis("Horizontal"); // -1 to 1
            bool jump = Input.GetKeyDown("space");

            animator.SetFloat(HorizontalHash, Mathf.Abs(horizonal));

            characterController.Move(horizonal, jump);
        }

        private void Attack()
        {
            if (!isAlive)
            {
                return;
            }

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
                    colliders[i].GetComponent<IDamageble>()?.TakeDamage();
                }
            }
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;

            if (hp < 0)
            {
                Die();
            }

            print("HP: " + hp);
        }

        public void Die()
        {
            isAlive = false;

            animator.SetTrigger(DieHash);
            StartCoroutine(Respawn());
        }

        private IEnumerator Respawn()
        {
            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public int GetKeys()
        {
            return keys;
        }
    }
}