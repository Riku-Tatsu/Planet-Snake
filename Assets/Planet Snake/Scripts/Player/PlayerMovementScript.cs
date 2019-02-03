using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    #region members

    #region Public
    public float  moveSpeed;
    public float rotationSpeed = 180.0f;
    public float minDis = 0.25f;
    public float Timer = 2.0f;

    
    [HideInInspector]
    public bool _playing;
    #endregion

    #region private
    private Vector3 moveDirection;
    private Rigidbody rBody;
    private Quaternion TurnRotation;
    private float input;
    private GameManager _managerRefrence;
    private float _dis;
    private Transform currBodyPart;
    private Transform prevBodyPart;
    private float _timer;
    [SerializeField]
    private List<Transform> TrailSegments = new List<Transform>();
    #endregion

    #endregion


    #region methods
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        _playing = true;
        _managerRefrence = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _timer = 0;
    }
    void Update()
    {
        //input = Input.GetAxisRaw("Horizontal");
        //_timer += 1 * Time.deltaTime;
        //if (_timer >= Timer)
        //{
        //    TrailSegments.Add(transform);
        //    _timer = 0;
        //}

        //TrailSegments.Add(transform);
    }

    void FixedUpdate()
    {
        Move();
        //Move_Modified();
    }

    public void UpdateSpeed(float MoveIncreaseValue, float RotationIncreaseValue)
    {
        moveSpeed += MoveIncreaseValue;
        rotationSpeed += RotationIncreaseValue;
    }

    private void Move()
    {
        if (_playing)
        {
            //Moving
            rBody.MovePosition(rBody.position + transform.forward * moveSpeed * Time.deltaTime);

            //Turning
            Quaternion Rotation = Quaternion.Euler(0, input * rotationSpeed * Time.deltaTime, 0);
            transform.rotation *= Rotation;

            PlayerScript script = _managerRefrence.Player.GetComponent<PlayerScript>();

            for(int i = 0; i < script.bodyParts.Count; i++)
            {
                currBodyPart = script.bodyParts[i];

                if (i == 0)
                    prevBodyPart = transform;
                else
                    prevBodyPart = script.bodyParts[i - 1];

                _dis = Vector3.Distance(prevBodyPart.position, currBodyPart.position);

                Vector3 newpos = prevBodyPart.position;


                float T = Time.deltaTime * _dis / minDis * moveSpeed;

                if (T > 0.5f)
                    T = 0.5f;

                currBodyPart.position = Vector3.Slerp(currBodyPart.position, newpos, T);
                currBodyPart.rotation = Quaternion.Slerp(currBodyPart.rotation, prevBodyPart.rotation, T);
            }
        }
    }

    private void Move_Modified()
    {
        if(_playing)
        {
            //Moving
            rBody.MovePosition(rBody.position + transform.forward * moveSpeed * Time.smoothDeltaTime);

            //Turning
            Quaternion Rotation = Quaternion.Euler(0, input * rotationSpeed * Time.deltaTime, 0);
            transform.rotation *= Rotation;

            PlayerScript script = _managerRefrence.Player.GetComponent<PlayerScript>();

            for (int i = 0; i < script.bodyParts.Count; i++)
            {
                currBodyPart = script.bodyParts[i];

                for (int j = 0; j < TrailSegments.Count; j++)
                {
                    prevBodyPart = TrailSegments[j];

                    _dis = Vector3.Distance(prevBodyPart.position, currBodyPart.position);

                    Vector3 newpos = prevBodyPart.position;

                    float targetDist = minDis * i + 1;

                    float T = Time.deltaTime * _dis / targetDist * moveSpeed;

                    if (T > 0.5f)
                        T = 0.5f;

                    currBodyPart.position = Vector3.Slerp(currBodyPart.position, newpos, T);
                    currBodyPart.rotation = Quaternion.Slerp(currBodyPart.rotation, prevBodyPart.rotation, T);
                }
            }
        }
    }

    public void RotationButtons(int dir)
    {
        if (dir < 0)
            input = -1;
        else if (dir > 0)
            input = 1;
        else
            input = 0;
    }
    #endregion




}
