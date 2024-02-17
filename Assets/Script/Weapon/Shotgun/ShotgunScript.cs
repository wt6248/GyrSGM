using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunScript : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        /*
            Audio can differ depending on the type of weapon
        */
        _audioSource = gameObject.AddComponent<AudioSource>();
        _gunshotSound = Resources.Load<AudioClip>("Audio/gunshotSound");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
