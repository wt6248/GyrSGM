using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float amount = 0.5f;
    float time;
    Vector3 dir;
    Vector3 initialPos = new Vector3(0, 0, -10);
    Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        dir = new Vector3(0.3f, 0.4f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (0 < time)
        {
            time -= Time.deltaTime;
            cam.transform.position = dir * amount;
        }
        else
        {
            time = 0;
            cam.transform.position = initialPos;
        }
    }

    public void Shake(float timeInSec = 0)
    {
        Debug.Log("shake!!!!!");
        if (timeInSec == 0)
        {
            time = 0.05f;
        }
        else
        {
            time = timeInSec;
        }
        initialPos = cam.transform.localPosition;
    }
}
