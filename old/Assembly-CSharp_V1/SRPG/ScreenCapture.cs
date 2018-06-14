// Decompiled with JetBrains decompiler
// Type: SRPG.ScreenCapture
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  internal class ScreenCapture : MonoBehaviour
  {
    internal byte[] Image;
    internal bool isCapturing;

    public ScreenCapture()
    {
      base.\u002Ector();
    }

    internal void SaveScreenshot(bool changeOrientation)
    {
      this.isCapturing = true;
      this.StartCoroutine(this.SaveScreenshot_ReadPixelsAsynch(changeOrientation));
    }

    [DebuggerHidden]
    private IEnumerator SaveScreenshot_ReadPixelsAsynch(bool changeOrientation)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ScreenCapture.\u003CSaveScreenshot_ReadPixelsAsynch\u003Ec__Iterator24() { changeOrientation = changeOrientation, \u003C\u0024\u003EchangeOrientation = changeOrientation, \u003C\u003Ef__this = this };
    }

    private Texture2D LandscapeToPortrait(Texture2D origTex)
    {
      Color[] pixels = origTex.GetPixels();
      Color[] colorArray = new Color[pixels.Length];
      for (int index1 = 0; index1 < ((Texture) origTex).get_width(); ++index1)
      {
        for (int index2 = 0; index2 < ((Texture) origTex).get_height(); ++index2)
        {
          int num1 = index2;
          int num2 = ((Texture) origTex).get_width() - index1 - 1;
          colorArray[num2 * ((Texture) origTex).get_height() + num1] = pixels[index2 * ((Texture) origTex).get_width() + index1];
        }
      }
      Texture2D texture2D = new Texture2D(((Texture) origTex).get_height(), ((Texture) origTex).get_width());
      texture2D.SetPixels(colorArray);
      texture2D.Apply();
      return texture2D;
    }
  }
}
