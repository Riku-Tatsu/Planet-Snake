using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    #region members

    #region Public
    public float moveSpeed;
    public float rotationSpeed = 180.0f;
    #endregion

    #region private
    private Vector3 moveDirection;
    private Rigidbody rBody;
    private Quaternion TurnRotation;
    private float input;
    #endregion

    #endregion


    #region methods
    void Start()
    {
        rBody = GetComponent<Rigidbody>();

    }
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        //Moving
        rBody.MovePosition(rBody.position + transform.forward * moveSpeed * Time.deltaTime);
        
        //Turning
        Quaternion Rotation = Quaternion.Euler(0, input * rotationSpeed * Time.deltaTime, 0);
        transform.rotation *= Rotation;

    }
    #endregion




}
