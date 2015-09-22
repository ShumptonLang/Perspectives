using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private Transform enemyTrans;
    public Vector3 startPos;
    public Vector3  endPos;
    private bool isMovingToEnd = true;
	// Use this for initialization
	void Start () {
        enemyTrans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (enemyTrans.localPosition.x >= endPos.x)
            isMovingToEnd = false;
        else if (enemyTrans.localPosition.x <= startPos.x)
            isMovingToEnd = true;

        if (isMovingToEnd)
        {
            transform.Rotate(0, 11, 0);
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        if (!isMovingToEnd)
        {
            transform.Rotate(0, -1, 0);
            transform.Translate(-Vector3.right * Time.deltaTime);

        }
    }
}
