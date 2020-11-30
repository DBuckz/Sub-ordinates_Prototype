using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamController : MonoBehaviour
{
    public GameObject[] characters;
    public Characters[] chars;
    public int player;
    public int selected;

    public int[] health;

    public bool[] dead;
    public int deadCount;

    public ChararacterControllerScript playerScript;
    public Transform spawn;

    private void Start()
    {
        for(int i = 0; i< 3; i++)
        {
            health[i] = chars[i].health;
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

        playerScript.Changed(chars[selected], death);
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
        if (health[selected] <= 0)
        {
            health[selected] = 0;
            dead[selected] = true;
            deadCount++;
        }
        characters[selected].transform.GetChild(0).GetComponent<Image>().fillAmount = (health[selected] / (float)chars[selected].health);
        if (dead[selected] && deadCount < 3)
        {
            NewChar(1, true);
            playerScript.transform.position = spawn.position;
        }
    }
}
