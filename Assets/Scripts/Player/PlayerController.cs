using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private float forceSpeed;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        PlayerMovement(horizontal, vertical);
        PlayerMovementAnimation(horizontal);
        Crouch(Input.GetKey(KeyCode.LeftControl));
        Jump(vertical > 0);
    }

    private void PlayerMovement(float horizontal, float vertical) 
    {
        // player Movement
        Vector3 playerPos = transform.position;
        playerPos.x += horizontal * speed * Time.deltaTime;
        transform.position = playerPos;

        // Player Jump
        playerRB.AddForce(new Vector2(0, vertical * forceSpeed), ForceMode2D.Force);

    }

    private void PlayerMovementAnimation(float speed)
    {
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

    private void Jump(bool isJump)
    {
        playerAnimator.SetBool("Jump", isJump);
    }
}
