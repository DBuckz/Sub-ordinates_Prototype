using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public GameObject[] players;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = 0.5f * (players[0].transform.position + players[1].transform.position);
        pos = new Vector3(pos.x, pos.y, transform.position.z);
        transform.position = pos;
        if(Vector2.Distance(players[0].transform.position, players[1].transform.position) > 4)
        {
            GetComponent<Camera>().orthographicSize = 4 + 0.25f* (Vector2.Distance(players[0].transform.position, players[1].transform.position) - 4);
        }
        else
        {
            GetComponent<Camera>().orthographicSize = 4;
        }
    }
}
