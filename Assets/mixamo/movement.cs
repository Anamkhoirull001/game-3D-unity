using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public CharacterController controller;

    public Animator animator;

    [Header("Movement")]
    public float speed = 1f;
    public float gravity = -9.10f;
    public float jumpHeight = 1f;

    [Header("Groud Check")]
    public Transform ground_check;
    public float ground_distance = 0.4f;
    public LayerMask ground_mask;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(ground_check.position, ground_distance, ground_mask);
        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");

        animator.SetFloat ("forward", z);
        animator.SetFloat ("strafe", x);

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move (move * speed * Time.deltaTime);

        if (Input.GetButtonDown ("Jump")){
            animator.SetBool("jump", true);
            velocity.y = Mathf.Sqrt (jumpHeight * -2f * gravity);
        }else{
            animator.SetBool("jump", false);
        }

        if(Input.GetMouseButtonDown (0)){
            animator.SetBool ("attack", true);
        }else{
                animator.SetBool("attack", false);
        }
        

        velocity.y += gravity * Time.deltaTime;
        controller.Move (velocity * Time.deltaTime);
    }

    private void OnTriggerEnter (Collider other){
        if(other.gameObject.CompareTag("EnemyAttack")){
            animator.SetBool ("pusing", true);
        }else{
            animator.SetBool("pusing", false);
        }
    }
}
