// Decompiled with JetBrains decompiler
// Type: SRPG.SGTrophyWindow_itemCompletedTutorial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Create", FlowNode.PinTypes.Output, 0)]
  public class SGTrophyWindow_itemCompletedTutorial : MonoBehaviour, IFlowInterface
  {
    public SGTrophyWindow_itemCompletedTutorial()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(((Component) this).get_gameObject(), (TrophyParam) null);
      if (dataOfClass == null || !(dataOfClass.iname == "LOGIN_GLTUTOTIAL_01"))
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    public void Activated(int pinID)
    {
    }
  }
}
