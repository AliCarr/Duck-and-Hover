using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    [SerializeField]
    private int m_width;

    [SerializeField]
    private int m_height;

    [SerializeField]
    private float m_scale;

    [SerializeField]
    private Renderer m_renderer;

    [ContextMenu("Generate Noise")]


    public void GenerateNoise()
    {
        m_renderer = GetComponent<Renderer>();

        float[,] m_noiseMap = Noise.GenerateNoiseMap(m_width, m_height, m_scale);

        Texture2D tex = new Texture2D(m_width, m_height);

        Color[] colorMap = new Color[m_width * m_height];

        for (int y = 0; y < m_height; y++)
        {
            for (int x = 0; x < m_width; x++)
            {
                colorMap[y * m_width + x] = Color.Lerp(Color.black, Color.white, m_noiseMap[x, y]);
            }
        }

        tex.SetPixels(colorMap);
        tex.Apply();

        m_renderer.sharedMaterial.mainTexture = tex;
        m_renderer.transform.localScale = new Vector3(m_width, 1, m_height);
    }  
}
