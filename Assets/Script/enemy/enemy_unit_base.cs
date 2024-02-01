using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace GyrSGM.Assets.Script.enemy
{
    public class enemy_unit_base : MonoBehaviour
    {
        private bool is_move = true;
        protected int hp = 3;

        void Start() {
            Vector3 init_pos = new Vector3(10f, 10f, 0f);
            transform.position = init_pos; 
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
                transform.Translate((player_pos - transform.position).normalized * 0.005f);
            } 
        }
        public void DecreaseHp() {
            hp--;
            if (hp<=0) {
                Destroy(this);
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
}


