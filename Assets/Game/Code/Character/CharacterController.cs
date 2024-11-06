using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform groundPoint;
    [SerializeField]
    private LayerMask whatIsGround;
    [Space]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpForce;

    private Rigidbody2D rigidbody;

    private bool grounded;
    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.position, 0.1f, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
    }

    public void Move(float move, bool jump)
    {
        if (grounded)
        {
            Vector3 targetVelocity = new Vector2(move * movementSpeed, rigidbody.linearVelocityY);
            rigidbody.linearVelocity = Vector3.SmoothDamp(rigidbody.linearVelocity, targetVelocity, ref currentVelocity, 0.05f);
        }

        if(grounded && jump)
        {
            grounded = false;
            rigidbody.AddForce(new Vector2(0, jumpForce));
        }

        if (move < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (move > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }
}
