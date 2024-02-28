using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class AttackSpeedTMPro : MonoBehaviour
{
    // 텍스트를 표시할 textMeshProUGUI 컴포넌트
    public TextMeshProUGUI SpeedTMPro;
    // Speed를 참조할 오브젝트
    public PlayerController playerController;
    // 현재 Speed를 나타내는 변수
    float _currentAttackSpeed;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        SpeedTMPro = GetComponent<TextMeshProUGUI>();
        GetAttackSpeed();
    }

    void Update()
    {
        GetAttackSpeed();
    }

    public void GetAttackSpeed()
    {
        // PlayerController.cs에서 _attackSpeed 값을 가져와서 텍스트로 표시
        if (playerController != null && SpeedTMPro != null)
        {
            _currentAttackSpeed = playerController.AttackSpeedManager();
            SpeedTMPro.text = "Speed: " + _currentAttackSpeed.ToString("F2");
        }
        else if (playerController == null)
        {
            SpeedTMPro.text = "Speed: 0.00";
        }
        else
        {
            // 디버깅 오류처리
            Debug.LogWarning("SpeedTMPro가 설정되지 않았습니다.");
        }
    }
}