﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise 
{
    public static float[,] GenerateNoiseMap(int width, int height, float scale)
    {
        
        float[,] m_noiseMap = new float[width, height];

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                m_noiseMap[x, y] = perlinValue;
            }
        }

        return m_noiseMap;
    }
}
