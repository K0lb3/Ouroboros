// Decompiled with JetBrains decompiler
// Type: SRPG.SharedWebWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class SharedWebWindow : MonoBehaviour
  {
    [SerializeField]
    private WebView Target;
    [SerializeField]
    private GameObject Caution;

    public SharedWebWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (Object.op_Equality((Object) this.Target, (Object) null))
      {
        Transform child = ((Component) this).get_transform().FindChild("window");
        if (Object.op_Inequality((Object) child, (Object) null))
        {
          WebView component = (WebView) ((Component) child).GetComponent<WebView>();
          if (Object.op_Inequality((Object) component, (Object) null))
            this.Target = component;
        }
      }
      if (Object.op_Inequality((Object) this.Caution, (Object) null))
      {
        this.Caution.SetActive(false);
      }
      else
      {
        if (!Object.op_Inequality((Object) this.Target, (Object) null))
          return;
        Transform child = ((Component) this.Target).get_transform().FindChild("caution");
        if (!Object.op_Inequality((Object) child, (Object) null))
          return;
        this.Caution = ((Component) child).get_gameObject();
        this.Caution.SetActive(false);
      }
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.Target, (Object) null))
        return;
      string text = FlowNode_Variable.Get("SHARED_WEBWINDOW_TITLE");
      if (!string.IsNullOrEmpty(text))
        this.Target.SetTitleText(text);
      string url = FlowNode_Variable.Get("SHARED_WEBWINDOW_URL");
      if (!string.IsNullOrEmpty(url))
      {
        this.Target.OpenURL(url);
        FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", string.Empty);
      }
      else
        this.Caution.SetActive(true);
    }
  }
}
