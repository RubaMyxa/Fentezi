using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float horizonal = Input.GetAxis("Horizontal"); // -1 to 1
        bool jump = Input.GetKeyDown("space");

        animator.SetFloat("horizontal", Mathf.Abs(horizonal));

        characterController.Move(horizonal, jump);
    }
}
