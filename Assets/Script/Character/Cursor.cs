using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private static Cursor instance;
    private GameObject cursorObject;
    private GyroGameObj gyroGameObj;
    public StopButton stopButton;

    private void Start()
    {
        gyroGameObj = FindObjectOfType<GyroGameObj>();


        // hold button to stop
        stopButton = FindObjectOfType<StopButton>();
    }

    void Update()
    {
        // 자이로 값을 가져와서 커서의 위치를 업데이트
        UpdateCursorPosition();
    }

    // 커서 위치 변경 함수
    void UpdateCursorPosition()
    {
        // 자이로 값 가져옴
        Vector3 gyroValue = gyroGameObj.GetGyroValue();

        // 자이로 값에 따라 커서의 이동량을 설정
        Vector3 cursorMovement = new Vector3(gyroValue.x, gyroValue.y, 0f) * Time.deltaTime;

        // 커서의 위치를 업데이트
        if (stopButton.IsPressed() == false)
        {
            transform.Translate(cursorMovement);
        }
        else
        {
            ResetCursorPosition();
        }

        // 화면을 벗어나지 않도록 커서의 위치를 제한
        // Vector3 clampedPosition = transform.position;
        // clampedPosition.x = Mathf.Clamp(clampedPosition.x, -5f, 5f);
        // clampedPosition.y = Mathf.Clamp(clampedPosition.y, -3f, 3f);
        // transform.position = clampedPosition;
    }

    // 커서 위치 반환 함수
    public Vector3 CursorPosition()
    {
        //Debug.Log("Cursor Position return: " + transform.position);
        return transform.position;
    }

    public Vector3 CursorLocalPosition()
    {
        //Debug.Log("Cursor Position return: " + transform.position);
        return transform.localPosition;
    }

    // 커서 위치 리셋 함수
    public void ResetCursorPosition()
    {
        transform.localPosition = Vector3.zero;
    }
}
