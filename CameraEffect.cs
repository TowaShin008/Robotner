using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 [ExecuteAlways]
public class CameraEffect : MonoBehaviour
{
   
    [SerializeField] private Material m_Material;

    // Start is called before the first frame update
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, m_Material);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
