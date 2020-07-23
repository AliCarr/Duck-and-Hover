using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (NoiseGenerator))]
public class NoiseDebug : Editor
{
    public override void OnInspectorGUI()
    {
        NoiseGenerator m_noiseGen = (NoiseGenerator)target;

        if(DrawDefaultInspector())
        {
            m_noiseGen.GenerateNoise();
        }

        //base.OnInspectorGUI();
    }
}
