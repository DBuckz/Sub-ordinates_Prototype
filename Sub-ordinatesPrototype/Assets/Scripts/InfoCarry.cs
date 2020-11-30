using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoCarry : MonoBehaviour
{

    public static InfoCarry infoCarry;
    public int[] playerTeams;

    // Start is called before the first frame update
    void Start()
    {
        if (infoCarry == null) infoCarry = this;
        else Destroy(this);
    }

    public void Recieve(int player, int team)
    {
        playerTeams[player] = team;
    }

    public int[] Give()
    {
        return playerTeams;
    }

    public void Winner(int player)
    {

    }

    public void End()
    {
        SceneManager.LoadScene(2);
    }
}
