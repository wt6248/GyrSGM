using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Cursor cursor;
    private ShotGun shotGun;

    
    void Start()
    {
      cursor = GetComponentInChildren<Cursor>();
      shotGun = GetComponentInChildren<ShotGun>();
    }

    
    void Update()
    {
        //주인공(커서) 움직이기
        MovePlayerWithCursor();

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

    void FireGun()
    {
        shotGun.FireGun(transform.position);
    }   
    
}
