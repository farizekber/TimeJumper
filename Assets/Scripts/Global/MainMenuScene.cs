using UnityEngine;
using System.Collections;

public class MainMenuScene : MonoBehaviour
{
    private static bool acceptsInput = true;

    // Use this for initialization
    void Start()
    {
        acceptsInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        if (!acceptsInput)
            return;

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

                Application.LoadLevel("Loading");
            }
        }
    }
}
