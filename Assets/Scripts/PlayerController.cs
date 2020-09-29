using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    public Rigidbody head;
    public LayerMask layerMask;

    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        //Initialize the character controller
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Setting the axis for horizontal and vertical to our moveDirection variable so
        //our WASD keys will respond accordingly
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed);

    }

    void FixedUpdate()
    {
        //Same initialization as seen in update
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveDirection == Vector3.zero)
        {
            // TODO
        }
        else
        {
            //Give the head a seperate force so it can bobble
            head.AddForce(transform.right * 150, ForceMode.Acceleration);
        }

        //Create an empty raycast hit
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Draws a ray in the scene view
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            //Tells the ray to be where the mouse is pointed
            if (hit.point != currentLookTarget)
            {
                currentLookTarget = hit.point;
            }
        }

        //Steps to getting the player model to turn relative to the mouse cursor
        // 1 
        Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        // 2
        Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
        // 3
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10.0f);
    }
}
