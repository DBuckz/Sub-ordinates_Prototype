using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TeamManager : MonoBehaviour
{
    public int[] team;
    Picker playerNumber;
    
    public Picker p1, p2;
    bool X1,X2,B,B2;
    public GameObject menu, choose;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        X1 =Input.GetButtonDown( "Attack1");
        X2 = Input.GetButtonDown("Attack2");
        if (X1 || X2)
        {
            if(p1.picked && p2.picked)
            {
                SceneManager.LoadScene(1);
            }
        }
        B = Input.GetButtonDown("Block1");
        B2 = Input.GetButtonDown("Block2");

        if (B || B2)
        {
            choose.SetActive(false);
            menu.SetActive(true);
        }
        
    }




   public void Fucksion(int player,int index)
    {
        Debug.Log("index" + index);
        //foreach (int i in team)
        //{
        //    if (i != index)
        //    {
        //        if (i == player)
        //        {
        //            team[i] = 0;
        //        }
        //    }



        //}
        for (int i=0; i < team.Length; i++)
        {
            if (team[i] == player)
            {
                if (i != index)
                {
                    team[i] = 0;
                }
            }
        }
        Debug.Log(team[0] + " " + team[1] + " " + team[2]);
    }
}
