using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float _shakeAmount = 0.1f;
    float _shakeTime = 0.5f;
    float _coolDown = 0;
    Vector3 _initialCameraPos = new(0, 0, -10);
    Transform _cam;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (0 < _coolDown)
        {
            _coolDown -= Time.deltaTime;
            _cam.Translate((_initialCameraPos - _cam.position) * _coolDown / _shakeTime);
        }
        else
        {
            _coolDown = 0;
            _cam.position = _initialCameraPos;
        }
    }

    public void Shake(Vector3 dir)
    {
        _coolDown = _shakeTime;
        _cam.position = _initialCameraPos + _shakeAmount * dir;
    }
}
