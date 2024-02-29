using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 _startSpeed = new(-0.5f, 0.5f, 0f);

    public float _shellSpeed = 10.0f;
    readonly public float _acceleration = 16.0f;
    readonly public float _duration = 0.5f;

    void Start()
    {
        //destroy shotgun shell after duration(second) passed
        Destroy(gameObject, _duration);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(_startSpeed * Time.deltaTime);
        _startSpeed.y -= _acceleration * Time.deltaTime;

    }
    public void SetDirection(float eulerAngle)
    {
        float directionX = Mathf.Cos(eulerAngle * Mathf.Deg2Rad);
        float directionY = Mathf.Sin(eulerAngle * Mathf.Deg2Rad);
        _startSpeed = new Vector3(directionX, directionY, 0f) * _shellSpeed;
    }
}
