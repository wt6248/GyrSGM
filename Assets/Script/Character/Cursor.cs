using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private static Cursor instance;
    private GameObject cursorObject;
    private GyroGameObj gyroGameObj;


    private void Start()
    {
       if(instance == null)
       {
            instance = this;
            CreateCursor();
       }
       else{
            Debug.Log("커서 이미 생성됨");
       }

       gyroGameObj = FindObjectOfType<GyroGameObj>();
    }
    
    //커서가 커서를 생성하는 스택 오버플로우가 발생함. 수정 바람.
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

    void Update()
    {
        // 자이로 값을 가져와서 커서의 위치를 업데이트
        UpdateCursorPosition();
        Debug.Log("커서 이동");
        Debug.Log("Cursor Position: " + transform.position);
    }

    // 커서 위치 변경 함수
    void UpdateCursorPosition()
    {
        // 자이로 값 가져옴
        Vector3 gyroValue = gyroGameObj.GetGyroValue();

        // 자이로 값에 따라 커서의 이동량을 설정
        Vector3 cursorMovement = new Vector3(gyroValue.x, gyroValue.y, 0f) * Time.deltaTime;

        // 커서의 위치를 업데이트
        transform.Translate(cursorMovement);

        // 화면을 벗어나지 않도록 커서의 위치를 제한
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -5f, 5f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -3f, 3f);
        transform.position = clampedPosition;
    }

    // 커서 위치 반환 함수
    public Vector3 CursorPosition()
    {
        Debug.Log("Cursor Position return: " + transform.position);
        return transform.position;
    }

    // 커서 위치 리셋 함수
    public void ResetCursorPosition()
    {
        transform.position = Vector3.zero;
    }
}
