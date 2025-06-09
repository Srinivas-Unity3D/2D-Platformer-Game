using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float speed = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Move(speed);
        Crouch(Input.GetKey(KeyCode.LeftControl));
        Jump(vertical > 0);
    }

    private void Move(float speed)
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
