using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Cursor cursor;
    private ShotgunController shotGun;

    public GyroGameObj GyroControl;
    

    
    void Start()
    {
      cursor = GetComponentInChildren<Cursor>();
      shotGun = GetComponentInChildren<ShotgunController>();
      //GyroControl = GameObject.Find("Gyro Controller").GetComponent<GyroGameObj>();
    }

    
    void Update()
    {
        //주인공(커서) 움직이기
        MovePlayerWithCursor();

        //임시로 주인공 자이로 직접 움직이기
        //tempMovePlayerByGyro();

        //총 발사
        if(Input.GetKeyDown(KeyCode.Space)){ //스페이스바를 누르면 발사
            FireGun();
        }   
    }

    void MovePlayerWithCursor()
    {
        Vector3 cursorPosition = cursor.CursorPosition();
        transform.position += new Vector3(cursorPosition.x * 0.01f, cursorPosition.y * 0.01f, 0);
    }

    void tempMovePlayerByGyro()
    {
        Vector3 a = GyroControl.GetGyroValue();
        //Debug.Log(a);
        Vector3 b = new Vector3(a.x, a.y,0f) * Time.deltaTime * 20;
        //Debug.Log(b);
        transform.Translate(b); 
    }

    void FireGun()
    {
        shotGun.FireGun(transform.position);
    }   
    
}
