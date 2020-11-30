using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Picker : MonoBehaviour
{
    public Transform celt, greek, japanese;
    public int player;
    public int i;
    bool A;
    //bool selecting;
    static bool cSelected, gSelected, jSelected;
   public TeamManager team;
    public float VerticalInput;
    public bool picked;
    public int selectedTeam;

    public Image[] images;
   
    public Sprite[] Celts,greeks,japan;

    int counter;
    public int buffer = 50;
    
    void Start()
    {
      
    }

    private void OnEnable()
    {
        //selecting = false;
    }

    // Update is called once per frame
    void Update()
    {

        VerticalInput = Input.GetAxis("Vertical" + player);
        A = Input.GetButtonDown("Jump" + player);


        if (VerticalInput < 0)
        {
            counter++;
        }
        if (VerticalInput > 0)
        {
            counter--;
        }

        if (counter > 3*buffer) counter -= 3*buffer;
        if (counter < 0) counter += 3*buffer;

        if (counter < buffer)
        {
            i = 1;
        }
        else if (counter < 2*buffer)
        {
            i = 2;
        }
        else if(counter < 3*buffer)
        {
            i = 3;
        }



        //if ( selecting==false )
        // {

        //StartCoroutine(Select());
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
                    images[0].sprite = Celts[0];
                    images[1].sprite = Celts[1];
                    images[2].sprite = Celts[2];
                    images[0].color = Color.white;
                    images[1].color = Color.white;
                    images[2].color = Color.white;

                    picked = true;
                    selectedTeam = i-1;

                }
                if (i == 2 && team.team[1] == 0)
                {
                    //greek
                    
                    team.team[1] = player;
                    images[0].sprite = greeks[0];
                    images[1].sprite = greeks[1];
                    images[2].sprite = greeks[2];
                    images[0].color = Color.white;
                    images[1].color = Color.white;
                    images[2].color = Color.white;
                    picked = true;
                    selectedTeam = i-1;

                }
                if (i == 3 && team.team[2] == 0)
                {
                    //japan
                   
                    team.team[2] = player;
                    images[0].sprite = japan[0];
                    images[1].sprite = japan[1];
                    images[2].sprite = japan[2];
                    images[0].color = Color.white;
                    images[1].color = Color.white;
                    images[2].color = Color.white;
                    picked = true;
                    selectedTeam = i-1;
                }
                team.Function(player, i-1);
            }
         
       // }


       

        

        
    }

    //private IEnumerator Select()
    //{
       
    //        if (VerticalInput < 0)
    //        {
    //            i++;
    //        }
    //        if (VerticalInput > 0)
    //        {
    //            i--;
    //        }
    //        selecting = true;
       
     

    //    yield return new WaitForSeconds(0.15f);
    //    selecting = false;
       

    //}
}
