using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputDetection : MonoBehaviour
{

    private Vector3 _movement = Vector3.zero;
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
        _movement = new Vector3(Input.GetAxisRaw(_xAxis), 0, Input.GetAxisRaw(_yAxis));
        Debug.Log(_movement);
       
    }

}
