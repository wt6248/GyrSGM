using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunScript : MonoBehaviour
{
    public FixedJoystick fixedJoystick;
    public GameObject shotgun_shell;
    public Button FireButton;
    
    private AudioSource audioSource;
    public AudioClip gunshotSound;
    
    Vector3 shell_drop_position = new Vector3(4.81f, 0.88f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        shotgun_shell = Resources.Load("Prefabs/shotgun_shell") as GameObject;
        //fixedJoystick 을 실시간으로 찾아오는 스크립트 작성
        fixedJoystick = GameObject.FindWithTag("GameController").GetComponent<FixedJoystick>();

        //
        audioSource = gameObject.AddComponent<AudioSource>();
        gunshotSound = Resources.Load<AudioClip>("Audio/gunshotSound");

        //fire button manage
        FireButton =  GameObject.Find("Fire Button").GetComponent<Button>();
        FireButton.onClick.AddListener(CustomButton_onClick); //subscribe to the onClick event
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 angle = fixedJoystick.Direction;

        float rotZ = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);        
        // transform.LookAt(angle);
    }

    public void generate_shotgun_shell()
    {
        //GameObject shotgun_shell_temp = Instantiate(shotgun_shell, shell_drop_position,Quaternion.identity, transform);
        GameObject shotgun_shell_temp = Instantiate(shotgun_shell, transform, true );
        shotgun_shell_temp.transform.localPosition = shell_drop_position;
    }

    public void makeFireSound(){
        if(gunshotSound != null)
        {
            audioSource.PlayOneShot(gunshotSound);
        }
    }


    void Awake()
    {
        
    }

    //Handle the onClick event
    void CustomButton_onClick()
    {
        //Debug.Log("testtttt");
        generate_shotgun_shell();
        makeFireSound();
    }
}
