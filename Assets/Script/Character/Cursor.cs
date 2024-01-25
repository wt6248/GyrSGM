using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private void Start()
    {
       //CreateCursor();
    }

    private void Update(){
        // 자이로 센서의 값을 기반으로 커서 이동 등을 수행
    }
    
    //커서가 커서를 생성하는 스택 오버플로우가 발생함. 수정 바람.
    private void CreateCursor()
    {
        GameObject cursorPrefab = Resources.Load<GameObject>("Prefabs/CursorPrefab");
        GameObject cursorObject = Instantiate(cursorPrefab, Vector3.zero, Quaternion.identity);
        cursorObject.transform.parent = transform;
    }

    // 커서 위치 반환 함수
    public Vector3 CursorPosition()
    {
        return transform.position;
    }

    // 커서 위치 리셋 함수
    public void RestCursorPosition()
    {
        transform.position = Vector3.zero;
    }
}
