using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
[AddComponentMenu("Custom/Image Effects/Recolor")]
public class Recolor : UnityStandardAssets.ImageEffects.ImageEffectBase
{
    public float redMultiplier;
    public float greenMultiplier;
    public float blueMultiplier;

    public float redInversed;
    public float greenInversed;
    public float blueInversed;

    // Called by camera to apply image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_RedMultiplier", redMultiplier);
        material.SetFloat("_GreenMultiplier", greenMultiplier);
        material.SetFloat("_BlueMultiplier", blueMultiplier);

        material.SetFloat("_RedInversed", redInversed);
        material.SetFloat("_GreenInversed", greenInversed);
        material.SetFloat("_BlueInversed", blueInversed);

        Graphics.Blit(source, destination, material);
    }
}