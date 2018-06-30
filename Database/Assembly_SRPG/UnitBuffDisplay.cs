namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class UnitBuffDisplay : MonoBehaviour
    {
        private static float DISP_TIME;
        public NodeData[] m_NodeData;
        public GameObject m_Root;
        public UnitBuffDisplayNode m_NodeRoot;
        private Unit m_Owner;
        private List<Object> m_Objects;
        private List<Object> m_DispObjects;
        private float m_Time;
        private Object m_CurrentObject;

        static UnitBuffDisplay()
        {
            DISP_TIME = 3f;
            return;
        }

        public UnitBuffDisplay()
        {
            this.m_Objects = new List<Object>();
            this.m_DispObjects = new List<Object>();
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.m_NodeRoot != null) == null)
            {
                goto Label_0022;
            }
            this.m_NodeRoot.get_gameObject().SetActive(0);
        Label_0022:
            return;
        }

        private void CreateNode(Object obj)
        {
            GameObject obj2;
            if (this.m_CurrentObject == null)
            {
                goto Label_0017;
            }
            this.RemoveNode(this.m_CurrentObject);
        Label_0017:
            obj2 = Object.Instantiate<GameObject>(this.m_NodeRoot.get_gameObject());
            if ((obj2 != null) == null)
            {
                goto Label_0086;
            }
            obj.node = obj2.GetComponent<UnitBuffDisplayNode>();
            if ((obj.node != null) == null)
            {
                goto Label_007F;
            }
            obj.node.Setup(obj.paramlist[0]);
            obj2.get_transform().SetParent(this.m_Root.get_transform(), 0);
        Label_007F:
            obj2.SetActive(1);
        Label_0086:
            this.m_Time = 0f;
            this.m_CurrentObject = obj;
            return;
        }

        public NodeData GetNodeData(UnitBuffDisplayNode.BuffType buffType)
        {
            int num;
            num = 0;
            goto Label_0034;
        Label_0007:
            if (this.m_NodeData[num] == null)
            {
                goto Label_0030;
            }
            if (this.m_NodeData[num].buff != buffType)
            {
                goto Label_0030;
            }
            return this.m_NodeData[num];
        Label_0030:
            num += 1;
        Label_0034:
            if (num < ((int) this.m_NodeData.Length))
            {
                goto Label_0007;
            }
            return null;
        }

        private Object GetObject(BuffAttachment buff)
        {
            int num;
            <GetObject>c__AnonStorey3B7 storeyb;
            storeyb = new <GetObject>c__AnonStorey3B7();
            storeyb.buff = buff;
            num = 0;
            goto Label_004D;
        Label_0014:
            if (this.m_Objects[num].paramlist.FindIndex(new Predicate<UnitBuffDisplayNode.Param>(storeyb.<>m__439)) == -1)
            {
                goto Label_0049;
            }
            return this.m_Objects[num];
        Label_0049:
            num += 1;
        Label_004D:
            if (num < this.m_Objects.Count)
            {
                goto Label_0014;
            }
            return null;
        }

        private void Initiallize(Unit owner)
        {
            this.m_Owner = owner;
            return;
        }

        private void OnDestroy()
        {
            this.Release();
            return;
        }

        private void OnDisable()
        {
        }

        private void OnEnable()
        {
            this.UpdateBuff();
            return;
        }

        private void Release()
        {
            int num;
            num = 0;
            goto Label_001D;
        Label_0007:
            this.RemoveNode(this.m_Objects[num]);
            num += 1;
        Label_001D:
            if (num < this.m_Objects.Count)
            {
                goto Label_0007;
            }
            this.m_Objects.Clear();
            this.m_DispObjects.Clear();
            this.m_CurrentObject = null;
            this.m_Owner = null;
            return;
        }

        private void RemoveNode(Object obj)
        {
            if ((obj.node != null) == null)
            {
                goto Label_0028;
            }
            Object.Destroy(obj.node.get_gameObject());
            obj.node = null;
        Label_0028:
            if (this.m_CurrentObject != obj)
            {
                goto Label_003B;
            }
            this.m_CurrentObject = null;
        Label_003B:
            return;
        }

        private unsafe void RequestBuff()
        {
            int num;
            BuffAttachment attachment;
            UnitBuffDisplayNode.Param[] paramArray;
            int num2;
            UnitBuffDisplayNode.Param param;
            Object obj2;
            int num3;
            if (this.m_Owner != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_00F1;
        Label_0013:
            attachment = this.m_Owner.BuffAttachments[num];
            if (attachment.IsPassive != null)
            {
                goto Label_00ED;
            }
            if (this.GetObject(attachment) == null)
            {
                goto Label_0046;
            }
            goto Label_00ED;
        Label_0046:
            paramArray = UnitBuffDisplayNode.CreateParams(this, this.m_Owner, attachment);
            num2 = 0;
            goto Label_00E4;
        Label_005B:
            param = *(&(paramArray[num2]));
            obj2 = null;
            num3 = 0;
            goto Label_00A7;
        Label_0074:
            if (this.m_Objects[num3].IsEqual(param) == null)
            {
                goto Label_00A1;
            }
            obj2 = this.m_Objects[num3];
            goto Label_00B9;
        Label_00A1:
            num3 += 1;
        Label_00A7:
            if (num3 < this.m_Objects.Count)
            {
                goto Label_0074;
            }
        Label_00B9:
            if (obj2 != null)
            {
                goto Label_00D7;
            }
            this.m_Objects.Add(new Object(param));
            goto Label_00E0;
        Label_00D7:
            obj2.Add(param);
        Label_00E0:
            num2 += 1;
        Label_00E4:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_005B;
            }
        Label_00ED:
            num += 1;
        Label_00F1:
            if (num < this.m_Owner.BuffAttachments.Count)
            {
                goto Label_0013;
            }
            return;
        }

        [DebuggerHidden]
        private IEnumerator Start()
        {
            <Start>c__Iterator149 iterator;
            iterator = new <Start>c__Iterator149();
            iterator.<>f__this = this;
            return iterator;
        }

        private void Update()
        {
            if (this.m_Owner != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.RequestBuff();
            this.UpdateBuff();
            this.UpdateNodeDisp();
            return;
        }

        private unsafe void UpdateBuff()
        {
            int num;
            Object obj2;
            int num2;
            bool flag;
            int num3;
            UnitBuffDisplayNode.Param param;
            UnitBuffDisplayNode.Param param2;
            this.m_DispObjects.Clear();
            num = 0;
            goto Label_00F2;
        Label_0012:
            obj2 = this.m_Objects[num];
            num2 = 0;
            goto Label_005A;
        Label_0026:
            param = obj2.paramlist[num2];
            if (&param.isAlive != null)
            {
                goto Label_0056;
            }
            obj2.Remove(obj2.paramlist[num2]);
            num2 -= 1;
        Label_0056:
            num2 += 1;
        Label_005A:
            if (num2 < obj2.paramlist.Count)
            {
                goto Label_0026;
            }
            if (obj2.paramlist.Count != null)
            {
                goto Label_0098;
            }
            this.RemoveNode(obj2);
            this.m_Objects.Remove(obj2);
            num -= 1;
            goto Label_00EE;
        Label_0098:
            flag = 0;
            num3 = 0;
            goto Label_00CA;
        Label_00A2:
            param2 = obj2.paramlist[num3];
            if (&param2.isNeedDispOn == null)
            {
                goto Label_00C4;
            }
            flag = 1;
            goto Label_00DC;
        Label_00C4:
            num3 += 1;
        Label_00CA:
            if (num3 < obj2.paramlist.Count)
            {
                goto Label_00A2;
            }
        Label_00DC:
            if (flag == null)
            {
                goto Label_00EE;
            }
            this.m_DispObjects.Add(obj2);
        Label_00EE:
            num += 1;
        Label_00F2:
            if (num < this.m_Objects.Count)
            {
                goto Label_0012;
            }
            return;
        }

        private void UpdateNodeDisp()
        {
            int num;
            int num2;
            if (this.m_DispObjects.Count != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            this.m_Time += Time.get_deltaTime();
            if (this.m_CurrentObject == null)
            {
                goto Label_003E;
            }
            if (this.m_Time < DISP_TIME)
            {
                goto Label_00C4;
            }
        Label_003E:
            num = 0;
            if (this.m_CurrentObject == null)
            {
                goto Label_00B2;
            }
            num2 = 0;
            goto Label_0076;
        Label_0052:
            if (this.m_CurrentObject != this.m_DispObjects[num2])
            {
                goto Label_0072;
            }
            num = num2 + 1;
            goto Label_0087;
        Label_0072:
            num2 += 1;
        Label_0076:
            if (num2 < this.m_DispObjects.Count)
            {
                goto Label_0052;
            }
        Label_0087:
            if (num < this.m_DispObjects.Count)
            {
                goto Label_009A;
            }
            num = 0;
        Label_009A:
            if (this.m_DispObjects[num] != this.m_CurrentObject)
            {
                goto Label_00B2;
            }
            return;
        Label_00B2:
            this.CreateNode(this.m_DispObjects[num]);
        Label_00C4:
            return;
        }

        public Object[] objects
        {
            get
            {
                return this.m_Objects.ToArray();
            }
        }

        [CompilerGenerated]
        private sealed class <GetObject>c__AnonStorey3B7
        {
            internal BuffAttachment buff;

            public <GetObject>c__AnonStorey3B7()
            {
                base..ctor();
                return;
            }

            internal unsafe bool <>m__439(UnitBuffDisplayNode.Param prop)
            {
                return (&prop.buff == this.buff);
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__Iterator149 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal UnitGauge <gauge>__0;
            internal int $PC;
            internal object $current;
            internal UnitBuffDisplay <>f__this;

            public <Start>c__Iterator149()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_005B;
                }
                goto Label_0088;
            Label_0021:
                this.<gauge>__0 = this.<>f__this.GetComponentInParent<UnitGauge>();
                if ((this.<gauge>__0 != null) == null)
                {
                    goto Label_0081;
                }
                goto Label_005B;
            Label_0048:
                this.$current = null;
                this.$PC = 1;
                goto Label_008A;
            Label_005B:
                if (this.<gauge>__0.GetOwner() == null)
                {
                    goto Label_0048;
                }
                this.<>f__this.Initiallize(this.<gauge>__0.GetOwner());
            Label_0081:
                this.$PC = -1;
            Label_0088:
                return 0;
            Label_008A:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [Serializable]
        public class NodeData
        {
            public UnitBuffDisplayNode.BuffType buff;
            public UnitBuffDisplayNode.DispType disp;
            public Sprite sprite;

            public NodeData()
            {
                base..ctor();
                return;
            }
        }

        public class Object
        {
            public List<UnitBuffDisplayNode.Param> paramlist;
            public UnitBuffDisplayNode.DispType dispType;
            public UnitBuffDisplayNode.EffectType effectType;
            public UnitBuffDisplayNode node;

            public unsafe Object(UnitBuffDisplayNode.Param param)
            {
                this.paramlist = new List<UnitBuffDisplayNode.Param>();
                base..ctor();
                this.paramlist.Add(param);
                this.dispType = &param.dispType;
                this.effectType = &param.effectType;
                this.node = null;
                return;
            }

            public void Add(UnitBuffDisplayNode.Param param)
            {
                this.paramlist.Add(param);
                return;
            }

            public unsafe bool IsEqual(UnitBuffDisplayNode.Param param)
            {
                return ((this.dispType != &param.dispType) ? 0 : (this.effectType == &param.effectType));
            }

            public void Remove(UnitBuffDisplayNode.Param param)
            {
                this.paramlist.Remove(param);
                return;
            }
        }
    }
}

