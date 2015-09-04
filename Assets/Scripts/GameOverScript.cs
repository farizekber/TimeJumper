using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector3 inputLocation = Vector3.zero;

            if (Input.GetMouseButtonDown(0))
            {
                inputLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // Get movement of the finger since last frame
                inputLocation = Input.GetTouch(0).deltaPosition;
            }

            if (GetComponentInChildren<BoxCollider2D>().bounds.Intersects(new Bounds(inputLocation, new Vector3(20, 20, 100))))
            {
                Application.LoadLevel("Scene1");
            }
        }
    }
}
