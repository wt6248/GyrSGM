using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float speed, directionX, directionY;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        directionX = 0.3f;
        directionY = 0.7f;
        Destroy(gameObject, 10);
        
    }

    // Update is called once per frame
    void Update()
    {
        float speedX = speed * directionX * Time.deltaTime;
        float speedY = speed * directionY * Time.deltaTime;
        
        transform.Translate(speedX,speedY,0f);
        
    }

    //적과 충돌했을 때 적의 체력을 깎는 함수를 호출하고 사라짐.
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy"))
        {
            //other.gameObject.getcomponent<ScriptName>() 의 체력깍는 함수 호출
            //Destroy(this);

        }
    }
    
    //처음 시작할 때 주어진 속도에 따라 움직이는 코드 작성
    public void set_velocity(float givenSpeed, Vector2 givenDirection)
    {
        speed = givenSpeed;
        directionX = givenDirection.x;
        directionY = givenDirection.y;
    }


}