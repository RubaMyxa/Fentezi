using Assets.Game.Code.Interfaces;
using Assets.Game.Code.Props;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Game.Code.Character
{
    public class Player : MonoBehaviour, IDieble
    {
        [SerializeField]
        private Transform hitPoint;
        [SerializeField]
        private LayerMask hittableLayer;

        private CharacterController characterController;
        private Animator animator;
        private static Rigidbody2D rigidbody;

        private int HorizontalHash = Animator.StringToHash("horizontal");
        private int AttackHash = Animator.StringToHash("Attack");
        private int DieHash = Animator.StringToHash("Die");

        private int hp = 100;
        private bool isAlive = true;
        private static bool tutorialOn = false;
        private bool isImmortal = false;
        private int keys = 0;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody2D>();
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
            if (!isAlive || tutorialOn)
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
            if (!isAlive || tutorialOn)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash != AttackHash)
            {
                animator.SetTrigger(AttackHash);
            }
        }

        private void SwordHit()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPoint.position, 0.5f, hittableLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].GetComponent<IDamageble>()?.TakeDamage(30);
            }
        }

        public void TakeDamage(int damage, Vector2 attackerPosition)
        {
            if (isImmortal)
            {
                return;
            }

            Direction direction =
                (attackerPosition.x - transform.position.x < 0)
                ? Direction.Right
                : Direction.Left;

            hp -= damage;

            if (hp <= 0)
            {
                Die();
            }

            //characterController.HitJump(direction);
            StartCoroutine(StayImmortal());
        }

        public void Die()
        {
            isAlive = false;

            characterController.ResetVelocity();
            animator.SetTrigger(DieHash);
            StartCoroutine(Respawn());
        }

        private IEnumerator Respawn()
        {
            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private IEnumerator StayImmortal()
        {
            isImmortal = true;

            float wtrTime = 0;
            float rtwTime = 0;

            // 0.25s
            while (wtrTime < 1)
            {
                wtrTime = Mathf.Clamp01(wtrTime + 0.04f);
                Color whiteToRed = Color.Lerp(Color.white, Color.red, wtrTime);
                GetComponent<SpriteRenderer>().color = whiteToRed; // Red
                yield return new WaitForSeconds(0.01f);
            }

            // 0.5
            yield return new WaitForSeconds(0.5f);

            // 0.25s
            while (rtwTime < 1)
            {
                rtwTime = Mathf.Clamp01(rtwTime + 0.04f);
                Color redToWhite = Color.Lerp(Color.red, Color.white, rtwTime);
                GetComponent<SpriteRenderer>().color = redToWhite; // White
                yield return new WaitForSeconds(0.01f);
            }

            isImmortal = false;
        }

        public int GetKeys()
        {
            return keys;
        }

        public static void TutorialOnOff(bool isOn)
        {
            tutorialOn = isOn;

            rigidbody.linearVelocity = Vector3.zero;
        }
    }
}