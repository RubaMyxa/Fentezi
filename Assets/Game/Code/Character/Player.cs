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
        float horizonal = Input.GetAxis("Horizontal"); // -1 to 1
        bool jump = Input.GetKeyDown("space");

        characterController.Move(horizonal, jump);
    }
}
