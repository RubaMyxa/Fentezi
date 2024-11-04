using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float horizonal = Input.GetAxis("Horizontal");
        bool jump = Input.GetKeyDown("space");

        characterController.Move(horizonal, jump);
    }
}
