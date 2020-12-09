using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeamController : MonoBehaviour
{
    public GameObject[] characters;
    public Characters[] chars;
    public Characters[] celts, greek, japanese;
    public int player;
    public int selected;

    public int[] health;

    public bool[] dead;
    public int deadCount;

    public ChararacterControllerScript playerScript;
    public Transform spawn;

    private InfoCarry _infoCarry;
    public int teamSelected;

    private void Start()
    {
        _infoCarry = GameObject.Find("InfoCarry").GetComponent<InfoCarry>();
        teamSelected = _infoCarry.playerTeams[player - 1];
        selected = teamSelected - 1;
        
        switch (teamSelected)
        {
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    health[i] = celts[i].health;
                }
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    health[i] = greek[i].health;
                }
                break;
            case 3:
                for (int i = 0; i < 3; i++)
                {
                    health[i] = japanese[i].health;
                }
                break;
        }
        Invoke("Wait", 0.04f);
    }

    public void Wait()
    {

        NewChar(0, false);
    }

    public void NewChar(int dir, bool death)
    {
        selected += dir;
        if (selected > 2) selected = 0;
        if (selected < 0) selected = 2;

        while (dead[selected])
        {
            selected += dir;
            if (selected > 2) selected = 0;
            if (selected < 0) selected = 2;
        }

        switch (teamSelected)
        {
            case 1:
                playerScript.Changed(celts[selected], death);
                break;
            case 2:
                playerScript.Changed(greek[selected], death);
                break;
            case 3:
                playerScript.Changed(japanese[selected], death);
                break;
        }

        //playerScript.Changed(chars[selected], death);
        //playerScript.character = chars[selected];

        foreach (GameObject character in characters)
        {
            if(character == characters[selected])
            {
                character.transform.localScale = new Vector3(1, 1, 1);
            }
            else character.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        }
    }

    public void Hurt(int dmg)
    {
        health[selected] -= dmg;
        switch (teamSelected)
        {
            case 1:
                if (health[selected] <= 0 && celts[selected].deity)
                {
                    SceneManager.LoadScene(2);
                }
                break;
            case 2:
                if (health[selected] <= 0 && greek[selected].deity)
                {
                    SceneManager.LoadScene(2);
                }
                break;
            case 3:
                if (health[selected] <= 0 && japanese[selected].deity)
                {
                    SceneManager.LoadScene(2);
                }
                break;
        }
        if (health[selected] <= 0)
        {
            health[selected] = 0;
            dead[selected] = true;
            deadCount++;
        }
        switch (teamSelected)
        {
            case 1:
                characters[selected].transform.GetChild(0).GetComponent<Image>().fillAmount = (health[selected] / (float)celts[selected].health);
                break;
            case 2:
                characters[selected].transform.GetChild(0).GetComponent<Image>().fillAmount = (health[selected] / (float)greek[selected].health);
                break;
            case 3:
                characters[selected].transform.GetChild(0).GetComponent<Image>().fillAmount = (health[selected] / (float)japanese[selected].health);
                break;
        }
        //characters[selected].transform.GetChild(0).GetComponent<Image>().fillAmount = (health[selected] / (float)chars[selected].health);
        if (dead[selected] && deadCount < 3)
        {
            NewChar(1, true);
            playerScript.transform.position = spawn.position;
        }
    }
}
