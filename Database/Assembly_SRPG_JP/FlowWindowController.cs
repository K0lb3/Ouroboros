// Decompiled with JetBrains decompiler
// Type: SRPG.FlowWindowController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class FlowWindowController
  {
    private bool m_Enabled = true;
    private List<FlowWindowBase> m_List = new List<FlowWindowBase>();
    private FlowNode m_FlowNode;

    public bool enabled
    {
      set
      {
        this.m_Enabled = value;
      }
      get
      {
        return this.m_Enabled;
      }
    }

    public void Initialize(FlowNode node)
    {
      this.m_FlowNode = node;
    }

    public void Release()
    {
      for (int index = 0; index < this.m_List.Count; ++index)
        this.m_List[index].Release();
      this.m_List.Clear();
    }

    public void Start()
    {
      for (int index = 0; index < this.m_List.Count; ++index)
        this.m_List[index].Start();
    }

    public void Update()
    {
      for (int index = 0; index < this.m_List.Count; ++index)
        this.m_List[index].Update(this);
    }

    public void LateUpdate()
    {
      for (int index = 0; index < this.m_List.Count; ++index)
        this.m_List[index].LateUpdate(this.m_FlowNode);
    }

    public void ActivateOutputLinks(int pinId)
    {
      if (!this.enabled || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_FlowNode, (UnityEngine.Object) null))
        return;
      this.m_FlowNode.ActivateOutputLinks(pinId);
    }

    public void Add(FlowWindowBase.SerializeParamBase param)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) param.window, (UnityEngine.Object) null))
        return;
      FlowWindowBase instance = Activator.CreateInstance(param.type) as FlowWindowBase;
      if (instance == null)
        return;
      instance.Initialize(param);
      this.m_List.Add(instance);
    }

    public void Remove(FlowWindowBase window)
    {
      window.Release();
      this.m_List.Remove(window);
    }

    public FlowWindowBase GetWindow(string name)
    {
      for (int index = 0; index < this.m_List.Count; ++index)
      {
        if (this.m_List[index].name == name)
          return this.m_List[index];
      }
      return (FlowWindowBase) null;
    }

    public FlowWindowBase GetWindow(System.Type type)
    {
      for (int index = 0; index < this.m_List.Count; ++index)
      {
        if ((object) this.m_List[index].GetType() == (object) type)
          return this.m_List[index];
      }
      return (FlowWindowBase) null;
    }

    public T GetWindow<T>() where T : FlowWindowBase
    {
      for (int index = 0; index < this.m_List.Count; ++index)
      {
        if (this.m_List[index] is T)
          return this.m_List[index] as T;
      }
      return (T) null;
    }

    public bool IsStartUp()
    {
      for (int index = 0; index < this.m_List.Count; ++index)
      {
        if (!this.m_List[index].IsStartUp())
          return false;
      }
      return true;
    }

    public void OnActivate(int pinId)
    {
      if (!this.enabled)
        return;
      for (int index = 0; index < this.m_List.Count; ++index)
        this.m_List[index].RequestPin(pinId);
    }
  }
}
