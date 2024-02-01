using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunScript : MonoBehaviour
{
    public FixedJoystick fixedJoystick;
    public Button FireButton;

    public GameObject shotgun_shell;
    public GameObject Bullet;
    Vector3 shell_drop_position = new Vector3(4.81f, 0.88f, 0f);
    
    private AudioSource audioSource;
    public AudioClip gunshotSound;


    /*
        산탄총 발사 관련 변수
    */    
    // 총구 위치
    public Vector3 firePoint = new(8f, 0.88f, 0f);
    // 적 게임오브젝트
    public GameObject enemyObject;
    // 산탄총 발사 각도
    public float shotgunAngle = 0;
    // Spreading angle
    public float spreadAngle = 15f;
    // 총알 개수
    public int pelletsCount = 8; 
    // 산탄 투사체 속도 계수
    public float pelletSpeedMultiplier = 50f; 
    // Radious of auto-aim
    const float autoAimRadious = 10f;


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
        AutoAim();
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
        shotgun_shell_temp.transform.SetParent(null);
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
        ShootShotgun();
        generate_shotgun_shell();
        makeFireSound();
    }


    /*
        산탄총을 발사하는 데 필요한 함수들
    */
    // 가까운 적을 찾는 함수
    GameObject FindNearestEnemy()
    { 
        // // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 10f, targetLayer);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 10f, LayerMask.GetMask("Enemy"));
        // killed every enemy || no enemy detected
        if (hitEnemies == null)
        {
            return null;
        }

        GameObject nearestEnemy = null;
        float nearestDistance = autoAimRadious;
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
    void ShootShotgun()
    {
        for (int i = 0; i < pelletsCount; i++)
        {
            float randomAngle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion error = Quaternion.AngleAxis(randomAngle, Vector3.forward);
            Vector2 shootDirection = error * transform.rotation * Vector3.right;

            GameObject pellet = Instantiate(Bullet, transform.position + firePoint, Quaternion.identity);
            Debug.Log(pellet.transform.position);
            // Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();

            // if (rb != null)
            // {
            //     rb.velocity = shootDirection * pelletSpeedMultiplier;
            // }
            // TODO : fix this!!!!!
            pellet.GetComponent<BulletScript>().set_velocity(0.4f, shootDirection.normalized);
        }
    }

    // 자동 사격 함수
    void AutoShoot()
    {        
        ShootShotgun();
        generate_shotgun_shell();
        Debug.Log("자동 사격");
    }   

    float Vec2angle(Vector2 v)
    {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    /*
        if v is zero vector -> rotate shotgun along v
        else                -> do nothing
    */
    void RotateShotgun(Vector2 v)
    {
        if (v != Vector2.zero)
        {
            shotgunAngle = Vec2angle(v);
            if (-90 < shotgunAngle && shotgunAngle < 90)
            {
                transform.localScale = new(0.2f, 0.2f, 1);
            }
            else
            {
                transform.localScale = new(0.2f, -0.2f, 1);
            }
            transform.rotation = Quaternion.Euler(0, 0, shotgunAngle);
        }
    }

    void AutoAim()
    {
        // Not holding joystick
        if (fixedJoystick.Direction == Vector2.zero)
        {
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                RotateShotgun(nearestEnemy.transform.position - transform.position);
            }
        }
        else
        {
            RotateShotgun(fixedJoystick.Direction);
        }
    }
}
