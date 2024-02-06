using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 start_speed = new(-0.5f, 0.5f, 0f);

    readonly public float shell_speed = 5.0f;
    readonly public float gravitational_acceleration = 8.0f;
    readonly public float duration = 0.5f;

    void Start()
    {
        //destroy shotgun shell after duration(second) passed
        Destroy(gameObject, duration);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(start_speed * Time.deltaTime);
        start_speed.y -= gravitational_acceleration * Time.deltaTime;

    }
    public void Set_direction(Vector3 v3)
    {
        start_speed = v3 * shell_speed;
    }
    public void Set_direction(float eulerAngle)
    {
        float directionX = Mathf.Cos(eulerAngle * Mathf.Deg2Rad);
        float directionY = Mathf.Sin(eulerAngle * Mathf.Deg2Rad);
        //Debug.Log(directionX);
        //Debug.Log(directionY);
        start_speed = new Vector3(directionX, directionY, 0f) * shell_speed;
    }

}
