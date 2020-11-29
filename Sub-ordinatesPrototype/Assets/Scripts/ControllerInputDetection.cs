using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputDetection : MonoBehaviour
{

    private Vector2 _movement = Vector2.zero;
    private string _xAxis, _yAxis,Abutton;
    public float speed;
   public  PlayerNum  id;
    private int controllerNumber;
    void Start()
    {
        if (id == PlayerNum.p1)
        {
            _xAxis = "Horizontal";
            _yAxis = "Vertical";
            Abutton = "Submit";
        }
        if (id == PlayerNum.p2)
        {
            _xAxis = "Horizontal2";
            _yAxis = "Vertical2";
            Abutton = "Submit2";
        }
    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw(_xAxis), Input.GetAxisRaw(_yAxis));
        if(Input.GetButtonDown(Abutton))
        {
Debug.Log(_movement);


            if ( _movement== new Vector2(1, 0))
            {
                
                Debug.Log(gameObject.name + "front strike" );
            }
        }
        
       
    }
    public enum PlayerNum
    {
        p1,
        p2
    }

    internal void SetControllerNumber(int number)
    {
        controllerNumber = number;
        _xAxis = "Horizontal"+controllerNumber;
        _yAxis = "Vertical" + controllerNumber;
        Abutton = "Submit" + controllerNumber;
    }

}
