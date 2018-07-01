// Decompiled with JetBrains decompiler
// Type: SRPG.UnitBuffDisplay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class UnitBuffDisplay : MonoBehaviour
  {
    private static float DISP_TIME = 3f;
    public UnitBuffDisplay.NodeData[] m_NodeData;
    public GameObject m_Root;
    public UnitBuffDisplayNode m_NodeRoot;
    private Unit m_Owner;
    private List<UnitBuffDisplay.Object> m_Objects;
    private List<UnitBuffDisplay.Object> m_DispObjects;
    private float m_Time;
    private UnitBuffDisplay.Object m_CurrentObject;

    public UnitBuffDisplay()
    {
      base.\u002Ector();
    }

    public UnitBuffDisplay.Object[] objects
    {
      get
      {
        return this.m_Objects.ToArray();
      }
    }

    private void Awake()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_NodeRoot, (UnityEngine.Object) null))
        return;
      ((Component) this.m_NodeRoot).get_gameObject().SetActive(false);
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitBuffDisplay.\u003CStart\u003Ec__Iterator11F() { \u003C\u003Ef__this = this };
    }

    private void OnDestroy()
    {
      this.Release();
    }

    private void Initiallize(Unit owner)
    {
      this.m_Owner = owner;
    }

    private void Release()
    {
      for (int index = 0; index < this.m_Objects.Count; ++index)
        this.RemoveNode(this.m_Objects[index]);
      this.m_Objects.Clear();
      this.m_DispObjects.Clear();
      this.m_CurrentObject = (UnitBuffDisplay.Object) null;
      this.m_Owner = (Unit) null;
    }

    private void Update()
    {
      if (this.m_Owner == null)
        return;
      this.RequestBuff();
      this.UpdateBuff();
      this.UpdateNodeDisp();
    }

    private void RequestBuff()
    {
      if (this.m_Owner == null)
        return;
      for (int index1 = 0; index1 < this.m_Owner.BuffAttachments.Count; ++index1)
      {
        BuffAttachment buffAttachment = this.m_Owner.BuffAttachments[index1];
        if (!(bool) buffAttachment.IsPassive && this.GetObject(buffAttachment) == null)
        {
          foreach (UnitBuffDisplayNode.Param obj in UnitBuffDisplayNode.CreateParams(this, this.m_Owner, buffAttachment))
          {
            UnitBuffDisplay.Object @object = (UnitBuffDisplay.Object) null;
            for (int index2 = 0; index2 < this.m_Objects.Count; ++index2)
            {
              if (this.m_Objects[index2].IsEqual(obj))
              {
                @object = this.m_Objects[index2];
                break;
              }
            }
            if (@object == null)
              this.m_Objects.Add(new UnitBuffDisplay.Object(obj));
            else
              @object.Add(obj);
          }
        }
      }
    }

    private void UpdateBuff()
    {
      this.m_DispObjects.Clear();
      for (int index1 = 0; index1 < this.m_Objects.Count; ++index1)
      {
        UnitBuffDisplay.Object @object = this.m_Objects[index1];
        for (int index2 = 0; index2 < @object.paramlist.Count; ++index2)
        {
          if (!@object.paramlist[index2].isAlive)
          {
            @object.Remove(@object.paramlist[index2]);
            --index2;
          }
        }
        if (@object.paramlist.Count == 0)
        {
          this.RemoveNode(@object);
          this.m_Objects.Remove(@object);
          --index1;
        }
        else
        {
          bool flag = false;
          for (int index2 = 0; index2 < @object.paramlist.Count; ++index2)
          {
            if (@object.paramlist[index2].isNeedDispOn)
            {
              flag = true;
              break;
            }
          }
          if (flag)
            this.m_DispObjects.Add(@object);
        }
      }
    }

    private void UpdateNodeDisp()
    {
      if (this.m_DispObjects.Count == 0)
        return;
      this.m_Time += Time.get_deltaTime();
      if (this.m_CurrentObject != null && (double) this.m_Time < (double) UnitBuffDisplay.DISP_TIME)
        return;
      int index1 = 0;
      if (this.m_CurrentObject != null)
      {
        for (int index2 = 0; index2 < this.m_DispObjects.Count; ++index2)
        {
          if (this.m_CurrentObject == this.m_DispObjects[index2])
          {
            index1 = index2 + 1;
            break;
          }
        }
        if (index1 >= this.m_DispObjects.Count)
          index1 = 0;
        if (this.m_DispObjects[index1] == this.m_CurrentObject)
          return;
      }
      this.CreateNode(this.m_DispObjects[index1]);
    }

    private UnitBuffDisplay.Object GetObject(BuffAttachment buff)
    {
      for (int index = 0; index < this.m_Objects.Count; ++index)
      {
        if (this.m_Objects[index].paramlist.FindIndex((Predicate<UnitBuffDisplayNode.Param>) (prop => prop.buff == buff)) != -1)
          return this.m_Objects[index];
      }
      return (UnitBuffDisplay.Object) null;
    }

    private void CreateNode(UnitBuffDisplay.Object obj)
    {
      if (this.m_CurrentObject != null)
        this.RemoveNode(this.m_CurrentObject);
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) ((Component) this.m_NodeRoot).get_gameObject());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
      {
        obj.node = (UnitBuffDisplayNode) gameObject.GetComponent<UnitBuffDisplayNode>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) obj.node, (UnityEngine.Object) null))
        {
          obj.node.Setup(obj.paramlist[0]);
          gameObject.get_transform().SetParent(this.m_Root.get_transform(), false);
        }
        gameObject.SetActive(true);
      }
      this.m_Time = 0.0f;
      this.m_CurrentObject = obj;
    }

    private void RemoveNode(UnitBuffDisplay.Object obj)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) obj.node, (UnityEngine.Object) null))
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) obj.node).get_gameObject());
        obj.node = (UnitBuffDisplayNode) null;
      }
      if (this.m_CurrentObject != obj)
        return;
      this.m_CurrentObject = (UnitBuffDisplay.Object) null;
    }

    public UnitBuffDisplay.NodeData GetNodeData(UnitBuffDisplayNode.BuffType buffType)
    {
      for (int index = 0; index < this.m_NodeData.Length; ++index)
      {
        if (this.m_NodeData[index] != null && this.m_NodeData[index].buff == buffType)
          return this.m_NodeData[index];
      }
      return (UnitBuffDisplay.NodeData) null;
    }

    private void OnEnable()
    {
      this.UpdateBuff();
    }

    private void OnDisable()
    {
    }

    [Serializable]
    public class NodeData
    {
      public UnitBuffDisplayNode.BuffType buff;
      public UnitBuffDisplayNode.DispType disp;
      public Sprite sprite;
    }

    public class Object
    {
      public List<UnitBuffDisplayNode.Param> paramlist = new List<UnitBuffDisplayNode.Param>();
      public UnitBuffDisplayNode.DispType dispType;
      public UnitBuffDisplayNode.EffectType effectType;
      public UnitBuffDisplayNode node;

      public Object(UnitBuffDisplayNode.Param param)
      {
        this.paramlist.Add(param);
        this.dispType = param.dispType;
        this.effectType = param.effectType;
        this.node = (UnitBuffDisplayNode) null;
      }

      public void Add(UnitBuffDisplayNode.Param param)
      {
        this.paramlist.Add(param);
      }

      public void Remove(UnitBuffDisplayNode.Param param)
      {
        this.paramlist.Remove(param);
      }

      public bool IsEqual(UnitBuffDisplayNode.Param param)
      {
        if (this.dispType == param.dispType)
          return this.effectType == param.effectType;
        return false;
      }
    }
  }
}
