using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip gunshotSound;

    // Start is called before the first frame update
    private void Start()
    {
        CreateShotGun();
        
        audioSource = gameObject.AddComponent<AudioSource>();
        gunshotSound = Resources.Load<AudioClip>("Audio/gunshotSound");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateShotGun(){
        GameObject shotgunPrefab = Resources.Load<GameObject>("Prefabs/ShotgunPrefab");
        GameObject shotgunObject = Instantiate(shotgunPrefab, Vector3.zero, Quaternion.identity);
        shotgunObject.transform.parent = transform;
    }

    // 총 쏘는 함수
    public void FireGun(Vector3 playerPosition){
        if(gunshotSound != null)
        {
            audioSource.PlayOneShot(gunshotSound);
        }
    }
}
