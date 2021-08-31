using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3;

    public float horizontalSpeed = 6;
    private float jumpForce = 5.0f;
    private Rigidbody rb;
    public SphereCollider col;
    public LayerMask groundLayers;
    public Animation lionAnimation;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        lionAnimation.Play("idle");
    }
    void Update()
    {
        lionAnimation.Play("run");
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if(this.gameObject.transform.position.x > LevelBoundary.leftSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed, Space.World);
            }         
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) 
        {   
            if(this.gameObject.transform.position.x < LevelBoundary.rightSide)
            {
                transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed, Space.World);
            }
            
        }
            if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                lionAnimation.Play("jump");
            }
    }
    void OnTriggerEnter(Collider other)
{
    print("Collision detected with trigger object " + other.name);
}

    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);
    }
}
