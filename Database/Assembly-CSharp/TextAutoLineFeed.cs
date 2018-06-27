// Decompiled with JetBrains decompiler
// Type: TextAutoLineFeed
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class TextAutoLineFeed : MonoBehaviour
{
  [SerializeField]
  private int LineMaxLength;
  [SerializeField]
  private string[] BeforeInsert;
  [SerializeField]
  private string[] LaterInsert;
  [SerializeField]
  private bool IgnoreEmptyLine;
  private Text mTargetText;
  private string mPreText;

  public TextAutoLineFeed()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.mTargetText = (Text) ((Component) this).GetComponent<Text>();
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.mTargetText, (Object) null) || string.IsNullOrEmpty(this.mTargetText.get_text()) || (this.mPreText == this.mTargetText.get_text() || this.mTargetText.get_text().Length <= this.LineMaxLength))
      return;
    this.mTargetText.set_text(this.InsertLineFeed(this.mTargetText.get_text()));
    if (this.IgnoreEmptyLine)
      this.mTargetText.set_text(this.DeleteEmptyLine(this.mTargetText.get_text()));
    this.mPreText = this.mTargetText.get_text();
  }

  private string InsertLineFeed(string text)
  {
    foreach (string str in this.BeforeInsert)
    {
      if (!string.IsNullOrEmpty(str))
      {
        int num1 = text.IndexOf(str, 1);
        if (num1 != -1)
        {
          int num2 = num1;
          return this.InsertLineFeed(text.Substring(0, num2)) + "\n" + this.InsertLineFeed(text.Substring(num2));
        }
      }
    }
    foreach (string str in this.LaterInsert)
    {
      if (!string.IsNullOrEmpty(str))
      {
        int num1 = text.IndexOf(str, 0, text.Length - 1);
        if (num1 != -1)
        {
          int num2 = num1 + str.Length;
          return this.InsertLineFeed(text.Substring(0, num2)) + "\n" + this.InsertLineFeed(text.Substring(num2));
        }
      }
    }
    return text;
  }

  private string DeleteEmptyLine(string text)
  {
    text = text.Replace("\n\n", "\n");
    return text;
  }
}
