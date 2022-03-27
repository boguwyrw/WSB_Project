using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform cannon;

    float speed = 5.0f;
    float tilt = 1.50f;
    float jumpForce = 390.0f;
    float direction = 0;

    float cannonRotationValue = 0.30f;

    bool isJumping = false;

    new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerMovement();

        PlayerJump();
    }

    void FixedUpdate()
    {
        float moveHorizontal = speed * direction;
        Vector3 movement = new Vector3(moveHorizontal, rigidbody.velocity.y, 0.0f);

        rigidbody.velocity = movement;
        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * tilt);
    }

    void LateUpdate()
    {
        CannonMovement();
    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            direction = 1.0f;
            transform.localScale = PlayerDirection(direction);
        }
        else if (!Input.GetKey(KeyCode.A))
        {
            direction = 0.0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction = -1.0f;
            transform.localScale = PlayerDirection(direction);
        }
        else if (!Input.GetKey(KeyCode.D))
        {
            direction = 0.0f;
        }
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rigidbody.AddForce(transform.up * jumpForce);
        }
    }

    Vector3 PlayerDirection(float dir)
    {
        Vector3 dirScale = new Vector3(dir, 1.0f, 1.0f);
        return dirScale;
    }

    void CannonMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.localScale.x == 1.0 && cannon.rotation.z < cannonRotationValue)
                cannon.Rotate(Vector3.forward);
            else if (transform.localScale.x == -1.0 && cannon.rotation.z > -cannonRotationValue)
                cannon.Rotate(Vector3.forward);
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (transform.localScale.x == 1.0 && cannon.rotation.z > -cannonRotationValue)
                cannon.Rotate(Vector3.back);
            else if (transform.localScale.x == -1.0 && cannon.rotation.z < cannonRotationValue)
                cannon.Rotate(Vector3.back);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            isJumping = false;
        }

        if (collision.gameObject.layer == 16)
        {
            Debug.Log("GAME OVER!!!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13 || other.gameObject.layer == 17)
        {
            Debug.Log("YOU HAVE BEEN HIT");
        }
    }
}
