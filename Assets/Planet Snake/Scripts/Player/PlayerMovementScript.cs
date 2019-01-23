using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    #region members

    #region Public
    public float  moveSpeed;
    public float rotationSpeed = 180.0f;

    
    [HideInInspector]
    public bool _playing;
    #endregion

    #region private
    private Vector3 moveDirection;
    private Rigidbody rBody;
    private Quaternion TurnRotation;
    private float input;
    private GameManager _managerRefrence;
    #endregion

    #endregion


    #region methods
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        _playing = true;
        _managerRefrence = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _managerRefrence.Points.Add(transform.position);
            Debug.Log(transform.position);
        }
    }

    void FixedUpdate()
    {
        if(_playing)
        {
            //Moving
            rBody.MovePosition(rBody.position + transform.forward * moveSpeed * Time.deltaTime);
        
            //Turning
            Quaternion Rotation = Quaternion.Euler(0, input * rotationSpeed * Time.deltaTime, 0);
            transform.rotation *= Rotation;
        }

    }

    public void UpdateSpeed(float MoveIncreaseValue, float RotationIncreaseValue)
    {
        moveSpeed += MoveIncreaseValue;
        rotationSpeed += RotationIncreaseValue;
    }
    #endregion




}
