using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class HealthPointTMPro : MonoBehaviour
{
    // 텍스트를 표시할 textMeshProUGUI 컴포넌트
    public TextMeshProUGUI HpTMPro;
    // Hp를 참조할 오브젝트
    public PlayerController playerController;
    // 현재 Hp를 나타내는 변수
    float _currentHealthPoint;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        HpTMPro = GetComponent<TextMeshProUGUI>();
        GetHealthPoint();
    }

    void Update()
    {
        GetHealthPoint();
    }

    public void GetHealthPoint()
    {
        // PlayerController.cs에서 hp 값을 가져와서 텍스트로 표시
        if (playerController != null && HpTMPro != null)
        {
            _currentHealthPoint = playerController.HealthPointManager();
            HpTMPro.text = "HP: " + _currentHealthPoint.ToString("F2");
        }
        else if (playerController == null)
        {
            HpTMPro.text = "HP: 0.00";
        }
        else
        {
            // 디버깅 오류처리
            Debug.LogWarning("HpTMPro가 설정되지 않았습니다.");
        }
    }
}