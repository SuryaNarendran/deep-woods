using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharMovement2 : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    public float moveSpeed;
    public float jumpSpeed;
    public float hJumpSpeed;
    public float inTheAirSpeedRatio;
    public float landingDelay = 0f;
    public float rotateSpeed;

    private float maxYSpeed = 0f;


    string H_AXIS_NAME = "Horizontal";
    string V_AXIS_NAME = "Jump";
    string L_AXIS_NAME = "Vertical";
    string ANIM_L_SPEED_NAME = "Run Button";
    string ANIM_V_SPEED_NAME = "Jump Button";
    string ANIM_ON_GROUND_NAME = "inTheAir";
    string ANIM_CROUCH_NAME = "Crouch";
    string ANIM_LANDING_NAME = "Landing";


    bool inTheAir = false;
    bool isLanding = false;
    float landingTime = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.Log("inTheAir: " + inTheAir + " isLanding: " + isLanding);

        float horizontal = Input.GetAxis(H_AXIS_NAME);
        float lateral = Mathf.Clamp(Input.GetAxis(L_AXIS_NAME), 0, 1);
        float vertical = Input.GetAxis(V_AXIS_NAME);

        if (!isLanding)
        {
            rb.MovePosition(transform.forward * lateral * moveSpeed * Time.deltaTime + rb.position);
        }


        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * horizontal * rotateSpeed);

        if (isLanding)
        {
            if(landingTime > landingDelay)
            {
                isLanding = false;
                landingTime = 0f;
            }
            else
            {
                landingTime += Time.deltaTime;
            }
        }

        animator.SetFloat(ANIM_L_SPEED_NAME, lateral);
        animator.SetFloat(ANIM_V_SPEED_NAME, vertical);
        animator.SetBool(ANIM_ON_GROUND_NAME, inTheAir);
        animator.SetBool(ANIM_LANDING_NAME, isLanding);

        if (vertical > 0 && !inTheAir && !isLanding)
        {
            rb.AddForce(Vector3.up * jumpSpeed + transform.forward * lateral * hJumpSpeed, ForceMode.VelocityChange);
            inTheAir = true;
            isLanding = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        inTheAir = false;
        isLanding = true;
        Debug.Log("collisionEnter");
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionExit(Collision collision)
    {
        inTheAir = true;
        isLanding = false;
        Debug.Log("collisionExit");
    }
}
