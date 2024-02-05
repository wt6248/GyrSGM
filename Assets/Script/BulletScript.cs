using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 0f, directionX, directionY;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        //샷건에서 총알 생성을 초기화 하고 난 뒤에 start가 적용되서, speed, direction을 정할 수 없는 문제가 있었음.
        // if(speed == 0f)
        // speed = 0.1f;
        //directionX = 0.1f;
        //directionY = 0.1f;
        Destroy(gameObject, 3);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = direction * speed * Time.deltaTime;
        transform.Translate(movement);
        
    }

    //적과 충돌했을 때 적의 체력을 깎는 함수를 호출하고 사라짐.
    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("hitted!");
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyUnitBase>().DecreaseHP();//의 체력깍는 함수 호출
        }
        // destroy whatever hit something
        Destroy(this.gameObject);
    }
    
    //처음 시작할 때 주어진 속도에 따라 움직이는 코드 작성
    public void set_velocity(float givenSpeed, Vector2 givenIdentityDirection)
    {
        speed = givenSpeed;
        direction = givenIdentityDirection;
    }

    public void set_velocity(float givenSpeed, float givenEulerAngle)
    {
        speed = givenSpeed;
        directionX = Mathf.Cos(givenEulerAngle* Mathf.Deg2Rad);
        directionY = Mathf.Sin(givenEulerAngle* Mathf.Deg2Rad);
        direction = new Vector2(directionX, directionY);
    }


}