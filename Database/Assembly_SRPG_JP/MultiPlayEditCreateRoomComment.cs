// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayEditCreateRoomComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class MultiPlayEditCreateRoomComment : MonoBehaviour
  {
    public InputFieldCensorship Comment;

    public MultiPlayEditCreateRoomComment()
    {
      base.\u002Ector();
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void OnClickEdit()
    {
      this.Comment.set_readOnly(false);
      this.Comment.ActivateInputField();
    }

    public void OnEndEdit()
    {
      this.Comment.set_readOnly(true);
    }
  }
}
