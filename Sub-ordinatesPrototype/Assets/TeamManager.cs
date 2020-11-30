using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public int[] team;
    Picker playerNumber;
    bool A;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        
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
