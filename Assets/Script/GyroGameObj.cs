using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GyroGameObj : MonoBehaviour
{
    Gyroscope _gyroController;
    Vector3 _velocity;
    List<Vector3> _rotList = new List<Vector3>();


    // TODO: modify these values properly
    public float _deadZoneRadious = 0.05f;
    public float _maxSpeedRadious = 10.1f;
    // to calculate average of frameCount-many rotation
    public int _gyroCountPerFrame = 15;


    // Start is called before the first frame update
    void Start()
    {
        _gyroController = Input.gyro;
        _gyroController.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        _rotList.Clear();
        for (int i = 0; i < _gyroCountPerFrame; i++)
        {
            _rotList.Add(_gyroController.rotationRateUnbiased);
        }
        _velocity = GyroMove();
        // For test
        transform.Translate(_velocity.x, _velocity.y, _velocity.z);
    }

    // return gyro-based velocity vector
    public Vector3 GyroMove()
    {
        // For LandscapeLeft/Right, rotate vector -90 degree along z-axis
        return Quaternion.Euler(0, 0, -90) * GyroSpeedCorrection();
    }

    // Implement dead zone & max speed
    Vector3 GyroSpeedCorrection()
    {
        // This average might make inertia
        Vector3 avr = new(_rotList.Average(x => x.x), _rotList.Average(x => x.y), _rotList.Average(x => x.z));
        // Dead Zone
        if (avr.magnitude < _deadZoneRadious)
        {
            avr = Vector3.zero;
        }
        // Max speed
        if (_maxSpeedRadious < avr.magnitude)
        {
            avr = avr.normalized * _maxSpeedRadious;
        }
        return avr;
    }

    // return the direction where bottom facing now
    Vector3 BottomFacing()
    {
        // TODO: check on andriod
        Vector3 dir = new(0, 0, 0);
        switch (Input.deviceOrientation)
        {
            case DeviceOrientation.Portrait:
                dir = new(0, -1, 0);
                // print("P");
                break;
            case DeviceOrientation.PortraitUpsideDown:
                dir = new(0, 1, 0);
                // print("PU");
                break;
            case DeviceOrientation.LandscapeLeft:
                dir = new(-1, 0, 0);
                // print("LL");
                break;
            case DeviceOrientation.LandscapeRight:
                dir = new(1, 0, 0);
                // print("LR");
                break;
            default:
                break;
        }
        return dir;
    }

    public Vector3 GetGyroValue()
    {
        return _velocity;
    }
}
