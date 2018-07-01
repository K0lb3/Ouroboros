// Decompiled with JetBrains decompiler
// Type: SRPG.TeamNameInputWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class TeamNameInputWindow : MonoBehaviour
  {
    [SerializeField]
    private InputFieldCensorship inputField;

    public TeamNameInputWindow()
    {
      base.\u002Ector();
    }

    public void SetInputName()
    {
      if (string.IsNullOrEmpty(this.inputField.get_text()))
        return;
      string str = this.inputField.get_text();
      if (str.Length > this.inputField.get_characterLimit())
        str = str.Substring(0, this.inputField.get_characterLimit());
      GlobalVars.TeamName = str;
    }
  }
}
