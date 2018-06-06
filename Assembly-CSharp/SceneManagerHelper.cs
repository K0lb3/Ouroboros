// Decompiled with JetBrains decompiler
// Type: SceneManagerHelper
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine.SceneManagement;

public class SceneManagerHelper
{
  public static string ActiveSceneName
  {
    get
    {
      Scene activeScene = SceneManager.GetActiveScene();
      return ((Scene) @activeScene).get_name();
    }
  }

  public static int ActiveSceneBuildIndex
  {
    get
    {
      Scene activeScene = SceneManager.GetActiveScene();
      return ((Scene) @activeScene).get_buildIndex();
    }
  }
}
