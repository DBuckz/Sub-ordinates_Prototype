using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public Player id;

    public float Horizontal { get; set; }
    public float Hit { get; set; }
    public string player;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal" + player);
        Hit = Input.GetAxis("Submit" + player);

        if (Horizontal != 0) transform.position = new Vector3(transform.position.x + Horizontal*0.05f,0,0);

        if (Hit != 0) transform.Rotate(0,0,5);
    }

    public enum Player
    {
        p1,
        p2
    }
}
