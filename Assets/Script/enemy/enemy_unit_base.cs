using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class enemy_unit_base : MonoBehaviour
{
    private bool is_move = true;
    protected int hp = 3;

    // audio instances
    public AudioSource hurt_sound_source;
    public AudioClip hurt_sound;
    public AudioSource dead_sound_source;
    public AudioClip dead_sound;

    // player size
    public const float enemyRadious = 0.5f; // if chose circle collider
    public readonly Vector2 enemySize = new(0.5f, 0.5f); // if chose box collider

    void Start() {

        /*
            This is disabled since it fixes every enemy's initial coordinate
            Look EnemyManage.cs
        */
        // Vector3 init_pos = new Vector3(5f, 5f, 0f);
        // transform.position = init_pos; 
    


        // init audio
        hurt_sound_source = gameObject.AddComponent<AudioSource>();
        hurt_sound = Resources.Load<AudioClip>("Audio/dspunch");
        dead_sound_source = gameObject.AddComponent<AudioSource>();
        dead_sound = Resources.Load<AudioClip>("Audio/dsbgdth1");

        /*
            if enemy has circle collider
        */
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius = enemyRadious;
        /*
            if enemy has box collider
        */
        // BoxCollider2D collider = GetComponent<BoxCollider2D>();
        // collider.size = enemySize;
    } 
    void Update()
    {
        //Debug.Log("enemy: (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")\n");
        Move();
    }

    private void Move()
    {
        Vector3 player_pos = get_player_pos(); 
        
        if ((transform.position - player_pos).magnitude > 1) {
            //transform.Translate((player_pos - transform.position).normalized * 0.005f);
            transform.Translate((player_pos - transform.position).normalized * Time.deltaTime);
        } 
    }
    public void DecreaseHp() {

        hp--;
        if(hurt_sound != null && hp > 0f) {
            hurt_sound_source.PlayOneShot(hurt_sound);
        }
        //Debug.Log(hp);
        if (hp<=0) {
            if(dead_sound != null) {
                dead_sound_source.PlayOneShot(dead_sound);
            }
            Destroy(this.gameObject);
        }
        return;
    }
    private Vector3 get_player_pos() {
        Transform player_info = GameObject.Find("Main Character").GetComponent<Transform>();
        Vector3 player_pos = player_info.position;
        //Debug.Log("player_pos: (" + player_pos.x + ", " + player_pos.y + ", " + player_pos.z + ")\n");
        return player_pos;
    }
}

