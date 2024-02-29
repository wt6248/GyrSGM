using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    public bool _canShoot = false;
    public Catrige enemyCartrige;
    Entity player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        // autoshooting function
        StartCoroutine(AutoShootCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        if (_canShoot)
        {
            AutoShoot();
        }
    }

    // autoshooting function
    void AutoShoot()
    {
        Vector3 fireDirection = (player.transform.position - transform.position).normalized;
        Vector3 firePosition = transform.position - new Vector3(0, 0, -0.1f);
        enemyCartrige.FireCatrige(fireDirection,1f,Entity.EntityType.Player,firePosition);

        //발사하면 _canShoot false로 수정.
        _canShoot = false;
    }
    IEnumerator AutoShootCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(4.5f);
            _canShoot = true;
        }
    }

    public void setCatrige(string name = "EnemySingle")
    {
        
        GameObject a = GameObject.Find("/Catrige/" + name);
        if(a == null)
            Debug.Log("cannot find enemy catrige");
        enemyCartrige = a.GetComponent<Catrige>();
    }

}
