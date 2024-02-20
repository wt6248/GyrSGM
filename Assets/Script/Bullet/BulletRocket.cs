using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRocket : BulletScript
{
    public float _explosionRadious;


    // Start is called before the first frame update
    void Start()
    {
        //차후 inspector에서 knockbackDistance 값을 저장하면 아래는 삭제할 것.
        _knockbackDistance = 2f;
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
        Collider2D[] entities = Physics2D.OverlapCircleAll(transform.position, _explosionRadious, LayerMask.GetMask("Enemy"));
        foreach (Collider2D entity in entities)
        {
            Vector3 explosionDir = entity.gameObject.transform.position - transform.position;
            AttackEntity(entity, explosionDir);
        }
        // destroy whatever hit something
        if(other.gameObject.name != "Main Character")
        {
            Destroy(this.gameObject);
        }
    }
}
