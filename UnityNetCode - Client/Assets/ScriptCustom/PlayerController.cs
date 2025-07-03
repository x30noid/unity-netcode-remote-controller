using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : NetworkBehaviour
{
    private float xInput;
    private float yInput;
    //private Rigidbody rb;
    public float speed = 1;
    private bool isJump = false;
    public float jumpSpeed = 10;
    private bool isGrounded = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.linearVelocity = new Vector3(0, 0, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
            //jump();
        //}
        
    }

    private void FixedUpdate()
    {
        transform.Translate(xInput * speed, 0, yInput * speed);

        //rb.linearVelocity = new Vector3(xInput * speed, rb.linearVelocity.y , yInput * speed);

        //if(isJump )
        //{
            

        //    isJump = false;
        //    isGrounded = false;
        //}

    }

    private void jump()
    {
        //rb.AddForce(0, jumpSpeed, 0);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
       
    }
}
