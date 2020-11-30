using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pointer : MonoBehaviour
{
    public int player;
    float VerticalInput;
    float HorizontalInput;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal" + player);
        VerticalInput = Input.GetAxis("Vertical" + player);
        
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(HorizontalInput, VerticalInput, 0f);
        transform.position = transform.position + movement/8;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("im here");
        if (collision.gameObject.CompareTag ("Start"))
        {
            if (Input.GetButtonDown("Jump1"))
            {
                Debug.Log("bbbbbb");
            }
        }
    }
}
