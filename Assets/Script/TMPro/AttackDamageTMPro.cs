using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class AttackDamageTMPro : MonoBehaviour
{
    // 텍스트를 표시할 textMeshProUGUI 컴포넌트
    public TextMeshProUGUI DamageTMPro;
    // Damage를 참조할 오브젝트
    public PlayerController playerController;
    // 현재 Damage를 나타내는 변수
    float _currentDamage;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        DamageTMPro = GetComponent<TextMeshProUGUI>();
        GetDamagePoint();
    }

    void Update()
    {
        GetDamagePoint();
    }

    public void GetDamagePoint()
    {
        // PlayerController.cs에서 damage 값을 가져와서 텍스트로 표시
        if (playerController != null && DamageTMPro != null)
        {
            _currentDamage = playerController.AttackDamageManager();
            DamageTMPro.text = "Damage: " + _currentDamage.ToString("F1");
        }
        else if (playerController == null)
        {
            DamageTMPro.text = "Damage: 0.0";
        }
        else
        {
            // 디버깅 오류처리
            Debug.LogWarning("DamageTMPro가 설정되지 않았습니다.");
        }
    }
}