using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

    public float moveSpeed;
    private Vector3 moveDirection;
    private Rigidbody rBody;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 1).normalized;
    }

    void FixedUpdate()
    {
        rBody.MovePosition(rBody.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
    }
}
