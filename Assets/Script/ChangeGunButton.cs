using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGun : MonoBehaviour
{
    public GameObject gunTypePrefab; 
    public GameObject gunType1;
    public GameObject gunType2;
    public GameObject gunType3;
    // GunType1, GunType2, GunType3 이미지들을 저장할 배열
    public Image[] gunImages; 
    // 이미지 배열의 인덱스
    public int currentImageIndex1 = 0; 
    public int currentImageIndex2 = 1; 
    public int currentImageIndex3 = 2; 

    void Start()
    {
        gunTypePrefab = Resources.Load("Prefabs/GunType") as GameObject;
        gunType1 = GameObject.Find("GunType_1");
        gunType2 = GameObject.Find("GunType_2");
        gunType3 = GameObject.Find("GunType_3");
       
        // GunType1, GunType2, GunType3 이미지들 가져오기
        gunImages = new Image[3];
        gunImages[0] = gunTypePrefab.transform.Find("Canvas/GunType1").GetComponent<Image>();
        gunImages[1] = gunTypePrefab.transform.Find("Canvas/GunType2").GetComponent<Image>();
        gunImages[2] = gunTypePrefab.transform.Find("Canvas/GunType3").GetComponent<Image>();

        // 초기 이미지 설정
        UpdateImage();
    }

    public void ChangeImage()
    {
        // 다음 이미지 인덱스로 변경
        currentImageIndex1 = (currentImageIndex1 + 1) % 3;
        currentImageIndex2 = (currentImageIndex2 + 1) % 3;
        currentImageIndex3 = (currentImageIndex3 + 1) % 3;

        UpdateImage();
    }

    void UpdateImage()
    {
        //3개의 총 이미지 오브젝트 스프라이트 변경하기
        Image imageComponent1 = gunType1.GetComponent<Image>();
        Image imageComponent2 = gunType2.GetComponent<Image>();
        Image imageComponent3 = gunType3.GetComponent<Image>();

        imageComponent1.sprite = gunImages[currentImageIndex1].sprite;
        imageComponent2.sprite = gunImages[currentImageIndex2].sprite;
        imageComponent3.sprite = gunImages[currentImageIndex3].sprite;
    }
}
