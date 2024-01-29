using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GyroGameObj : MonoBehaviour
{
    Gyroscope gyroController;
    Vector3 v;
    List<Vector3> rotList = new List<Vector3>();

    struct gyroConst
    {
        // TODO: modify these values properly
        public const float acc = 9.8f;
        public const float deadZoneRadious = 0.02f;
        public const float maxSpeedRadious = 2f;
        // to calculate average of frameCount-many rotation
        public const int frameCount = 4;
    }

    // Start is called before the first frame update
    void Start()
    {
        gyroController = Input.gyro;
        gyroController.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotList.Count < gyroConst.frameCount)
        {
            rotList.Add(gyroController.rotationRateUnbiased);
        }
        else
        {
            rotList.Add(gyroController.rotationRateUnbiased);
            rotList.RemoveAt(0); // front pop
        }
        v = GyroSpeedCorrection();
        // send v to character
        // Character.move(v);
    }

    // Implement dead zone & max speed
    Vector3 GyroSpeedCorrection()
    {
        // This average might make inertia
        Vector3 avr = new Vector3(rotList.Average(x=>x.x), rotList.Average(x=>x.y), rotList.Average(x=>x.z));
        // Dead Zone
        if(avr.magnitude < gyroConst.deadZoneRadious) avr = Vector3.zero;
        // Max speed
        if(gyroConst.maxSpeedRadious < avr.magnitude) avr = avr.normalized * gyroConst.maxSpeedRadious;
        return avr;
    }

    // return the direction where bottom facing now
    Vector3 BottomFacing()
    {
        // TODO: check on andriod
        Vector3 dir = new Vector3(0,0,0);
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
                dir = new Vector3(0,-1,0);
                break;
            case ScreenOrientation.PortraitUpsideDown:
                dir = new Vector3(0,1,0);
                break;
            case ScreenOrientation.LandscapeLeft:
                dir = new Vector3(-1,0,0);
                break;
            case ScreenOrientation.LandscapeRight:
                dir = new Vector3(1,0,0);
                break;
            default:
                break;
        }
        return dir;
    }

    public Vector3 GetGyroValue()
    {
        return v;
    }
}
