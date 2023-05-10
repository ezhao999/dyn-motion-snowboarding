using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emove : MonoBehaviour
{

    CharacterController _controller;
    Transform target;
    GameObject Player;

    public float distance = 0f;

    [SerializeField]
    float _moveSpeed = 3.5f;


    // Use this for initialization
     IEnumerator Start()
 {
     yield return new WaitForSeconds(20f);
    
        Player = GameObject.FindWithTag("Player"); 
        target = Player.transform;
    


        _controller = GetComponent<CharacterController>();

 }

    

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        distance = direction.magnitude;
        if (_moveSpeed<5.0f){
            _moveSpeed+=0.1f;
        }
        else {
            _moveSpeed-=0.1f;
        }

        direction = direction.normalized;
        
        Vector3 velocity = direction * _moveSpeed;

        _controller.Move(velocity * Time.deltaTime);


    }
}