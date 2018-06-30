namespace SRPG
{
    using GR;
    using System;
    using System.Reflection;
    using UnityEngine;

    [Pin(1, "Done", 1, 1), Pin(0, "Load", 0, 0), NodeType("LoadShopBG", 0x7fe5)]
    public class FlowNode_LoadShopBG : FlowNode
    {
        public PrefabType Type;
        public string TypeString;
        public string BasePath;
        public Transform Parent;
        public bool WorldPositionStays;

        public FlowNode_LoadShopBG()
        {
            this.BasePath = "ShopBG";
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            SectionParam param;
            string str;
            System.Type type;
            FieldInfo info;
            System.Type type2;
            GameObject obj2;
            GameObject obj3;
            if (pinID != null)
            {
                goto Label_0153;
            }
            param = MonoSingleton<GameManager>.Instance.GetCurrentSectionParam();
            str = null;
            if (this.Type != 3)
            {
                goto Label_002B;
            }
            str = this.TypeString;
            goto Label_00C9;
        Label_002B:
            if (param != null)
            {
                goto Label_0036;
            }
            goto Label_00C9;
        Label_0036:
            if (this.Type != null)
            {
                goto Label_004D;
            }
            str = param.bar;
            goto Label_00C9;
        Label_004D:
            if (this.Type != 1)
            {
                goto Label_0065;
            }
            str = param.shop;
            goto Label_00C9;
        Label_0065:
            if (this.Type != 2)
            {
                goto Label_007D;
            }
            str = param.inn;
            goto Label_00C9;
        Label_007D:
            if (this.Type != 4)
            {
                goto Label_00C9;
            }
            info = param.GetType().GetField(this.TypeString);
            if (info == null)
            {
                goto Label_00C9;
            }
            if (info.FieldType != typeof(string))
            {
                goto Label_00C9;
            }
            str = (string) info.GetValue(param);
        Label_00C9:
            if (str == null)
            {
                goto Label_00F1;
            }
            if (string.IsNullOrEmpty(this.BasePath) != null)
            {
                goto Label_00F1;
            }
            str = this.BasePath + "/" + str;
        Label_00F1:
            if (str == null)
            {
                goto Label_014B;
            }
            if ((this.Parent != null) == null)
            {
                goto Label_014B;
            }
            obj2 = AssetManager.Load<GameObject>(str);
            if ((obj2 != null) == null)
            {
                goto Label_014B;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            if ((obj3 != null) == null)
            {
                goto Label_014B;
            }
            obj3.get_transform().SetParent(this.Parent, this.WorldPositionStays);
        Label_014B:
            base.ActivateOutputLinks(1);
        Label_0153:
            return;
        }

        public enum PrefabType
        {
            SectionParamBar,
            SectionParamShop,
            SectionParamInn,
            DirectResourcePath,
            SectionParamMemberName
        }
    }
}

