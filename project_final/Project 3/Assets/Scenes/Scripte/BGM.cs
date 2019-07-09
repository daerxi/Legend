using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource BackGroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        BackGroundMusic = GetComponent<AudioSource>();
        BackGroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //BackGroundMusic.Play();
    }
}
