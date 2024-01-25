using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{
    public FixedJoystick fixedJoystick;
    public GameObject shotgun_shell;
    Vector3 shell_drop_position = new Vector3(4.81f, 0.88f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        shotgun_shell = Resources.Load("Prefab/shotgun_shell") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 angle = fixedJoystick.Direction;

        float rotZ = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);        
        // transform.LookAt(angle);
    }

    public void generate_shotgun_shell()
    {
        //GameObject shotgun_shell_temp = Instantiate(shotgun_shell, shell_drop_position,Quaternion.identity, transform);
        GameObject shotgun_shell_temp = Instantiate(shotgun_shell, transform, true );
        shotgun_shell_temp.transform.localPosition = shell_drop_position;
    }
}
