using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public List<GameObject> body;
    public float SpeedTimer = 30;
    public float MoveIncreaseValue = 2;
    public float RotationIncreaseValue = 1.5f;
    public GameObject BodyPrefab;



    private PlayerMovementScript _movementScript;
    private GameManager _managerRefrence;
    private float _timer;
    private int _bodyIndex;


    void Start()
    {
        _managerRefrence = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _movementScript = GetComponent<PlayerMovementScript>();
        _timer = SpeedTimer;
        _bodyIndex = 0;
    }

    private void Update()
    {
        if(_managerRefrence.InGame)
        {
            _timer -= 1 * Time.deltaTime;
            if (_timer <= 0)
            {
                _movementScript.UpdateSpeed(MoveIncreaseValue, RotationIncreaseValue);

                foreach (var part in body)
                {
                    BodyMovementScript script = part.GetComponent<BodyMovementScript>();
                    script.UpdateSpeed(MoveIncreaseValue);
                }
                _timer = SpeedTimer;
            }
        }
        
    }

    public void GrowBody()
    {
        Vector3 Pos;
        if (body.Count == 0)
        {
            Pos = GetComponentInChildren<Expansion>().transform.position;
            _bodyIndex -= 1;
        }
        else
            Pos = body[_bodyIndex].GetComponentInChildren<Expansion>().transform.position;

        GameObject bodyPart = Instantiate(BodyPrefab, Pos, Quaternion.identity);
        bodyPart.transform.parent = null;
        bodyPart.GetComponent<BodyMovementScript>().PartIndex = _bodyIndex + 1;
        body.Add(bodyPart);

        _bodyIndex++;
            
    }
}
