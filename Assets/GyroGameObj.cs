using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GyroGameObj : MonoBehaviour
{
    Gyroscope gyroController;
    Vector3 v;
    List<Vector3> rotList = new List<Vector3>();

    struct gyroConst
    {
        // TODO: modify these values properly
        public const float acc = 9.8f;
        public const float deadZoneRadious = 0.05f;
        public const float maxSpeedRadious = 0.1f;
        // to calculate average of frameCount-many rotation
        public const int frameCount = 15;
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
        rotList.Clear();
        for (int i = 0; i < gyroConst.frameCount; i++)
        {
            rotList.Add(gyroController.rotationRateUnbiased);
        }
        v = gyroMove();
        // For test
        transform.Translate(v.x,v.y,v.z);
    }

    // return gyro-based velocity vector
    public Vector3 gyroMove()
    {
        // For LandscapeLeft/Right, rotate vector -90 degree along z-axis
        return Quaternion.Euler(0,0,-90) * GyroSpeedCorrection();
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
        switch (Input.deviceOrientation)
        {
            case DeviceOrientation.Portrait:
                dir = new Vector3(0,-1,0);
                // print("P");
                break;
            case DeviceOrientation.PortraitUpsideDown:
                dir = new Vector3(0,1,0);
                // print("PU");
                break;
            case DeviceOrientation.LandscapeLeft:
                dir = new Vector3(-1,0,0);
                // print("LL");
                break;
            case DeviceOrientation.LandscapeRight:
                dir = new Vector3(1,0,0);
                // print("LR");
                break;
            default:
                break;
        }
        return dir;
    }
}
