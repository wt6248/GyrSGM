using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : Bullet
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = _speed * Time.deltaTime * _dir;
        transform.Translate(movement);
    }

    //적과 충돌했을 때 적의 체력을 깎는 함수를 호출하고 사라짐.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("hitted!");
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Entity>().DecreaseHP(_damage);//의 체력깍는 함수 호출
        }
        // destroy whatever hit something
        Destroy(this.gameObject);
    }

    //처음 시작할 때 주어진 속도에 따라 움직이는 코드 작성
    public void SetVelocity(float speed, Vector3 direction)
    {
        _speed = speed;
        _dir = direction;
    }

    public void SetVelocity(float speed, float eulerAngle)
    {
        _speed = speed;
        float directionX = Mathf.Cos(eulerAngle * Mathf.Deg2Rad);
        float directionY = Mathf.Sin(eulerAngle * Mathf.Deg2Rad);
        _dir = new(directionX, directionY, 0);
    }
}