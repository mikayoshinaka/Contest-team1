using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("速さの最低値")]
    [SerializeField]
    private float minSpeed = 3.0f;
    [Header("速さの最高値")]
    [SerializeField]
    private float maxSpeed = 5.0f;
    [Header("プレイヤーの方向転換スピードの調整値")]
    [SerializeField, Range(0.0f, 1.0f)]
    private float turnRate = 0.3f;

    private Vector3 velocity;

    private CharacterController controller;

    private void OnValidate()
    {
        this.turnRate = Mathf.Clamp(this.turnRate, 0.0f, 1.0f);
    }

    void Awake()
    {
        this.controller = this.GetComponent<CharacterController>();

        
        this.velocity = Vector3.zero;

    }

    
    void Update()
    {
        Vector3 vec = this.velocity;
        float Speed = 0.0f;

       
        if (this.controller.isGrounded)
        {
           
            vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

           
            if (vec.magnitude > 0.1f)
            {
               
                Speed = Mathf.Lerp(this.minSpeed, this.maxSpeed, vec.magnitude);

                
                Vector3 dir = vec.normalized;
                float rotate = Mathf.Acos(dir.z);
                if (dir.x < 0) rotate = -rotate;
                rotate *= Mathf.Rad2Deg;
                Quaternion Q = Quaternion.Euler(0, rotate, 0);
               
                this.transform.rotation = Quaternion.Slerp(
                          this.transform.rotation, Q, this.turnRate
                          );
            }

           
            vec = vec.normalized;
        }

       
        this.velocity.x = vec.x * Speed;
        this.velocity.y = vec.y;
        this.velocity.z = vec.z * Speed;

        
        this.velocity.y += Physics.gravity.y * Time.deltaTime;

        this.controller.Move(this.velocity * Time.deltaTime);

    }

}
