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
        // 커서와 플레이어 간의 상대적인 위치 계산
        Vector3 relativePosition = cursor.CursorPosition() - transform.position;

        // 플레이어와 커서 간의 거리 계산
        float distance = relativePosition.magnitude;

        // 플레이어의 이동 속도를 거리에 따라 조절 (임의로 0.01f로 설정)
        float speed = distance * 0.01f;

        // 상대적인 위치를 정규화하여 이동 방향을 설정하고 속도를 적용
        Vector3 moveDirection = relativePosition.normalized * speed;

        // 플레이어 이동
        transform.position += moveDirection;
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
