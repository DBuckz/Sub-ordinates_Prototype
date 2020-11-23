using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputDetection : MonoBehaviour
{

    private Vector2 _movement = Vector2.zero;
    private string _xAxis, _yAxis;
    public float speed;

    void Start()
    {
        _xAxis = "Horizontal";
        _yAxis = "Vertical";
    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw(_xAxis), Input.GetAxisRaw(_yAxis));
        if(Input.GetButtonDown("Submit"))
        {
Debug.Log(_movement);
        }
        
       
    }

}
