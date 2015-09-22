using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {
    public float stretchedZ = 100000;
    public float flatZ = 10;
    private BoxCollider tileTransform;
    private bool is3D = false;
	// Use this for initialization
	void Start () {
        tileTransform = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (is3D)
            {
                tileTransform.size = new Vector3(tileTransform.size.x, tileTransform.size.y, stretchedZ);
                Debug.Log("STretch");
                is3D = false;
            }
            else if (!is3D)
            {

                tileTransform.size = new Vector3(tileTransform.size.x, tileTransform.size.y, flatZ);
                Debug.Log("Flat");
                is3D = true;


            }


        }
    }

    
}
