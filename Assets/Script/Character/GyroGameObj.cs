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


    // Priority : value under < inspector < value in start()
    public float _deadZoneRadious = 0.05f;
    public float _maxSpeedRadious = 10.1f;
    // to calculate average of frameCount-many rotation
    public int _gyroCountPerFrame = 15;
    public float _gyroSensitivity = 1f;


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
        avr *= _gyroSensitivity;
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

    public Vector3 GetGyroValue()
    {
        return _velocity;
    }
}
