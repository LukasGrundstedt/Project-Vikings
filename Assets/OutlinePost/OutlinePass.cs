using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OutlinePass : ScriptableRenderPass
{
    private Material material;
    private int outlineId = Shader.PropertyToID("_Temp");
    private RenderTargetIdentifier src, dst;

    public OutlinePass()
    {
        if (!material)
        {
            material = CoreUtils.CreateEngineMaterial("Custom/ScreenTint");
        }

        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
        src = renderingData.cameraData.renderer.cameraColorTargetHandle;
        cmd.GetTemporaryRT(outlineId, descriptor, FilterMode.Bilinear);
        dst = new RenderTargetIdentifier();
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer commandBuffer = CommandBufferPool.Get("OutlineRendererFeature");
        VolumeStack volumes = VolumeManager.instance.stack;
        PostOutlineData outlineData = volumes.GetComponent<PostOutlineData>();

        if (outlineData.IsActive())
        {
            material.SetColor("_OverlayColor", (Color)outlineData.OverlayColor);
            material.SetFloat("_Intensity", (float)outlineData.Intensity);
            //material.SetFloat("_MinDepth", 0f);
            //material.SetFloat("_MaxDepth", 1f);

            Blit(commandBuffer, src, dst, material, 0);
            Blit(commandBuffer, dst, src);
        }

        context.ExecuteCommandBuffer(commandBuffer);
        CommandBufferPool.Release(commandBuffer);
    }

    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        cmd.ReleaseTemporaryRT(outlineId);
    }
}