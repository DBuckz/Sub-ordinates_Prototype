using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool picked;

    public Image[] images;
   
    public Sprite[] Celts,greeks,japan;

    public AudioSource move;
    public AudioSource selected;

    private InfoCarry _infoCarry;
    void Start()
    {
        _infoCarry = GameObject.Find("InfoCarry").GetComponent<InfoCarry>();
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
                    images[0].sprite = Celts[0];
                    images[1].sprite = Celts[1];
                    images[2].sprite = Celts[2];
                    _infoCarry.Recieve(player - 1, 1);
                    picked = true;
                    selected.Play();

                }
                if (i == 2 && team.team[1] == 0)
                {
                    //greek
                    
                    team.team[1] = player;
                    images[0].sprite = greeks[0];
                    images[1].sprite = greeks[1];
                    images[2].sprite = greeks[2];
                    _infoCarry.Recieve(player - 1, 2);
                    picked = true;
                    selected.Play();

                }
                if (i == 3 && team.team[2] == 0)
                {
                    //japan
                   
                    team.team[2] = player;
                    images[0].sprite = japan[0];
                    images[1].sprite = japan[1];
                    images[2].sprite = japan[2];
                    _infoCarry.Recieve(player - 1, 3);
                    picked = true;
                    selected.Play();
                    
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
            move.Play();
        }
            if (VerticalInput > 0)
            {
                i--;
            move.Play();
        }
            selecting = true;

        

        yield return new WaitForSeconds(0.2f);
        selecting = false;
       

    }
}
