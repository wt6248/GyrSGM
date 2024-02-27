using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGun : MonoBehaviour
{
    public GameObject gunTypePrefab; // Canvas의 Transform 컴포넌트
    public GameObject obj;
    public Image[] gunImages; // GunType1, GunType2, GunType3 이미지들을 저장할 배열
    public int currentImageIndex = 0; // 현재 이미지의 인덱스를 나타내는 변수

    void Start()
    {
        gunTypePrefab = Resources.Load("Prefabs/GunType") as GameObject;
        // GunType1, GunType2, GunType3 이미지들을 가져오기
        gunImages = new Image[3];
        gunImages[0] = gunTypePrefab.transform.Find("Canvas/GunType1").GetComponent<Image>();
        gunImages[1] = gunTypePrefab.transform.Find("Canvas/GunType2").GetComponent<Image>();
        gunImages[2] = gunTypePrefab.transform.Find("Canvas/GunType3").GetComponent<Image>();

        // 초기 이미지 설정
        UpdateImage();
    }

    // 버튼을 클릭할 때 호출될 함수
    public void ChangeImage()
    {
        // 다음 이미지 인덱스로 변경
        currentImageIndex = (currentImageIndex + 1) % 3;
        UpdateImage();
    }

    // 현재 이미지 업데이트 함수
    void UpdateImage()
    {
        Sprite updateSprite = gunImages[currentImageIndex].sprite;
        gunTypePrefab.GetComponent<Image>().sprite = updateSprite;
    }
}
