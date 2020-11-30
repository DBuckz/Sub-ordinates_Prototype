using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject main;
    public GameObject controls;
    public GameObject start;

    public EventSystemManager eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Block1")&& SceneManager.GetActiveScene().buildIndex == 0)
        {
            eventSystem.OnBack();
            main.SetActive(true);
            controls.SetActive(false);
            start.SetActive(false);
        }
    }


    public void End()
    {
        Application.Quit();
    }

    public void Fight()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
