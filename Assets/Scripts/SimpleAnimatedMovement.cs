using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimatedMovement : MonoBehaviour
{
    public float speed = 10.0f;
    private float translation;
    private float straffe;

    private CharacterController charController;

    string H_AXIS_NAME = "Horizontal";
    string V_AXIS_NAME = "Jump";
    string L_AXIS_NAME = "Vertical";
    string ANIM_L_SPEED_NAME = "Run Button";
    string ANIM_V_SPEED_NAME = "Jump Button";
    string ANIM_ON_GROUND_NAME = "inTheAir";
    string ANIM_CROUCH_NAME = "Crouch";
    string ANIM_LANDING_NAME = "Landing";

    // Use this for initialization
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input.GetAxis() is used to get the user's input
        // You can furthor set it on Unity. (Edit, Project Settings, Input)
        translation = Input.GetAxis("Vertical") * speed;
        straffe = Input.GetAxis("Horizontal") * speed;
        Vector3 globalDirection = transform.TransformVector(new Vector3(straffe, 0, translation));
        charController.SimpleMove(globalDirection);
    }
}
