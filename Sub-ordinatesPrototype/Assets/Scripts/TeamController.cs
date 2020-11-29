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


    public void NewChar(int dir)
    {
        selected += dir;
        if (selected > 2) selected = 0;

        foreach(GameObject character in characters)
        {
            if(character == characters[dir])
            {
                character.transform.localScale = new Vector3(1, 1, 1);
            }
            else character.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        }
    }

    public void Hurt(int health)
    {
        characters[selected].GetComponent<Image>().fillAmount = (health / chars[selected].health);
    }
}
