namespace SRPG
{
    using System;
    using UnityEngine;

    [ExecuteInEditMode, RequireComponent(typeof(Camera))]
    public class CharacterRenderEffect : MonoBehaviour
    {
        public Material RenderMaterial;

        public CharacterRenderEffect()
        {
            base..ctor();
            return;
        }

        private void OnPostRender()
        {
            Shader.EnableKeyword("ALPHA_EMISSIVE");
            Shader.DisableKeyword("ALPHA_DEPTH");
            return;
        }

        private void OnPreRender()
        {
            Shader.DisableKeyword("ALPHA_EMISSIVE");
            Shader.EnableKeyword("ALPHA_DEPTH");
            return;
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if ((this.RenderMaterial != null) == null)
            {
                goto Label_001E;
            }
            Graphics.Blit(src, dest, this.RenderMaterial);
        Label_001E:
            return;
        }
    }
}

