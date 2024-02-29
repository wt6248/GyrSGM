using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGunButton : MonoBehaviour
{
    public GameObject gunType1;
    public GameObject gunType2;
    public GameObject gunType3;
    public ShotgunController CatrigeChangeScript;
    // GunType1, GunType2, GunType3 이미지들을 저장할 배열
    public Image[] gunImages; 
    // 이미지 배열의 인덱스
    public int currentImageIndex1 = 0; 
    public int currentImageIndex2 = 1; 
    public int currentImageIndex3 = 2; 

    void Start()
    {
        gunType1 = GameObject.Find("GunType_1");
        gunType2 = GameObject.Find("GunType_2");
        gunType3 = GameObject.Find("GunType_3");

        // 초기 이미지 설정
        //UpdateImageUp();
    }

    public void ChangeImageUp()
    {
        UpdateImageUp();
        CatrigeChangeScript.changeCatrigeTypeReverse();
    }

    public void ChangeImageDown()
    {
        UpdateImageDown();
        CatrigeChangeScript.changeCatrigeType();
    }

    void UpdateImageUp()
    {
        //3개의 총 이미지 오브젝트 스프라이트 변경하기
        Image imageComponent1 = gunType1.GetComponent<Image>();
        Image imageComponent2 = gunType2.GetComponent<Image>();
        Image imageComponent3 = gunType3.GetComponent<Image>();
        Sprite temp = imageComponent1.sprite;

        imageComponent1.sprite = imageComponent2.sprite;
        imageComponent2.sprite = imageComponent3.sprite; 
        imageComponent3.sprite = temp;
    }

    void UpdateImageDown()
    {
        //3개의 총 이미지 오브젝트 스프라이트 변경하기
        Image imageComponent1 = gunType1.GetComponent<Image>();
        Image imageComponent2 = gunType2.GetComponent<Image>();
        Image imageComponent3 = gunType3.GetComponent<Image>();
        Sprite temp = imageComponent1.sprite;

        imageComponent1.sprite = imageComponent3.sprite;
        imageComponent3.sprite = imageComponent2.sprite;
        imageComponent2.sprite = temp;        
    }
}
