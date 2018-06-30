namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/アクター/マテリアル変更", "マテリアルプロパティーを変更", 0x555555, 0x444488)]
    public class EventAction_SetActorMaterial : EventAction
    {
        [StringIsActorList]
        public string ActorID;
        [HideInInspector]
        public bool allMaterials;
        [HideInInspector]
        public string materialName;
        [HideInInspector]
        public bool changeTexture;
        [HideInInspector]
        public Texture2D texture;
        [HideInInspector]
        public ColorModes mode;
        [HideInInspector]
        public Color blendColor;

        public EventAction_SetActorMaterial()
        {
            this.allMaterials = 1;
            this.blendColor = new Color(0f, 0f, 0f, 0f);
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            TacticsUnitController controller;
            Renderer[] rendererArray;
            int num;
            Material material;
            Color color;
            ColorModes modes;
            controller = TacticsUnitController.FindByUniqueName(this.ActorID);
            if ((controller != null) == null)
            {
                goto Label_0158;
            }
            rendererArray = controller.get_gameObject().GetComponentsInChildren<Renderer>(1);
            num = ((int) rendererArray.Length) - 1;
            goto Label_0151;
        Label_0030:
            material = rendererArray[num].get_material();
            if (string.IsNullOrEmpty(material.GetTag("Character", 0)) == null)
            {
                goto Label_0065;
            }
            if (string.IsNullOrEmpty(material.GetTag("CharacterSimple", 0)) != null)
            {
                goto Label_014D;
            }
        Label_0065:
            if (this.allMaterials != null)
            {
                goto Label_0090;
            }
            if ((material.get_name() == (this.materialName + " (Instance)")) == null)
            {
                goto Label_014D;
            }
        Label_0090:
            if (this.changeTexture == null)
            {
                goto Label_00AC;
            }
            material.SetTexture("_MainTex", this.texture);
        Label_00AC:
            material.EnableKeyword("MONOCHROME_OFF");
            material.DisableKeyword("MONOCHROME_ON");
            material.EnableKeyword("COLORBLEND_OFF");
            material.DisableKeyword("COLORBLEND_ON");
            switch (this.mode)
            {
                case 0:
                    goto Label_00F8;

                case 1:
                    goto Label_00FD;

                case 2:
                    goto Label_0118;
            }
            goto Label_0148;
        Label_00F8:
            goto Label_014D;
        Label_00FD:
            material.EnableKeyword("MONOCHROME_ON");
            material.DisableKeyword("MONOCHROME_OFF");
            goto Label_014D;
        Label_0118:
            color = this.blendColor;
            material.EnableKeyword("COLORBLEND_ON");
            material.DisableKeyword("COLORBLEND_OFF");
            material.SetColor("_blendColor", color);
            goto Label_014D;
        Label_0148:;
        Label_014D:
            num -= 1;
        Label_0151:
            if (num >= 0)
            {
                goto Label_0030;
            }
        Label_0158:
            base.ActivateNext();
            return;
        }

        public enum ColorModes
        {
            None,
            Monochrome,
            Blend
        }
    }
}

