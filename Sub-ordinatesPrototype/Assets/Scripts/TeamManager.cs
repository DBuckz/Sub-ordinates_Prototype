using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TeamManager : MonoBehaviour
{
    public int[] team;
    bool A;
    public Picker p1, p2;
    bool X1, X2;
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
                InfoCarry.infoCarry.Recieve(0,p1.selectedTeam);
                InfoCarry.infoCarry.Recieve(1,p2.selectedTeam);
                SceneManager.LoadScene(1);
            }
        }
        
        
    }




   public void Function(int player,int index)
    {
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
    }
}
