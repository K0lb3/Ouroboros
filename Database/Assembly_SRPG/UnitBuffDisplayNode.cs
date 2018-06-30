namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitBuffDisplayNode : MonoBehaviour
    {
        public GameObject Icon;
        public GameObject Arrows;
        private RectTransform m_RectTransform;
        private DispType m_DispType;
        private EffectType m_EffectType;

        public UnitBuffDisplayNode()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
        }

        public static Param[] CreateParams(UnitBuffDisplay parent, Unit owner, BuffAttachment buff)
        {
            List<Param> list;
            int num;
            BuffType type;
            UnitBuffDisplay.NodeData data;
            list = new List<Param>();
            num = 1;
            goto Label_0041;
        Label_000D:
            type = num;
            if (((float) GetValue(type, buff)) == 0f)
            {
                goto Label_003D;
            }
            data = parent.GetNodeData(type);
            if (data == null)
            {
                goto Label_003D;
            }
            list.Add(new Param(owner, buff, data));
        Label_003D:
            num += 1;
        Label_0041:
            if (num < 5)
            {
                goto Label_000D;
            }
            if (list.Count != null)
            {
                goto Label_0061;
            }
            list.Add(new Param(owner, buff, null));
        Label_0061:
            return list.ToArray();
        }

        public DispType GetDispType()
        {
            return this.m_DispType;
        }

        public EffectType GetEffectType()
        {
            return this.m_EffectType;
        }

        public static int GetValue(BuffType buffType, BuffAttachment buff)
        {
            BuffType type;
            type = buffType;
            switch ((type - 1))
            {
                case 0:
                    goto Label_001F;

                case 1:
                    goto Label_0035;

                case 2:
                    goto Label_004B;

                case 3:
                    goto Label_0061;
            }
            goto Label_0077;
        Label_001F:
            return buff.status.param.atk;
        Label_0035:
            return buff.status.param.def;
        Label_004B:
            return buff.status.param.mag;
        Label_0061:
            return buff.status.param.mnd;
        Label_0077:
            return 0;
        }

        public static unsafe bool NeedDispOn(Param param)
        {
            if (&param.dispType != null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            return 1;
        }

        private void OnDestroy()
        {
            this.Release();
            return;
        }

        private void Release()
        {
        }

        public void SetPos(float x, float y)
        {
            this.rectTransform.set_anchoredPosition(new Vector2(x, y));
            return;
        }

        public unsafe void Setup(Param param)
        {
            Image image;
            string str;
            Transform transform;
            int num;
            int num2;
            Transform transform2;
            this.m_DispType = &param.dispType;
            this.m_EffectType = &param.effectType;
            if ((this.Icon != null) == null)
            {
                goto Label_0089;
            }
            if (this.m_DispType == null)
            {
                goto Label_0089;
            }
            if (&param.data == null)
            {
                goto Label_007D;
            }
            image = this.Icon.GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_0089;
            }
            image.set_sprite(&param.data.sprite);
            this.Icon.SetActive(1);
            goto Label_0089;
        Label_007D:
            this.Icon.SetActive(0);
        Label_0089:
            if ((this.Arrows != null) == null)
            {
                goto Label_0126;
            }
            if (this.m_EffectType == null)
            {
                goto Label_0126;
            }
            str = "arrow_" + ((EffectType) this.m_EffectType).ToString().ToLower();
            transform = this.Arrows.get_transform();
            num = 0;
            num2 = transform.get_childCount();
            goto Label_011E;
        Label_00E0:
            transform2 = transform.GetChild(num);
            if ((transform2.get_name() == str) == null)
            {
                goto Label_010D;
            }
            transform2.get_gameObject().SetActive(1);
            goto Label_011A;
        Label_010D:
            transform2.get_gameObject().SetActive(0);
        Label_011A:
            num += 1;
        Label_011E:
            if (num < num2)
            {
                goto Label_00E0;
            }
        Label_0126:
            return;
        }

        private void Update()
        {
        }

        public RectTransform rectTransform
        {
            get
            {
                if ((this.m_RectTransform == null) == null)
                {
                    goto Label_001D;
                }
                this.m_RectTransform = base.GetComponent<RectTransform>();
            Label_001D:
                return this.m_RectTransform;
            }
        }

        public enum BuffType
        {
            NONE,
            ATK,
            DEF,
            MAG,
            MND,
            MAX
        }

        public enum DispType
        {
            NONE,
            ATK,
            DEF,
            MAG,
            MND
        }

        public enum EffectType
        {
            NONE,
            UP,
            DOWN
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Param
        {
            public Unit owner;
            public BuffAttachment buff;
            public UnitBuffDisplay.NodeData data;
            public Param(Unit _owner, BuffAttachment _buff, UnitBuffDisplay.NodeData _data)
            {
                this.owner = _owner;
                this.buff = _buff;
                this.data = _data;
                return;
            }

            public bool isAlive
            {
                get
                {
                    int num;
                    int num2;
                    if (this.owner == null)
                    {
                        goto Label_004C;
                    }
                    num = 0;
                    num2 = this.owner.BuffAttachments.Count;
                    goto Label_0045;
                Label_0023:
                    if (this.buff != this.owner.BuffAttachments[num])
                    {
                        goto Label_0041;
                    }
                    return 1;
                Label_0041:
                    num += 1;
                Label_0045:
                    if (num < num2)
                    {
                        goto Label_0023;
                    }
                Label_004C:
                    return 0;
                }
            }
            public bool isNeedDispOn
            {
                get
                {
                    return UnitBuffDisplayNode.NeedDispOn(*(this));
                }
            }
            public UnitBuffDisplayNode.BuffType buffType
            {
                get
                {
                    if (this.data == null)
                    {
                        goto Label_0017;
                    }
                    return this.data.buff;
                Label_0017:
                    return 0;
                }
            }
            public UnitBuffDisplayNode.DispType dispType
            {
                get
                {
                    if (this.data == null)
                    {
                        goto Label_0017;
                    }
                    return this.data.disp;
                Label_0017:
                    return 0;
                }
            }
            public UnitBuffDisplayNode.EffectType effectType
            {
                get
                {
                    if (this.value <= 0)
                    {
                        goto Label_000E;
                    }
                    return 1;
                Label_000E:
                    if (this.value >= 0)
                    {
                        goto Label_001C;
                    }
                    return 2;
                Label_001C:
                    return 0;
                }
            }
            public int value
            {
                get
                {
                    if (this.data == null)
                    {
                        goto Label_0022;
                    }
                    return UnitBuffDisplayNode.GetValue(this.data.buff, this.buff);
                Label_0022:
                    return 0;
                }
            }
        }
    }
}

