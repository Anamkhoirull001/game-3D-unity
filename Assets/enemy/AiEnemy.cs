using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEnemy : MonoBehaviour
{
    public Transform player;
    static Animator anim;
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
      anim = GetComponent<Animator> ();   
    }

    // Update is called once per frame
    void Update()
    {
       if(Vector3.Distance (player.position, this.transform.position) < 30){
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime);
            anim.SetBool ("IsIdle", false);

            if (direction.magnitude > 2){
                this.transform.Translate(0, 0, speed);
                anim.SetBool("IsWalking", true);
                anim.SetBool("IsAttack", false);
            }else{
                anim.SetBool("IsAttack", true);
                anim.SetBool("IsWalking", false);
            }
       } else{
            anim.SetBool ("IsIdle", true);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsAttack", false);
       }
    }

        private void OnTriggerEnter (Collider other){
        if(other.gameObject.CompareTag("PlayerAttack")){
            anim.SetBool ("enemyPusing", true);
        }else{
            anim.SetBool("enemyPusing", false);
        }
    }
}
