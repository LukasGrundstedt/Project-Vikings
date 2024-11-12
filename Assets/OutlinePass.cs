using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OutlinePass : ScriptableRenderPass
{
    private Material material;
    private int outlineId = Shader.PropertyToID("_Temp");
    private RenderTargetIdentifier src, outline;

    public OutlinePass()
    {
        if (!material)
        {
            material = CoreUtils.CreateEngineMaterial("Custom/Outline");
        }

        renderPassEvent =  RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
        src = renderingData.cameraData.renderer.cameraColorTargetHandle;
        cmd.GetTemporaryRT(outlineId, descriptor, FilterMode.Bilinear);
        outline = new RenderTargetIdentifier();
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer commandBuffer = CommandBufferPool.Get("OutlineRendererFeature");
        VolumeStack volumes = VolumeManager.instance.stack;
        CustomPostOutline outlineData = volumes.GetComponent<CustomPostOutline>();

        if (outlineData.IsActive())
        {
            material.SetColor("_EdgeColor", (Color)outlineData.OutlineColor);
            material.SetFloat("_Thickness", (float)outlineData.OutlineThickness);
            material.SetFloat("_MinDepth", 0f);
            material.SetFloat("_MaxDepth", 1f);

            Blit(commandBuffer, src, outline, material, 0);
            Blit(commandBuffer, outline, src);
        }

        context.ExecuteCommandBuffer(commandBuffer);
        CommandBufferPool.Release(commandBuffer);
    }

    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        cmd.ReleaseTemporaryRT(outlineId);
    }
}