using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10;

    public float horizontalSpeed = 6;
    private float jumpForce = 18.0f;
    private float forwardForce = 18.0f;
    private Rigidbody rb;
    public Rigidbody rbEnemy;
    public SphereCollider col;
    public LayerMask groundLayers;
    public Animation lionAnimation;
    public Vector3 startingPoint;
    public Vector3 currentPosition;
    public AudioSource wolf1;
    public AudioSource wolf2;
    public AudioSource wolfAttack;
    public AudioSource lionDeath;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        startingPoint = transform.position;
        lionAnimation.Play("idle");
        startingPoint = transform.position;
        rb.velocity = new Vector3(0,0,3);
    }
    void Update()
    {
        if(rb.velocity.z<3)
        {
            wolf2.Play();
            forwardForce = forwardForce - 9.0f;
            if(rb.velocity.z<1)
            {
                rbEnemy.MovePosition(currentPosition);
                wolfAttack.Play();
                Debug.Log("You dead");
                StartCoroutine(delayRespawn());
            }
        }
        float maxSpeed = 3;
        rb.AddForce(Vector3.forward * jumpForce, ForceMode.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        currentPosition = transform.position;
        lionAnimation.Play("run");
        //transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
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
                if(isGrounded())
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    lionAnimation.Play("jump");
                   
                }
                else
                {
                     //Debug.Log("Player State: " + isGrounded());
                }
            }
    }
    void OnTriggerEnter(Collider other)
{
    print("Collision detected with trigger object " + other.name);
    wolf1.Play();
    lionAnimation.Stop();
}

    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);
    }

        IEnumerator delayRespawn()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        lionDeath.Play();
        yield return new WaitForSeconds(4);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        StartCoroutine(Reload());
    }
    private IEnumerator Reload()
        {
        yield return new WaitForSeconds(6);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
}
