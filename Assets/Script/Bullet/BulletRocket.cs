using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRocket : BulletScript
{
    // Start is called before the first frame update
    void Start()
    {
        _damage = 10;
        _speed = 1f;
        _pelletCount = 1;
        _spreadAngle = 0f;
        _duration = 3f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FIXME : override
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("hitted!");
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Entity>().DecreaseHP(_damage);//의 체력깍는 함수 호출
        }
        // destroy whatever hit something
        Destroy(this.gameObject);
    }
}
