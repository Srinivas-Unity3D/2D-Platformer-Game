using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private float forceSpeed;

    [SerializeField] private bool isGrounded = false;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        PlayerMovement(horizontal, vertical);
        PlayerMovementAnimation(horizontal);
        Crouch(Input.GetKey(KeyCode.LeftControl));
        JumpAnimation(vertical > 0);
    }

    private void PlayerMovement(float horizontal, float vertical) 
    {
        if (!isGrounded) return;
       
        Vector3 playerPos = transform.position;
        playerPos.x += horizontal * speed * Time.deltaTime;
        transform.position = playerPos;

        playerRB.AddForce(new Vector2(0, vertical * forceSpeed), ForceMode2D.Impulse);
    }

    private void PlayerMovementAnimation(float speed)
    {
        if (!isGrounded) return;
        playerAnimator.SetFloat("Speed", Mathf.Abs(speed));
        Vector3 scale = transform.localScale;
        if (speed < 0f)
        {
            scale.x = -1 * Mathf.Abs(scale.x);
        }
        else if (speed > 0f)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    private void Crouch(bool isCrouch) 
    {
        playerAnimator.SetBool("Crouch", isCrouch);
    }

    private void JumpAnimation(bool isJump)
    {
        if (!isGrounded) return;
        playerAnimator.SetBool("Jump", isJump);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckGround(collision, true);
    }

    

    private void OnCollisionExit2D(Collision2D collision)
    {
        CheckGround(collision, false);
    }

    private void CheckGround(Collision2D collision, bool grounded)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = grounded;
        }
    }
}
