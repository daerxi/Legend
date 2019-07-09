using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("level 1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        if (GameObject.FindWithTag("Player")!=null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.SetActive(false);
        }
        SceneManager.LoadScene("Menu");
    }
}
