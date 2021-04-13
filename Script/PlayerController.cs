using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
	Vector3 latestPos;

	public GameObject mainCamera;
	Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
		latestPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

		
			
		if(Input.GetAxis("Horizontal")== 0 && Input.GetAxis("Vertical") == 0)
        {

        }
        else
        {
			Vector3 cameraFoward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
			Vector3 moveFoward = -cameraFoward * Input.GetAxis("Vertical") + -mainCamera.transform.right * Input.GetAxis("Horizontal");
			//rb.velocity = moveFoward * speed + new Vector3(0,rb.velocity.y, 0);
			//rb.velocity = moveFoward * speed + Vector3.forward;

			if (moveFoward != Vector3.zero)
			{
				transform.rotation = Quaternion.LookRotation(moveFoward);
			}
			transform.position += -moveFoward * 0.5f;

			//transform.position = transform.position * Input.GetAxis("Horizontal") * Input.GetAxis("Vertical") * speed;
        }

		/*if (Input.GetAxis("Horizontal") > 0)
		{
			transform.position += speed * new Vector3(0.1f, 0, 0);
			//moving = true;
			
		}
		if (Input.GetAxis("Horizontal") < 0)
		{
			transform.position += speed * new Vector3(-0.1f, 0, 0);
			//moving = true;
			
		}
		if (Input.GetAxis("Vertical") > 0)
		{
			transform.position += speed * new Vector3(0, 0, 0.1f);
			//moving = true;
			
		}
		if (Input.GetAxis("Vertical") < 0)
		{
			transform.position += speed * new Vector3(0, 0, -0.1f);
			//moving = true;
			
		}*/

		Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
		latestPos = transform.position;  //前回のPositionの更新

		if (diff.magnitude > 0.01f)
		{
			transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
		}
	}
}
