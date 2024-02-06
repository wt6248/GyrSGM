using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
    // Start is called before the first frame update
    float duration;

    void Start()
    {
        //destroy shotgun shell after 10 seconds
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f,-0.3f * Time.deltaTime,0f);
    }
}
