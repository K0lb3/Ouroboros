// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DeletePrefs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/DeletePrefs", 32741)]
  public class FlowNode_DeletePrefs : FlowNode
  {
    public FlowNode_DeletePrefs.PrefsType Type;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (this.Type == FlowNode_DeletePrefs.PrefsType.PlayerPrefs || this.Type == FlowNode_DeletePrefs.PrefsType.All)
      {
        bool flag = GameUtility.Config_UseAssetBundles.Value;
        PlayerPrefsUtility.DeleteAll();
        GameUtility.Config_UseAssetBundles.Value = flag;
      }
      if (this.Type == FlowNode_DeletePrefs.PrefsType.UserInfoManager || this.Type == FlowNode_DeletePrefs.PrefsType.All)
        MonoSingleton<UserInfoManager>.Instance.Delete();
      this.ActivateOutputLinks(10);
    }

    public enum PrefsType : byte
    {
      None,
      PlayerPrefs,
      UserInfoManager,
      All,
    }
  }
}
