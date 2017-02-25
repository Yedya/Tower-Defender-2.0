using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class G_BlackAndWhiteCameraScript : MonoBehaviour
{
    public float intensity;
    private Material _material;

    //Creates a private meterial used to the effect.
    void Awake()
    {
        _material = new Material(Shader.Find("Hidden/G_BlackAndWhiteCameraShader"));
    }

    //Postprocess the image.
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(intensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        _material.SetFloat("_bwBlend", intensity);
        Graphics.Blit(source, destination, _material);
    }
}
