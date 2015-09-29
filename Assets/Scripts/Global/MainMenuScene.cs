using UnityEngine;
using System.Collections;

public class MainMenuScene : MonoBehaviour
{
    private static bool acceptsInput = true;
    private BoxCollider2D boxCollider;

    // Use this for initialization
    void Start()
    {
        acceptsInput = true;
        Application.targetFrameRate = -1;
        boxCollider = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!acceptsInput)// && global.ApplicationGlobal.isLoggedIn)
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

            if (boxCollider.bounds.Intersects(new Bounds(inputLocation, new Vector3(20, 20, 100))))
            {
                Application.LoadLevel("Loading");
            }
        }
    }
}
