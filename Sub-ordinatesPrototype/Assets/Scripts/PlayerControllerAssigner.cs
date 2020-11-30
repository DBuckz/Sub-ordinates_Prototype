using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAssigner : MonoBehaviour
{
    private List<int> assignedControllers = new List<int>();


   public bool HasControllerAssigned;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i <= 2; i++)
        {
            if (assignedControllers.Contains(i))
                continue;

            if (Input.GetButton("Submit" + i))
            {
                AddPlayerController(i);
                break;
            }

        }
    }



    public Player AddPlayerController(int controller)
    {
        assignedControllers.Add(controller);


        return null;
    }



    public Player AssignController(int controller)
    {
        Debug.Log("setting player to controller");
       // Player.Input.SetControllerNumber(controller);
        HasControllerAssigned = true;

        return null;
    }




}
