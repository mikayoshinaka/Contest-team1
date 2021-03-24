using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePlayerController : MonoBehaviour
{
	//public float speed = 5.0f;
	public GameObject start;

	Vector3 playerPos;
	Vector3 direction;
	RaycastHit RH;
	float speed = 0.15f; // 0.05fから

	bool moveon = false;
	// Start is called before the first frame update
	void Start()
    {
        
    }

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) {
			moveon = !moveon;
		}
		if (moveon)
		{
			if (Physics.Raycast((Camera.main.ScreenPointToRay(Input.mousePosition)), out RH, 100))
			{
				transform.position += transform.forward * speed;
			}
			playerPos = this.transform.position;
			direction = RH.point - playerPos;
			transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		}


		/*if (Input.GetKey("up"))
		{
			transform.position += transform.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey("down"))
		{
			transform.position -= transform.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey("right"))
		{
			transform.position += transform.right * speed * Time.deltaTime;
		}
		if (Input.GetKey("left"))
		{
			transform.position -= transform.right * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.LeftShift))
		{
			transform.Rotate(0,-1,0);
		}
		else if (Input.GetKey(KeyCode.RightShift))
		{
			transform.Rotate(0,1,0);
		}
        if (Input.GetKeyUp(KeyCode.Space))
        {
			Instantiate(start, transform.position, Quaternion.identity);
		}*/
	}
}
