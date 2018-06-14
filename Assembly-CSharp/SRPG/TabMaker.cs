// Decompiled with JetBrains decompiler
// Type: SRPG.TabMaker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TabMaker : MonoBehaviour
  {
    public SerializeValueBehaviour m_TabNode;
    public Sprite m_CornerSpriteOff;
    public Sprite m_CornerSpriteOn;
    public Sprite m_SpriteOff;
    public Sprite m_SpriteOn;
    public TabMaker.Element[] m_Elements;
    private List<TabMaker.Info> m_InfoList;

    public TabMaker()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_TabNode, (UnityEngine.Object) null))
        return;
      ((Component) this.m_TabNode).get_gameObject().SetActive(false);
    }

    private void Start()
    {
    }

    private void OnDestroy()
    {
      this.Destroy();
    }

    private void Update()
    {
    }

    public void Create(string[] keys, Action<GameObject, SerializeValueList> callback)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_TabNode, (UnityEngine.Object) null))
      {
        Debug.LogException(new Exception("Failed TabNode Null > " + ((UnityEngine.Object) ((Component) this).get_gameObject()).get_name()));
      }
      else
      {
        this.Destroy();
        for (int index = 0; index < keys.Length; ++index)
        {
          TabMaker.Element element = this.GetElement(keys[index]);
          if (element == null)
          {
            Debug.LogError((object) ("Tab素材見つかりません > " + (keys[index] == null ? "null" : keys[index])));
          }
          else
          {
            GameObject _node = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) ((Component) this.m_TabNode).get_gameObject());
            SerializeValueBehaviour component = (SerializeValueBehaviour) _node.GetComponent<SerializeValueBehaviour>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            {
              Image uiImage1 = component.list.GetUIImage("off");
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiImage1, (UnityEngine.Object) null))
              {
                if (index == 0 || index == keys.Length - 1)
                {
                  uiImage1.set_sprite(!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_CornerSpriteOff, (UnityEngine.Object) null) ? this.m_SpriteOff : this.m_CornerSpriteOff);
                  if (index == keys.Length - 1)
                  {
                    Vector3 localScale = ((Component) uiImage1).get_transform().get_localScale();
                    // ISSUE: explicit reference operation
                    // ISSUE: variable of a reference type
                    Vector3& local = @localScale;
                    // ISSUE: explicit reference operation
                    // ISSUE: explicit reference operation
                    (^local).x = (__Null) ((^local).x * -1.0);
                    ((Component) uiImage1).get_transform().set_localScale(localScale);
                  }
                }
                else
                  uiImage1.set_sprite(this.m_SpriteOff);
              }
              Image uiImage2 = component.list.GetUIImage("on");
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiImage2, (UnityEngine.Object) null))
              {
                if (index == 0 || index == keys.Length - 1)
                {
                  uiImage2.set_sprite(!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_CornerSpriteOn, (UnityEngine.Object) null) ? this.m_SpriteOn : this.m_CornerSpriteOn);
                  if (index == keys.Length - 1)
                  {
                    Vector3 localScale = ((Component) uiImage2).get_transform().get_localScale();
                    // ISSUE: explicit reference operation
                    // ISSUE: variable of a reference type
                    Vector3& local = @localScale;
                    // ISSUE: explicit reference operation
                    // ISSUE: explicit reference operation
                    (^local).x = (__Null) ((^local).x * -1.0);
                    ((Component) uiImage2).get_transform().set_localScale(localScale);
                  }
                }
                else
                  uiImage2.set_sprite(this.m_SpriteOn);
              }
              Image uiImage3 = component.list.GetUIImage("icon");
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiImage3, (UnityEngine.Object) null))
              {
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) element.icon, (UnityEngine.Object) null))
                {
                  uiImage3.set_sprite(element.icon);
                  ((Component) uiImage3).get_gameObject().SetActive(true);
                }
                else
                  ((Component) uiImage3).get_gameObject().SetActive(false);
              }
              Text uiLabel = component.list.GetUILabel("text");
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiLabel, (UnityEngine.Object) null))
              {
                if (!string.IsNullOrEmpty(element.text))
                {
                  if (element.text.IndexOf("sys.") == -1)
                    uiLabel.set_text(element.text);
                  else
                    uiLabel.set_text(LocalizedText.Get(element.text));
                  ((Component) uiLabel).get_gameObject().SetActive(true);
                }
                else
                  ((Component) uiLabel).get_gameObject().SetActive(false);
              }
              component.list.AddObject("element", (object) element);
              if (callback != null)
                callback(_node, component.list);
              ((UnityEngine.Object) _node).set_name(element.key);
              _node.get_transform().SetParent(((Component) this).get_gameObject().get_transform(), false);
              _node.SetActive(true);
              this.m_InfoList.Add(new TabMaker.Info(_node, component.list, element));
            }
          }
        }
      }
    }

    public void Destroy()
    {
      for (int index = 0; index < this.m_InfoList.Count; ++index)
      {
        TabMaker.Info info = this.m_InfoList[index];
        if (info != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) info.node, (UnityEngine.Object) null))
          UnityEngine.Object.Destroy((UnityEngine.Object) info.node);
      }
      this.m_InfoList.Clear();
    }

    public TabMaker.Element GetElement(string key)
    {
      if (this.m_Elements == null)
        return (TabMaker.Element) null;
      for (int index = 0; index < this.m_Elements.Length; ++index)
      {
        if (this.m_Elements[index] != null && this.m_Elements[index].key == key)
          return this.m_Elements[index];
      }
      return (TabMaker.Element) null;
    }

    public TabMaker.Info[] GetInfos()
    {
      return this.m_InfoList.ToArray();
    }

    public TabMaker.Info GetInfo(string key)
    {
      for (int index = 0; index < this.m_InfoList.Count; ++index)
      {
        TabMaker.Info info = this.m_InfoList[index];
        if (info != null && info.element != null && info.element.key == key)
          return info;
      }
      return (TabMaker.Info) null;
    }

    public TabMaker.Info GetOnIfno()
    {
      for (int index = 0; index < this.m_InfoList.Count; ++index)
      {
        TabMaker.Info info = this.m_InfoList[index];
        if (info != null && info.tgl.get_isOn())
          return info;
      }
      return (TabMaker.Info) null;
    }

    public void SetOn(string key, bool value)
    {
      TabMaker.Info info = this.GetInfo(key);
      if (info == null)
        return;
      info.isOn = value;
    }

    public void SetOn(Enum key, bool value)
    {
      TabMaker.Info info = this.GetInfo(key.ToString());
      if (info == null)
        return;
      info.isOn = value;
    }

    [Serializable]
    public class Element
    {
      public string key;
      public Sprite icon;
      public string text;
      public int value;
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
        this.node = _node;
        this.values = _values;
        this.element = _element;
        this.tgl = (Toggle) this.node.GetComponent<Toggle>();
        this.ev = (ButtonEvent) this.node.GetComponent<ButtonEvent>();
      }

      public bool interactable
      {
        set
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.tgl, (UnityEngine.Object) null))
            return;
          ((Selectable) this.tgl).set_interactable(value);
        }
        get
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.tgl, (UnityEngine.Object) null))
            return ((Selectable) this.tgl).get_interactable();
          return false;
        }
      }

      public bool isOn
      {
        set
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.tgl, (UnityEngine.Object) null))
            return;
          this.tgl.set_isOn(value);
        }
        get
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.tgl, (UnityEngine.Object) null))
            return this.tgl.get_isOn();
          return false;
        }
      }

      public void SetColor(Color color)
      {
        if (this.values == null)
          return;
        Image uiImage1 = this.values.GetUIImage("off");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiImage1, (UnityEngine.Object) null))
          ((Graphic) uiImage1).set_color(color);
        Image uiImage2 = this.values.GetUIImage("on");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiImage2, (UnityEngine.Object) null))
          ((Graphic) uiImage2).set_color(color);
        Image uiImage3 = this.values.GetUIImage("icon");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiImage3, (UnityEngine.Object) null))
          ((Graphic) uiImage3).set_color(color);
        Text uiLabel = this.values.GetUILabel("text");
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) uiLabel, (UnityEngine.Object) null))
          return;
        ((Graphic) uiLabel).set_color(color);
      }
    }
  }
}
