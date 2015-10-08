using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RenderTextureManager : MonoBehaviour {
    RenderTexture tex;
    Camera cam;
    public Material mat;
    public List<Sprite> originalBackgroundImages = new List<Sprite>();
    public List<Sprite> enhancedBackgroundImages = new List<Sprite>();
    public List<Sprite> backupBackgroundImages = new List<Sprite>();
    public static RenderTextureManager instance;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }

    // Use this for initialization
    void Start ()
    {
        if (instance == null)
        {
            instance = this;
            if (PlayerPrefs.GetInt("UseShader") == 1)
                StartCoroutine(CreateAllRenderTextures());
            else
            {
                GameObject bg = GameObject.Find("background-1");
                if (bg != null)
                {
                    bg.SetActive(false);
                }
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            GameObject bg = GameObject.Find("background-1");
            if (bg != null)
            {
                bg.SetActive(false);
            }
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator CreateAllRenderTextures()
    {
        yield return new WaitForEndOfFrame();

        foreach (Sprite item in originalBackgroundImages)
        {
            CreateRenderTexture(item);
        }

        GameObject bg = GameObject.Find("background-1");
        if (bg != null)
        {
            bg.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

    void CreateRenderTexture(Sprite sprite)
    {
        GameObject.Find("background-1").GetComponent<SpriteRenderer>().sprite = sprite;

        int width = (int)GameObject.Find("background-1").GetComponent<Renderer>().bounds.size.x;
        int height = (int)GameObject.Find("background-1").GetComponent<Renderer>().bounds.size.y;
        Vector3 nsize = Camera.main.WorldToScreenPoint(new Vector3(width, height, 0));
        nsize.x *= 2.0f;
        //nsize.x *= 1.33f;
        nsize.y *= 2.0f;
        //nsize.y *= 1.33f;

        tex = new RenderTexture((int)nsize.x, (int)nsize.y, 1);
        tex.useMipMap = false;
        tex.filterMode = FilterMode.Point;
        tex.antiAliasing = 1;
        tex.Create();

        cam = GetComponent<Camera>();
        float previousOrthoSize = cam.orthographicSize;
        float previousAspect = cam.aspect;
        RenderTexture previousRenderTexture = cam.targetTexture;

        Texture2D image = new Texture2D((int)nsize.x - 50, (int)nsize.y);
        cam.targetTexture = tex;
        float scale = 1.0f / (cam.orthographicSize / GameObject.Find("background-1").GetComponent<Renderer>().bounds.size.y / 2.0f);
        cam.orthographicSize = scale;
        cam.aspect = GameObject.Find("background-1").GetComponent<Renderer>().bounds.size.x / GameObject.Find("background-1").GetComponent<Renderer>().bounds.size.y;
        
        RenderTexture past = RenderTexture.active;
        RenderTexture.active = tex;

        cam.Render();

        image.ReadPixels(new Rect(25, 0, (int)nsize.x - 50, (int)nsize.y), 0, 0);
        image.Apply();

        cam.targetTexture = previousRenderTexture;
        cam.orthographicSize = previousOrthoSize;
        cam.aspect = previousAspect;
        RenderTexture.active = past;
        tex.Release();

        enhancedBackgroundImages.Add(Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f)));
    }

    // Update is called once per frame
    void Update () {
        //if(!finishedGenerating)
        //{
        //    return;
        //}
        
        //GameObject.Find("TEST").GetComponent<SpriteRenderer>().sprite = enhancedBackgroundImages[10];
    }
}
