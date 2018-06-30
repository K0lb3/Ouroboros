namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class TabMaker : MonoBehaviour
    {
        public SerializeValueBehaviour m_TabNode;
        public Sprite m_CornerSpriteOff;
        public Sprite m_CornerSpriteOn;
        public Sprite m_SpriteOff;
        public Sprite m_SpriteOn;
        public Element[] m_Elements;
        private List<Info> m_InfoList;

        public TabMaker()
        {
            this.m_InfoList = new List<Info>();
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.m_TabNode != null) == null)
            {
                goto Label_0022;
            }
            this.m_TabNode.get_gameObject().SetActive(0);
        Label_0022:
            return;
        }

        public unsafe void Create(string[] keys, Action<GameObject, SerializeValueList> callback)
        {
            int num;
            Element element;
            GameObject obj2;
            SerializeValueBehaviour behaviour;
            Image image;
            Vector3 vector;
            Vector3 vector2;
            Text text;
            if ((this.m_TabNode == null) == null)
            {
                goto Label_0031;
            }
            Debug.LogException(new Exception("Failed TabNode Null > " + base.get_gameObject().get_name()));
            return;
        Label_0031:
            this.Destroy();
            num = 0;
            goto Label_0334;
        Label_003E:
            element = this.GetElement(keys[num]);
            if (element != null)
            {
                goto Label_0077;
            }
            Debug.LogError("Tab素材見つかりません > " + ((keys[num] == null) ? "null" : keys[num]));
            goto Label_0330;
        Label_0077:
            obj2 = Object.Instantiate<GameObject>(this.m_TabNode.get_gameObject());
            behaviour = obj2.GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_0330;
            }
            image = behaviour.list.GetUIImage("off");
            if ((image != null) == null)
            {
                goto Label_0140;
            }
            if ((num != null) && (num != (((int) keys.Length) - 1)))
            {
                goto Label_0133;
            }
            image.set_sprite(((this.m_CornerSpriteOff != null) == null) ? this.m_SpriteOff : this.m_CornerSpriteOff);
            if (num != (((int) keys.Length) - 1))
            {
                goto Label_0140;
            }
            vector = image.get_transform().get_localScale();
            &vector.x *= -1f;
            image.get_transform().set_localScale(vector);
            goto Label_0140;
        Label_0133:
            image.set_sprite(this.m_SpriteOff);
        Label_0140:
            image = behaviour.list.GetUIImage("on");
            if ((image != null) == null)
            {
                goto Label_01E5;
            }
            if ((num != null) && (num != (((int) keys.Length) - 1)))
            {
                goto Label_01D8;
            }
            image.set_sprite(((this.m_CornerSpriteOn != null) == null) ? this.m_SpriteOn : this.m_CornerSpriteOn);
            if (num != (((int) keys.Length) - 1))
            {
                goto Label_01E5;
            }
            vector2 = image.get_transform().get_localScale();
            &vector2.x *= -1f;
            image.get_transform().set_localScale(vector2);
            goto Label_01E5;
        Label_01D8:
            image.set_sprite(this.m_SpriteOn);
        Label_01E5:
            image = behaviour.list.GetUIImage("icon");
            if ((image != null) == null)
            {
                goto Label_0241;
            }
            if ((element.icon != null) == null)
            {
                goto Label_0234;
            }
            image.set_sprite(element.icon);
            image.get_gameObject().SetActive(1);
            goto Label_0241;
        Label_0234:
            image.get_gameObject().SetActive(0);
        Label_0241:
            text = behaviour.list.GetUILabel("text");
            if ((text != null) == null)
            {
                goto Label_02C9;
            }
            if (string.IsNullOrEmpty(element.text) != null)
            {
                goto Label_02BC;
            }
            if (element.text.IndexOf("sys.") != -1)
            {
                goto Label_0298;
            }
            text.set_text(element.text);
            goto Label_02AA;
        Label_0298:
            text.set_text(LocalizedText.Get(element.text));
        Label_02AA:
            text.get_gameObject().SetActive(1);
            goto Label_02C9;
        Label_02BC:
            text.get_gameObject().SetActive(0);
        Label_02C9:
            behaviour.list.AddObject("element", element);
            if (callback == null)
            {
                goto Label_02EE;
            }
            callback(obj2, behaviour.list);
        Label_02EE:
            obj2.set_name(element.key);
            obj2.get_transform().SetParent(base.get_gameObject().get_transform(), 0);
            obj2.SetActive(1);
            this.m_InfoList.Add(new Info(obj2, behaviour.list, element));
        Label_0330:
            num += 1;
        Label_0334:
            if (num < ((int) keys.Length))
            {
                goto Label_003E;
            }
            return;
        }

        public void Destroy()
        {
            int num;
            Info info;
            num = 0;
            goto Label_003A;
        Label_0007:
            info = this.m_InfoList[num];
            if (info == null)
            {
                goto Label_0036;
            }
            if ((info.node != null) == null)
            {
                goto Label_0036;
            }
            Object.Destroy(info.node);
        Label_0036:
            num += 1;
        Label_003A:
            if (num < this.m_InfoList.Count)
            {
                goto Label_0007;
            }
            this.m_InfoList.Clear();
            return;
        }

        public Element GetElement(string key)
        {
            int num;
            if (this.m_Elements != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            num = 0;
            goto Label_0046;
        Label_0014:
            if (this.m_Elements[num] == null)
            {
                goto Label_0042;
            }
            if ((this.m_Elements[num].key == key) == null)
            {
                goto Label_0042;
            }
            return this.m_Elements[num];
        Label_0042:
            num += 1;
        Label_0046:
            if (num < ((int) this.m_Elements.Length))
            {
                goto Label_0014;
            }
            return null;
        }

        public Info GetInfo(string key)
        {
            int num;
            Info info;
            num = 0;
            goto Label_0041;
        Label_0007:
            info = this.m_InfoList[num];
            if (info == null)
            {
                goto Label_003D;
            }
            if (info.element == null)
            {
                goto Label_003D;
            }
            if ((info.element.key == key) == null)
            {
                goto Label_003D;
            }
            return info;
        Label_003D:
            num += 1;
        Label_0041:
            if (num < this.m_InfoList.Count)
            {
                goto Label_0007;
            }
            return null;
        }

        public Info[] GetInfos()
        {
            return this.m_InfoList.ToArray();
        }

        public Info GetOnIfno()
        {
            int num;
            Info info;
            num = 0;
            goto Label_0030;
        Label_0007:
            info = this.m_InfoList[num];
            if (info == null)
            {
                goto Label_002C;
            }
            if (info.tgl.get_isOn() == null)
            {
                goto Label_002C;
            }
            return info;
        Label_002C:
            num += 1;
        Label_0030:
            if (num < this.m_InfoList.Count)
            {
                goto Label_0007;
            }
            return null;
        }

        private void OnDestroy()
        {
            this.Destroy();
            return;
        }

        public void SetOn(Enum key, bool value)
        {
            Info info;
            info = this.GetInfo(key.ToString());
            if (info == null)
            {
                goto Label_001A;
            }
            info.isOn = value;
        Label_001A:
            return;
        }

        public void SetOn(string key, bool value)
        {
            Info info;
            info = this.GetInfo(key);
            if (info == null)
            {
                goto Label_0015;
            }
            info.isOn = value;
        Label_0015:
            return;
        }

        private void Start()
        {
        }

        private void Update()
        {
        }

        [Serializable]
        public class Element
        {
            public string key;
            public Sprite icon;
            public string text;
            public int value;

            public Element()
            {
                base..ctor();
                return;
            }
        }

        public class Info
        {
            public GameObject node;
            public SerializeValueList values;
            public TabMaker.Element element;
            public Toggle tgl;
            public ButtonEvent ev;

            public Info(GameObject _node, SerializeValueList _values, TabMaker.Element _element)
            {
                base..ctor();
                this.node = _node;
                this.values = _values;
                this.element = _element;
                this.tgl = this.node.GetComponent<Toggle>();
                this.ev = this.node.GetComponent<ButtonEvent>();
                return;
            }

            public void SetColor(Color color)
            {
                Image image;
                Text text;
                if (this.values == null)
                {
                    goto Label_009B;
                }
                image = this.values.GetUIImage("off");
                if ((image != null) == null)
                {
                    goto Label_002F;
                }
                image.set_color(color);
            Label_002F:
                image = this.values.GetUIImage("on");
                if ((image != null) == null)
                {
                    goto Label_0053;
                }
                image.set_color(color);
            Label_0053:
                image = this.values.GetUIImage("icon");
                if ((image != null) == null)
                {
                    goto Label_0077;
                }
                image.set_color(color);
            Label_0077:
                text = this.values.GetUILabel("text");
                if ((text != null) == null)
                {
                    goto Label_009B;
                }
                text.set_color(color);
            Label_009B:
                return;
            }

            public bool interactable
            {
                get
                {
                    return (((this.tgl != null) == null) ? 0 : this.tgl.get_interactable());
                }
                set
                {
                    if ((this.tgl != null) == null)
                    {
                        goto Label_001D;
                    }
                    this.tgl.set_interactable(value);
                Label_001D:
                    return;
                }
            }

            public bool isOn
            {
                get
                {
                    return (((this.tgl != null) == null) ? 0 : this.tgl.get_isOn());
                }
                set
                {
                    if ((this.tgl != null) == null)
                    {
                        goto Label_001D;
                    }
                    this.tgl.set_isOn(value);
                Label_001D:
                    return;
                }
            }
        }
    }
}

