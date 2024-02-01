using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Cursor cursor;
    private ShotgunController shotGun;
    private GameObject cursorObject;
    private SpriteRenderer spriteRenderer;
    public GyroGameObj GyroControl;
    private float hp = 10.0f;

    private bool isInvincible = false;
    
    void Start()
    {
      //cursor = GetComponentInChildren<Cursor>();
      CreateCursor();
      cursor = cursorObject.GetComponent<Cursor>();
      shotGun = GetComponentInChildren<ShotgunController>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      //GyroControl = GameObject.Find("Gyro Controller").GetComponent<GyroGameObj>();
    }

    
    void Update()
    {
        //주인공(커서) 움직이기
        //MovePlayerWithCursor();
        MovePlayerWithCursor_modified();
        
        //임시로 주인공 자이로 직접 움직이기
        //tempMovePlayerByGyro();

        //총 발사
        if(Input.GetKeyDown(KeyCode.Space)){ //스페이스바를 누르면 발사
            FireGun();
        }   
    }

    private void CreateCursor()
    {
        Debug.Log("CreateCursor() 메서드 호출됨");
        if(cursorObject == null)
        {
            GameObject cursorPrefab = Resources.Load<GameObject>("Prefabs/CursorPrefab");
            cursorObject = Instantiate(cursorPrefab, Vector3.zero, Quaternion.identity);
            cursorObject.transform.parent = transform;
            Debug.Log("커서 생성");
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

    void MovePlayerWithCursor_modified()
    {
        if(cursor == null)
        {
            cursor = transform.GetChild(1).gameObject.GetComponent<Cursor>();
        }
        Vector3 relativePosition = cursor.CursorLocalPosition();
        transform.Translate(relativePosition * Time.deltaTime * 4); //4는 임의로 정한 속도. 너무 느려서 배속 넣음

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
    private void Unbeatable() {
        isInvincible = !isInvincible;

        if (isInvincible) {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        } else {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    } 
    void getDamaged(float damage=3.0f, float invincible_time=0.5f) 
    {
        if (isInvincible) {
            Debug.Log("Invincible!");
            return;
        }


        hp -= damage;
        Debug.Log("Player HP is " + hp);
        if (hp <= 0.0f) {
            Debug.Log("Player Died");
        }

        // 무적 
        Unbeatable();
        Invoke("Unbeatable", invincible_time);


    }
    void OnCollisionEnter2D(Collision2D other) {
        bool local_debug = false;
        int n_attack = local_debug ? 2:1;

        for (int i=0;i<n_attack;i++){
            getDamaged(3.0f);
        }

        // test code for the function operates well
        if (hp > 0 && local_debug) {
            other.gameObject.transform.position = new Vector3(10, 10, 0);
        }
        
        

    }
   
}
