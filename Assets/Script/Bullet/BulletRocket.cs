using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRocket : BulletScript
{
    public float _explosionRadious;

    // Start is called before the first frame update
    void Start()
    {
        _bulletPrefab = Resources.Load("Prefabs/BulletRocket") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = _speed * Time.deltaTime * _dir;
        transform.Translate(movement);
    }

    // override
    override public void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _explosionRadious, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemy in enemies)
        {
            enemy.gameObject.GetComponent<Entity>().DecreaseHP(_damage);//의 체력깍는 함수 호출
            /*
                explosionDir is calculated based on hit-point = bullet's position
                explosionDir = enemy.position - bullet.position
            */
            Vector3 explosionDir = enemy.gameObject.transform.position - transform.position;
            enemy.gameObject.GetComponent<Entity>().Knockback(explosionDir, _knockbackDistance);
        }
        // destroy whatever hit something
        Destroy(this.gameObject);
    }
}
