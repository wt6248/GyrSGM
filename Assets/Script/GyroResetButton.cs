using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GyroResetButton : MonoBehaviour
{
    public void ResetGyroscope()
    {
        // 자이로스코프 비활성화
        Input.gyro.enabled = false; 
        // 자이로스코프 재활성화
        Input.gyro.enabled = true; 
        // 디버깅: 함수 작동 여부
        Debug.Log("자이로 초기화");
    }
}