using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emove : MonoBehaviour
{

    CharacterController _controller;
    Transform target;
    GameObject Player;

    [SerializeField]
    float _moveSpeed = 2.0f;


    // Use this for initialization
     IEnumerator Start()
 {
     yield return new WaitForSeconds(10f);
    
        Player = GameObject.FindWithTag("Player"); 
        target = Player.transform;
    


        _controller = GetComponent<CharacterController>();

 }

    

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        

        direction = direction.normalized;
        
        Vector3 velocity = direction * _moveSpeed;

        _controller.Move(velocity * Time.deltaTime);


    }
}