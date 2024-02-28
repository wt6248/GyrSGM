using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopButton : MonoBehaviour
{
    bool isPressed;
    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonUp()
    {
        isPressed = false;
    }
    public void ButtonDown()
    {
        isPressed = true;
    }
    public bool IsPressed()
    {
        return isPressed;
    }
}
