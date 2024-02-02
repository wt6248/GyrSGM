using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunScript : MonoBehaviour
{
    public FixedJoystick fixedJoystick;
    public GameObject shotgun_shell;
    public GameObject Bullet;
    public Button FireButton;
    
    private AudioSource audioSource;
    public AudioClip gunshotSound;

    Vector3 shell_drop_position = new Vector3(4.81f, 0.88f, 0f);

    /*
        산탄총 발사 관련 변수
    */    
    // 총구 위치
    public Transform firePoint; 
    // 적 게임오브젝트
    public GameObject enemyObject;
    // 산탄총 발사 각도
    public float shotgunAngle = 7.5f; 
    // 총알 개수
    public int pelletsCount = 8; 
    // 산탄 투사체 속도 계수
    public float pelletSpeedMultiplier = 50f; 


    // Start is called before the first frame update
    void Start()
    {
        shotgun_shell = Resources.Load("Prefabs/shotgun_shell") as GameObject;
        Bullet = Resources.Load("Prefabs/bullet") as GameObject;
        //fixedJoystick 을 실시간으로 찾아오는 스크립트 작성
        fixedJoystick = GameObject.FindWithTag("GameController").GetComponent<FixedJoystick>();

        //
        audioSource = gameObject.AddComponent<AudioSource>();
        gunshotSound = Resources.Load<AudioClip>("Audio/gunshotSound");

        //fire button manage
        FireButton =  GameObject.Find("Fire Button").GetComponent<Button>();
        FireButton.onClick.AddListener(CustomButton_onClick); //subscribe to the onClick event

        // 자동사격: 적을 찾아서 총을 발사하는 함수를 1초마다 호출
        InvokeRepeating("AutoShoot", 0f, 1f); 
    }

    // Update is called once per frame
    void Update()
    {
        rotateShotgun(fixedJoystick.Direction);
        // transform.LookAt(angle);

        /*
        산탄총 발사
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FindEnemiesAndShoot();
        }
        */        
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
        ShootShotgun_manually();
        generate_shotgun_shell();
        makeFireSound();
    }


    /*
        산탄총을 발사하는 데 필요한 함수들
    */
    // 가까운 적을 찾는 함수
    GameObject FindNearestEnemy()
    {   
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 10f, targetLayer);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 10f, LayerMask.GetMask("Enemy"));
        print(hitEnemies.Length);

        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;
        foreach (Collider2D enemy in hitEnemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < nearestDistance)
            {
                nearestEnemy = enemy.gameObject;
                nearestDistance = distanceToEnemy;
            }
        }

        return nearestEnemy;
    }

    // 산탄총 발사 함수
    void ShootShotgun(GameObject nearestEnemy)
    {
        Vector2 directionToEnemy = (nearestEnemy.transform.position - transform.position).normalized;
        for (int i = 0; i < pelletsCount; i++)
        {
            float randomAngle = Random.Range(-shotgunAngle, shotgunAngle);
            Quaternion randomRotation = Quaternion.AngleAxis(randomAngle, Vector3.forward);
            Vector2 shootDirection = randomRotation * directionToEnemy;

            //GameObject pellet = Instantiate(shotgun_shell, transform.position, Quaternion.identity);
            GameObject pellet = Instantiate(Bullet, transform.position, Quaternion.identity);
            Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = shootDirection * pelletSpeedMultiplier;
            }
        }
        shotgunAngle = vec2angle(directionToEnemy);
        transform.rotation = Quaternion.Euler(0,0,shotgunAngle);
    }

    void ShootShotgun_manually()
    {
        // Vector3 temp_firept = new Vector3( 1.0f, 1.0f , 0);
        // Vector2 directionToEnemy = new Vector2(transform.rotation.eulerAngles.x , transform.rotation.eulerAngles.y);
        Vector2 directionToEnemy = fixedJoystick.Direction;
        for (int i = 0; i < pelletsCount; i++)
        {
            // Debug.Log(i);
            float randomAngle = Random.Range(-shotgunAngle, shotgunAngle);
            Quaternion randomRotation = Quaternion.AngleAxis(randomAngle, Vector3.forward);
            Vector2 shootDirection = randomRotation * directionToEnemy;

            //GameObject pellet = Instantiate(Bullet, temp_firep, Quaternion.identity);
            //GameObject pellet = Instantiate(shotgun_shell, transform.position, Quaternion.identity);
            GameObject pellet = Instantiate(Bullet, transform.position, Quaternion.identity);
            Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = shootDirection * pelletSpeedMultiplier;
            }
        }
        rotateShotgun(directionToEnemy);
    }

    // 적을 찾아 산탄총을 발사하는 함수
    void FindEnemiesAndShoot()
    {
        // 적을 찾기
        GameObject nearestEnemy = FindNearestEnemy();

        // 산탄총 발사
        // 적이 없으면 발사하지 않음
        if (nearestEnemy == null)
        {
            Debug.Log("적이 없음");
        }
        // 가장 가까운 적을 찾아 발사
        else{
            ShootShotgun(nearestEnemy);
            Debug.Log("적을 제거함");
        }
    }

    // 자동 사격 함수
    void AutoShoot()
    {        
        FindEnemiesAndShoot();
        generate_shotgun_shell();
        Debug.Log("자동 사격");
    }   

    float vec2angle(Vector2 v)
    {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    // rotate shotgun along v if v is not zero. if v is zero, do nothing
    void rotateShotgun(Vector2 v)
    {
        if (v != Vector2.zero)
        {
            shotgunAngle = vec2angle(v);
            transform.rotation = Quaternion.Euler(0, 0, shotgunAngle);
        }
    }
}
