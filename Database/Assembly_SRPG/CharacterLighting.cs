namespace SRPG
{
    using System;
    using UnityEngine;

    public class CharacterLighting : MonoBehaviour
    {
        public CharacterLighting()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            this.Update();
            return;
        }

        private unsafe void Update()
        {
            Vector3 vector;
            StaticLightVolume volume;
            Color color;
            Color color2;
            GameSettings settings;
            MeshRenderer[] rendererArray;
            int num;
            SkinnedMeshRenderer[] rendererArray2;
            int num2;
            vector = base.get_transform().get_position();
            volume = StaticLightVolume.FindVolume(vector);
            if ((volume == null) == null)
            {
                goto Label_003B;
            }
            settings = GameSettings.Instance;
            color = settings.Character_DefaultDirectLitColor;
            color2 = settings.Character_DefaultIndirectLitColor;
            goto Label_0046;
        Label_003B:
            volume.CalcLightColor(vector, &color, &color2);
        Label_0046:
            rendererArray = base.GetComponentsInChildren<MeshRenderer>();
            num = 0;
            goto Label_0086;
        Label_0056:
            rendererArray[num].get_material().SetColor("_directLitColor", color);
            rendererArray[num].get_material().SetColor("_indirectLitColor", color2);
            num += 1;
        Label_0086:
            if (num < ((int) rendererArray.Length))
            {
                goto Label_0056;
            }
            rendererArray2 = base.GetComponentsInChildren<SkinnedMeshRenderer>();
            num2 = 0;
            goto Label_00D1;
        Label_00A1:
            rendererArray2[num2].get_material().SetColor("_directLitColor", color);
            rendererArray2[num2].get_material().SetColor("_indirectLitColor", color2);
            num2 += 1;
        Label_00D1:
            if (num2 < ((int) rendererArray2.Length))
            {
                goto Label_00A1;
            }
            return;
        }
    }
}

