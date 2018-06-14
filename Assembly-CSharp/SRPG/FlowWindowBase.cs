// Decompiled with JetBrains decompiler
// Type: SRPG.FlowWindowBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class FlowWindowBase
  {
    protected List<int> m_Request = new List<int>();
    protected List<IEnumerator> m_TaskReq = new List<IEnumerator>();
    private FlowWindowController m_Controller;
    protected GameObject m_Window;
    protected Animator m_Animator;
    protected FlowWindowBase.AnimState m_AnimState;
    protected bool m_StartUp;
    protected bool m_AnimStart;
    protected int m_FrameCount;
    protected IEnumerator m_Task;

    public virtual string name
    {
      get
      {
        return "FriendPresentWindowBase";
      }
    }

    public bool isValid
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Window, (UnityEngine.Object) null);
      }
    }

    public bool isOpen
    {
      get
      {
        return this.m_AnimState == FlowWindowBase.AnimState.OPEN;
      }
    }

    public bool isClose
    {
      get
      {
        return this.m_AnimState == FlowWindowBase.AnimState.CLOSE;
      }
    }

    public bool isOpened
    {
      get
      {
        return this.m_AnimState == FlowWindowBase.AnimState.OPENED;
      }
    }

    public bool isClosed
    {
      get
      {
        return this.m_AnimState == FlowWindowBase.AnimState.CLOSED;
      }
    }

    public virtual void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      this.m_Request.Clear();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) param.window, (UnityEngine.Object) null))
        return;
      this.m_Window = param.window;
      this.m_Animator = (Animator) param.window.GetComponent<Animator>();
      this.m_Window.SetActive(true);
    }

    public virtual void Release()
    {
      this.ClearTask();
      this.m_Request.Clear();
    }

    public virtual void Start()
    {
      this.StartUp();
    }

    public void Update(FlowWindowController controller)
    {
      this.m_Controller = controller;
      if (this.UpdateTask())
        return;
      if (this.m_Request.Count > 0)
      {
        for (int index = 0; index < this.m_Request.Count; ++index)
        {
          int pinId = this.OnActivate(this.m_Request[index]);
          if (pinId != -1)
            controller.ActivateOutputLinks(pinId);
        }
        this.m_Request.Clear();
      }
      int pinId1 = this.Update();
      if (pinId1 != -1)
        controller.ActivateOutputLinks(pinId1);
      this.m_Controller = (FlowWindowController) null;
    }

    public void LateUpdate(FlowNode flowNode)
    {
    }

    public virtual int Update()
    {
      if (this.m_AnimState == FlowWindowBase.AnimState.OPEN)
      {
        if (this.m_AnimStart && this.m_FrameCount != Time.get_frameCount())
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Animator, (UnityEngine.Object) null))
            this.m_Animator.SetBool("close", false);
          this.m_AnimStart = false;
          this.m_FrameCount = 0;
        }
        if (this.IsState("opened"))
        {
          int pinId = this.OnOpened();
          if (pinId != -1)
            this.m_Controller.ActivateOutputLinks(pinId);
          this.m_AnimState = FlowWindowBase.AnimState.OPENED;
        }
      }
      else if (this.m_AnimState == FlowWindowBase.AnimState.CLOSE && this.IsState("closed"))
      {
        int pinId = this.OnClosed();
        if (pinId != -1)
          this.m_Controller.ActivateOutputLinks(pinId);
        this.m_AnimState = FlowWindowBase.AnimState.CLOSED;
      }
      return -1;
    }

    public void Open()
    {
      this.m_AnimState = FlowWindowBase.AnimState.OPEN;
      this.m_AnimStart = true;
      this.m_FrameCount = Time.get_frameCount();
      this.SetActiveChild(true);
    }

    public void Close(bool immidiate = false)
    {
      this.m_AnimState = FlowWindowBase.AnimState.CLOSE;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Window, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Animator, (UnityEngine.Object) null))
        return;
      this.m_Animator.SetBool("close", true);
      if (!immidiate)
        return;
      this.m_Animator.Play("closed");
      this.SetActiveChild(false);
    }

    private void ClearTask()
    {
      this.m_TaskReq.Clear();
      this.m_Task = (IEnumerator) null;
    }

    protected void AddTask(IEnumerator enumrator)
    {
      this.m_TaskReq.Add(enumrator);
    }

    public bool UpdateTask()
    {
      if (this.m_TaskReq.Count == 0)
        return false;
      for (int index = 0; index < this.m_TaskReq.Count; ++index)
      {
        IEnumerator enumerator = this.m_TaskReq[index];
        if (enumerator == null || !enumerator.MoveNext())
        {
          this.m_TaskReq.RemoveAt(index);
          --index;
        }
      }
      return true;
    }

    public void StartUp()
    {
      this.m_StartUp = true;
    }

    public void SetActiveChild(GameObject root, bool value)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) root, (UnityEngine.Object) null))
        return;
      Transform transform = root.get_transform();
      for (int index = 0; index < transform.get_childCount(); ++index)
      {
        Transform child = transform.GetChild(index);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
          ((Component) child).get_gameObject().SetActive(value);
      }
    }

    public void SetActiveChild(bool value)
    {
      this.SetActiveChild(this.m_Window, value);
    }

    public GameObject GetChild(string name)
    {
      return this.GetChild(this.m_Window, name);
    }

    public GameObject GetChild(GameObject root, string name)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) root, (UnityEngine.Object) null))
      {
        if (((UnityEngine.Object) root).get_name() == name)
          return root;
        Transform transform = root.get_transform();
        for (int index = 0; index < transform.get_childCount(); ++index)
        {
          Transform child = transform.GetChild(index);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null) && ((UnityEngine.Object) child).get_name() == name)
            return ((Component) child).get_gameObject();
        }
      }
      return (GameObject) null;
    }

    public GameObject GetChildAll(string name)
    {
      return this.GetChildAll(this.m_Window, name);
    }

    public GameObject GetChildAll(GameObject root, string name)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) root, (UnityEngine.Object) null))
      {
        if (((UnityEngine.Object) root).get_name() == name)
          return root;
        Transform transform = root.get_transform();
        List<GameObject> gameObjectList = new List<GameObject>();
        for (int index = 0; index < transform.get_childCount(); ++index)
        {
          Transform child = transform.GetChild(index);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
          {
            if (((UnityEngine.Object) child).get_name() == name)
              return ((Component) child).get_gameObject();
            gameObjectList.Add(((Component) child).get_gameObject());
          }
        }
        for (int index = 0; index < gameObjectList.Count; ++index)
        {
          GameObject child = this.GetChild(gameObjectList[index], name);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
            return child;
        }
      }
      return (GameObject) null;
    }

    public T GetChildComponent<T>(GameObject root, string name) where T : Component
    {
      GameObject child = this.GetChild(root, name);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
        return child.GetComponent<T>();
      return (T) null;
    }

    public T GetChildComponent<T>(string name) where T : Component
    {
      GameObject child = this.GetChild(name);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
        return child.GetComponent<T>();
      return (T) null;
    }

    public T GetChildAllComponent<T>(string name) where T : Component
    {
      GameObject childAll = this.GetChildAll(name);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) childAll, (UnityEngine.Object) null))
        return childAll.GetComponent<T>();
      return (T) null;
    }

    public bool IsStartUp()
    {
      return this.m_StartUp;
    }

    public bool IsState(string stateName)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Window, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Animator, (UnityEngine.Object) null) || (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Animator.get_runtimeAnimatorController(), (UnityEngine.Object) null) || this.m_Animator.get_runtimeAnimatorController().get_animationClips() == null) || this.m_Animator.get_runtimeAnimatorController().get_animationClips().Length == 0)
        return true;
      AnimatorStateInfo animatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      return ((AnimatorStateInfo) @animatorStateInfo).IsName(stateName);
    }

    public void RequestPin(int pinId)
    {
      this.m_Request.Add(pinId);
    }

    public virtual int OnActivate(int pinId)
    {
      return -1;
    }

    protected virtual int OnOpened()
    {
      return -1;
    }

    protected virtual int OnClosed()
    {
      return -1;
    }

    public enum AnimState
    {
      IDEL,
      OPEN,
      CLOSE,
      OPENED,
      CLOSED,
    }

    [Serializable]
    public class SerializeParamBase
    {
      public GameObject window;

      public virtual System.Type type
      {
        get
        {
          return typeof (FlowWindowBase);
        }
      }
    }
  }
}
