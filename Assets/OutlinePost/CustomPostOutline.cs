using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom/CustomPostOutline", typeof(UniversalRenderPipeline))]
public class CustomPostOutline : VolumeComponent, IPostProcessComponent
{
    public FloatParameter OutlineThickness = new FloatParameter(1);
    public ColorParameter OutlineColor = new ColorParameter(Color.white);

    public bool IsActive()
    {
        return true;
    }

    public bool IsTileCompatible()
    {
        return true;
    }
}