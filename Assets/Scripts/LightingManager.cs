using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    public Light directionalLight;
    public LightingPreset preset;
    [Range(0, 24)]
    public float timeOfDay;
    
    void Update()
    {
        //timeOfDay += Time.deltaTime;
        //timeOfDay %= 24;
        UpdateLighting(timeOfDay / 24f);
    }

    void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        directionalLight.color = preset.directionalColor.Evaluate(timePercent);
        directionalLight.transform.localRotation = 
            Quaternion.Euler(new Vector3(timePercent * 360f - 90f, 170f, 0));
    }
}
