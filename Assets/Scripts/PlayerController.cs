using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{	
	public float speed;
	public GUIText countText;
	public GUIText winText;
	private int count;
    private bool is3D = false;
    private Transform playerTransform;
    public Transform camTransform;
    public Camera cam;
    public float playerJump = 10;
    private LayerMask mask = -1;
    public CanvasGroup canGroup;
    private float currTime;
    public float fadeSpeed = 5;
    private float lerpTime;
	
	void Start()
	{
		count = 0;
		winText.text = "";
        playerTransform = GetComponent<Transform>();
	}
	
	void Update ()
	{
        camTransform.localPosition = new Vector3(playerTransform.localPosition.x, camTransform.localPosition.y, -10);
        
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(0,0,0);

        if (!is3D)
        {
            movement = new Vector3(moveHorizontal, 0.0f, 0);
        }
        else
        {
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        }

        GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (is3D)
            {
                camTransform.Rotate(-45, 0, 0);
                camTransform.localPosition = new Vector3(playerTransform.localPosition.x, 1, playerTransform.localPosition.z - 10);
                playerTransform.transform.localPosition = new Vector3(playerTransform.localPosition.x, playerTransform.localPosition.y, 0);
                cam.orthographic = true;
                Debug.Log("2d");
                is3D = false;
                BroadcastMessage("SwitchTo2D");
            } else if (!is3D)
            {
                camTransform.Rotate(45,0,0);
                cam.orthographic = false;
                camTransform.localPosition = new Vector3(playerTransform.localPosition.x, 8.5f, playerTransform.localPosition.z - 10);
                Debug.Log("3d");
                is3D = true;
               

            }


        }
        if(!is3D)
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 0);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, playerJump, GetComponent<Rigidbody>().velocity.z);
        }
    }
	
	void OnTriggerEnter(Collider other) 
	{
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count = count + 1;
        }
        if(other.gameObject.tag == "Reset")
        {
            playerTransform.localPosition = new Vector3(0, 0.5f, 0);
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "StartText")
        {
            lerpTime += fadeSpeed * Time.deltaTime;
            canGroup.alpha = -Mathf.Lerp(1, 0, lerpTime) + 1;
        }
    }


    bool IsGrounded()
    {
        return Physics.Raycast(playerTransform.position, -Vector3.up, 0.6f, mask);
    }


	
	
}
