// Decompiled with JetBrains decompiler
// Type: UIWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (CanvasGroup))]
[RequireComponent(typeof (Animator))]
[AddComponentMenu("UI/Window")]
public class UIWindow : MonoBehaviour
{
  public string OpenState;
  public string WaitState;
  public string CloseState;
  private bool mClose;
  private bool mUpdateAnimatorState;
  private bool mWaitForAnimatorStateChange;
  public UIWindow.WindowEvent OnWindowClose;
  public UIWindow.WindowEvent OnWindowOpen;

  public UIWindow()
  {
    base.\u002Ector();
  }

  public bool IsClosed
  {
    get
    {
      if (!((Component) this).get_gameObject().get_activeSelf())
        return false;
      AnimatorStateInfo animatorStateInfo = ((Animator) ((Component) this).GetComponent<Animator>()).GetCurrentAnimatorStateInfo(0);
      if (((AnimatorStateInfo) @animatorStateInfo).IsName(this.CloseState))
        return (double) ((AnimatorStateInfo) @animatorStateInfo).get_normalizedTime() >= 1.0;
      return false;
    }
  }

  public bool IsOpened
  {
    get
    {
      if (!((Component) this).get_gameObject().get_activeSelf())
        return false;
      AnimatorStateInfo animatorStateInfo = ((Animator) ((Component) this).GetComponent<Animator>()).GetCurrentAnimatorStateInfo(0);
      return ((AnimatorStateInfo) @animatorStateInfo).IsName(this.WaitState);
    }
  }

  public static bool CheckOpened(GameObject obj)
  {
    UIWindow component = (UIWindow) obj.GetComponent<UIWindow>();
    if (Object.op_Inequality((Object) component, (Object) null))
      return component.IsOpened;
    Debug.LogError((object) (obj.ToString() + " has no UIWindow component."));
    return false;
  }

  public static bool CheckClosed(GameObject obj)
  {
    UIWindow component = (UIWindow) obj.GetComponent<UIWindow>();
    if (Object.op_Inequality((Object) component, (Object) null))
      return component.IsClosed;
    Debug.LogError((object) (obj.ToString() + " has no UIWindow component."));
    return false;
  }

  public void Open()
  {
    bool activeInHierarchy = ((Component) this).get_gameObject().get_activeInHierarchy();
    if (!this.mClose)
      return;
    this.mClose = false;
    this.mWaitForAnimatorStateChange = true;
    if (activeInHierarchy)
    {
      this.UpdateAnimatorState();
    }
    else
    {
      ((CanvasGroup) ((Component) this).GetComponent<CanvasGroup>()).set_blocksRaycasts(false);
      ((Component) this).get_gameObject().SetActive(true);
      this.mUpdateAnimatorState = true;
    }
  }

  public void Close()
  {
    if (!((Component) this).get_gameObject().get_activeInHierarchy() || this.mClose)
      return;
    ((CanvasGroup) ((Component) this).GetComponent<CanvasGroup>()).set_blocksRaycasts(false);
    this.mClose = true;
    this.UpdateAnimatorState();
  }

  private void UpdateAnimatorState()
  {
    ((Animator) ((Component) this).GetComponent<Animator>()).SetBool("close", this.mClose);
    this.mWaitForAnimatorStateChange = true;
  }

  private void Awake()
  {
    if (!((Component) this).get_gameObject().get_activeInHierarchy())
    {
      this.mClose = true;
    }
    else
    {
      ((CanvasGroup) ((Component) this).GetComponent<CanvasGroup>()).set_blocksRaycasts(false);
      this.mWaitForAnimatorStateChange = true;
    }
  }

  private void OnEnable()
  {
  }

  private void OnDisable()
  {
  }

  private void Update()
  {
    if (this.mUpdateAnimatorState)
    {
      this.mUpdateAnimatorState = false;
      this.UpdateAnimatorState();
    }
    else
    {
      if (!this.mWaitForAnimatorStateChange)
        return;
      if (this.mClose)
      {
        if (!this.IsClosed)
          return;
        ((Component) this).get_gameObject().SetActive(false);
        this.mWaitForAnimatorStateChange = false;
        if (this.OnWindowClose == null)
          return;
        this.OnWindowClose(this);
      }
      else
      {
        if (!this.IsOpened)
          return;
        ((CanvasGroup) ((Component) this).GetComponent<CanvasGroup>()).set_blocksRaycasts(true);
        this.mWaitForAnimatorStateChange = false;
        if (this.OnWindowOpen == null)
          return;
        this.OnWindowOpen(this);
      }
    }
  }

  public delegate void WindowEvent(UIWindow window);
}
