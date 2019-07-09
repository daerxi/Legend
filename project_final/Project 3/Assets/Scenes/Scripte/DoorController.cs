using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public string scene;
    KnightController knightController;
    GameObject player;
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.tag = "door";
        player = GameObject.FindWithTag("Player");
        knightController = player.GetComponent<KnightController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (knightController.hasMap)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (scene == "Menu" || scene == "Win" || scene == "Lose")
                {
                    collision.gameObject.SetActive(false);
                    knightController.hpOutput.text = "";
                    camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
                    camera.enabled = false;
                }
                knightController.hasMap = false;
            }
            SceneManager.LoadScene(scene);
            GameObject startPoint = GameObject.Find("startPoint");
            if (startPoint != null)
            {
                player.gameObject.transform.position = new Vector3(startPoint.gameObject.transform.position.x, startPoint.gameObject.transform.position.y, 0);
            }
        }

    }
}
