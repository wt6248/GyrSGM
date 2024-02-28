using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResetButton : MonoBehaviour
{
    private Cursor cursor;
    void Start()
    {
        cursor = FindObjectOfType<Cursor>();
    }

    // 리셋 버튼 클릭 시 호출될 함수
    public void OnResetButtonClicked()
    {
        if (cursor == null)
        {
            Debug.Log("커서를 찾을 수 없음");
        }
        else
        {
            // 커서 위치를 초기화함
            cursor.ResetCursorPosition();
            Debug.Log("커서 위치 초기화");
        }
    }

}