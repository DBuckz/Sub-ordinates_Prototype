using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public Transform celt, greek, japanese;
    public int player;
    int i;
    bool A;
    bool selecting;
    static bool cSelected, gSelected, jSelected;
   public TeamManager team;
    public float VerticalInput;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        VerticalInput = Input.GetAxis("Vertical" + player);
        A = Input.GetButton("Jump" + player);
        if ( selecting==false )
            {

                StartCoroutine(Select());
                if (i == 0)
                {
                    transform.position = japanese.position;
                    i = 3;
                }
                if (i == 1)
                {
                    transform.position = celt.position;
                }
                if (i == 2)
                {

                    transform.position = greek.position;
                }
                if (i == 3)
                {
                    transform.position = japanese.position;
                }
                if (i == 4)
                {

                    transform.position = celt.position;
                    i = 1;
                }

            if (A)
            {
                if (i == 1 && team.team[0]==0)
                {
                    //celt
                  
                    team.team[0]=player;
                   
                }
                if (i == 2 && team.team[1] == 0)
                {
                    //greek
                    
                    team.team[1] = player;
                  
                }
                if (i == 3 && team.team[2] == 0)
                {
                    //japan
                   
                    team.team[2] = player;
                  
                }
                team.Fucksion(player, i-1);
            }
         
        }


       

        

        
    }

    private IEnumerator Select()
    {
       
            if (VerticalInput < 0)
            {
                i++;
            }
            if (VerticalInput > 0)
            {
                i--;
            }
            selecting = true;
       
     

        yield return new WaitForSeconds(0.2f);
        selecting = false;
       

    }
}
