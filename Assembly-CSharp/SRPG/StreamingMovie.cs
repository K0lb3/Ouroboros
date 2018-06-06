// Decompiled with JetBrains decompiler
// Type: SRPG.StreamingMovie
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace SRPG
{
  public class StreamingMovie : MonoSingleton<StreamingMovie>
  {
    private StreamingMovie.OnFinished onFinished;
    private bool skip;

    private string URL
    {
      get
      {
        return AssetDownloader.StreamingURL;
      }
    }

    private string Extention
    {
      get
      {
        return ".mp4";
      }
    }

    protected override void Initialize()
    {
      base.Initialize();
      this._Initialize();
    }

    protected override void Release()
    {
      base.Release();
      this._Release();
    }

    public void Skip()
    {
      this.skip = true;
    }

    public void Play(string fileName, StreamingMovie.OnFinished callback = null, string prefabPath = null)
    {
      this.onFinished = callback;
      this.StartCoroutine(this._Play(Path.Combine(this.URL, fileName) + this.Extention, prefabPath));
    }

    private void _Initialize()
    {
    }

    private void _Release()
    {
    }

    [DebuggerHidden]
    private IEnumerator _Play(string url, string prefabPath)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new StreamingMovie.\u003C_Play\u003Ec__Iterator5E() { url = url, prefabPath = prefabPath, \u003C\u0024\u003Eurl = url, \u003C\u0024\u003EprefabPath = prefabPath, \u003C\u003Ef__this = this };
    }

    public void _Finish()
    {
      if (this.onFinished == null)
        return;
      try
      {
        this.onFinished();
      }
      catch (Exception ex)
      {
        Debug.Log((object) ex.ToString());
      }
    }

    public delegate void OnFinished();
  }
}
