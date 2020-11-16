using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcess : MonoBehaviour
{
    public Material postProcessMaterial;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //Shader.SetGlobalVector("_Position", transform.position);
        Graphics.Blit(source, destination, postProcessMaterial);
    }
}
