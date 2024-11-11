using UnityEngine;

public class Player : MonoBehaviour
{
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
            print(animator.GetCurrentAnimatorStateInfo(0).fullPathHash + " | " + AttackHash);
            animator.SetTrigger(AttackHash);
        }
    }
}
