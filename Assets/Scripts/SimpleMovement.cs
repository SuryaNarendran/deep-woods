using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{

    [SerializeField] public Transform model;
    [SerializeField] Animator animator;
    [SerializeField] Footsteps footsteps;

    public bool movementEnabled = true;

    public float speed = 10.0f;
    private float translation;
    private float straffe;

    private CharacterController charController;

    private string ANIM_RUN_BOOL = "Run bool";

    //Vector3 lastDirection = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementEnabled == false)
        {
            translation = 0;
            straffe = 0;
        }
        else
        {
            // Input.GetAxis() is used to get the user's input
            // You can furthor set it on Unity. (Edit, Project Settings, Input)
            translation = Input.GetAxis("Vertical") * speed;
            straffe = Input.GetAxis("Horizontal") * speed;
        }
      
        Vector3 globalDirection = transform.TransformVector(new Vector3(straffe, 0, translation));
        //Vector3 globalDirection = new Vector3(straffe, 0, translation);
        charController.SimpleMove(globalDirection);

        if (globalDirection.sqrMagnitude > 0.05f)
        {
            model.transform.rotation = Quaternion.LookRotation(globalDirection, Vector3.up);
        }

        float motion = Mathf.Abs(translation) + Mathf.Abs(straffe);
        bool isMoving = motion > 0;
        animator.SetBool(ANIM_RUN_BOOL, isMoving);
        footsteps.running = isMoving;
    }
}
