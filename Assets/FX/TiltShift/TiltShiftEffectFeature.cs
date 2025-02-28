using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

public class TiltShiftEffectFeature : ScriptableRendererFeature
{
    class RenderGraphEffectPass : ScriptableRenderPass
    {
        private Material effectMaterial;

        public RenderGraphEffectPass(Material material)
        {
            effectMaterial = material;
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
            TextureHandle colorTarget = resourceData.activeColorTexture;

            using (var builder = renderGraph.AddRasterRenderPass<PassData>("Tilt Shift Fullscreen Effect", out var passData))
            {
                passData.material = effectMaterial;
                passData.colorTarget = colorTarget;

                builder.SetRenderAttachment(colorTarget, 0);

                builder.SetRenderFunc((PassData data, RasterGraphContext ctx) =>
                {
                    RasterCommandBuffer cmd = ctx.cmd;
                    data.material.SetPass(0);

                    // Draw fullscreen triangle
                    cmd.DrawProcedural(Matrix4x4.identity, data.material, 0, MeshTopology.Triangles, 3);
                    //Debug.Log("Drawing Fullscreen Shader Graph Effect");
                });
            }
        }

        private class PassData
        {
            public Material material;
            public TextureHandle colorTarget;
        }
    }

    RenderGraphEffectPass renderPass;
    public Material effectMaterial;

    public override void Create()
    {
        renderPass = new RenderGraphEffectPass(effectMaterial)
        {
            renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (effectMaterial == null)
        {
            Debug.LogWarning("Effect Material is missing. Pass will not execute.");
            return;
        }

        renderer.EnqueuePass(renderPass);
    }
}
