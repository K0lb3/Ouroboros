// Decompiled with JetBrains decompiler
// Type: GridLayer
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (MeshRenderer))]
[RequireComponent(typeof (MeshFilter))]
[DisallowMultipleComponent]
public class GridLayer : MonoBehaviour
{
  public int LayerID;
  [Multiline]
  public string Preview;
  private int mXSize;
  private int mYSize;
  private Texture2D mTex;
  private Color32[] mPixels;
  private float mOpacity;
  private float mTransitTime;
  private float mDesiredOpacity;
  private float mCurrentOpacity;

  public GridLayer()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    if (string.IsNullOrEmpty(this.Preview))
      return;
    string[] strArray = this.Preview.Split('\n');
    GridMap<Color32> grid = new GridMap<Color32>(strArray[0].Length, strArray.Length);
    Color32[] color32Array = new Color32[7]{ new Color32((byte) 0, (byte) 0, (byte) 0, (byte) 0), new Color32(byte.MaxValue, (byte) 64, (byte) 64, byte.MaxValue), new Color32((byte) 64, byte.MaxValue, (byte) 64, byte.MaxValue), new Color32((byte) 64, (byte) 64, byte.MaxValue, byte.MaxValue), new Color32(byte.MaxValue, byte.MaxValue, (byte) 64, byte.MaxValue), new Color32(byte.MaxValue, (byte) 64, byte.MaxValue, byte.MaxValue), new Color32((byte) 64, byte.MaxValue, byte.MaxValue, byte.MaxValue) };
    for (int y = 0; y < strArray.Length; ++y)
    {
      for (int index = 0; index < strArray[0].Length && index < strArray[y].Length; ++index)
      {
        int result;
        int.TryParse(strArray[y].Substring(index, 1), out result);
        grid.set(index, y, color32Array[result % color32Array.Length]);
      }
    }
    this.UpdateGrid(grid);
  }

  public void UpdateGrid(GridMap<Color32> grid)
  {
    if (grid.w != this.mXSize || grid.h != this.mYSize)
      this.InitTexture(grid.w, grid.h);
    int width = ((Texture) this.mTex).get_width();
    Color32[] mPixels = this.mPixels;
    for (int y = 0; y < grid.h; ++y)
    {
      for (int x = 0; x < grid.w; ++x)
        mPixels[x + 1 + (y + 1) * width] = grid.get(x, y);
    }
    this.mTex.SetPixels32(mPixels);
    this.mTex.Apply();
  }

  private void Awake()
  {
    ((Renderer) ((Component) this).GetComponent<Renderer>()).set_material((Material) Resources.Load<Material>("BG/GridMaterial"));
  }

  private void Update()
  {
    float num = 2f * Time.get_deltaTime();
    if ((double) this.mDesiredOpacity < (double) this.mCurrentOpacity)
      num = -num;
    this.mCurrentOpacity = Mathf.Clamp01(this.mCurrentOpacity + num);
    if ((double) this.mDesiredOpacity > 0.0 || (double) this.mCurrentOpacity > 0.0)
      return;
    ((Component) this).get_gameObject().SetActive(false);
  }

  private void OnWillRenderObject()
  {
    ((Renderer) ((Component) this).GetComponent<Renderer>()).get_material().SetFloat("_opacity", this.mCurrentOpacity);
  }

  private void InitTexture(int w, int h)
  {
    this.mXSize = w;
    this.mYSize = h;
    w += 2;
    h += 2;
    Object.DestroyImmediate((Object) this.mTex);
    if (!Mathf.IsPowerOfTwo(w))
      w = Mathf.NextPowerOfTwo(w);
    if (!Mathf.IsPowerOfTwo(h))
      h = Mathf.NextPowerOfTwo(h);
    this.mTex = new Texture2D(w, h, (TextureFormat) 4, false);
    ((Texture) this.mTex).set_wrapMode((TextureWrapMode) 1);
    ((Texture) this.mTex).set_filterMode((FilterMode) 0);
    Vector2 vector2 = (Vector2) null;
    vector2.x = (__Null) ((double) this.mXSize / (double) ((Texture) this.mTex).get_width() / (double) this.mXSize);
    vector2.y = (__Null) ((double) this.mYSize / (double) ((Texture) this.mTex).get_height() / (double) this.mYSize);
    Material material = ((Renderer) ((Component) this).GetComponent<Renderer>()).get_material();
    material.SetTexture("_indexTex", (Texture) this.mTex);
    material.SetTextureScale("_indexTex", vector2);
    material.SetTextureOffset("_indexTex", vector2);
    if (!string.IsNullOrEmpty(SystemInfo.get_deviceModel()))
    {
      string[] strArray = new string[69]{ "FJL21", "FJL22", "HW-01E", "HW-03E", "016SH", "106SH", "107SH", "200SH", "205SH", "206SH", "302SH", "303SH", "304SH", "201K", "202F", "L-01F", "SC-01D", "SC-03D", "SC-05D", "SC-06D", "SHL21", "SHL22", "SHL23", "SHL24", "SHL25", "N-04D", "N-05D", "N-07D", "N-08D", "F-02E", "F-03E", "F-04E", "F-06E", "F-05D", "F-11D", "SH-01E", "SH-04E", "SH-06E", "SH-07E", "SH-07D", "SH-09D", "SH-10D", "SH-02F", "SH-05F", "WX05SH", "P-02E", "P-03E", "ISW11F", "ISW13F", "IS12S", "IS15SH", "IS17SH", "SO-02E", "SO-03E", "SO-04E", "SO-03D", "SO-04D", "SO-05D", "SOL21", "SOL22", "SOL23", "LGL21", "LGL22", "LGL23", "KYY04", "KYY21", "KYY22", "KYY23", "KYY24" };
      foreach (string str in strArray)
      {
        if (SystemInfo.get_deviceModel().Contains(str))
        {
          material.SetFloat("_offset", 0.0f);
          material.SetFloat("_offsetZ", -1f / 1000f);
          break;
        }
      }
    }
    this.mPixels = new Color32[((Texture) this.mTex).get_width() * ((Texture) this.mTex).get_height()];
  }

  private void OnDestroy()
  {
    if (Object.op_Inequality((Object) this.mTex, (Object) null))
    {
      Object.DestroyImmediate((Object) this.mTex);
      this.mTex = (Texture2D) null;
    }
    this.mPixels = (Color32[]) null;
  }

  public void Show()
  {
    if ((double) this.mDesiredOpacity >= 1.0)
      return;
    this.mCurrentOpacity = 0.0f;
    this.mDesiredOpacity = 1f;
    ((Component) this).get_gameObject().SetActive(true);
  }

  public void Hide()
  {
    if ((double) this.mDesiredOpacity <= 0.0)
      return;
    this.mDesiredOpacity = 0.0f;
  }

  public void SetMask(bool enable)
  {
    Material material = ((Renderer) ((Component) this).GetComponent<Renderer>()).get_material();
    if (enable)
      material.set_shaderKeywords(new string[1]
      {
        "WITH_MASK"
      });
    else
      material.set_shaderKeywords(new string[1]
      {
        "WITHOUT_MASK"
      });
  }

  public void ChangeMaterial(string path)
  {
    if (string.IsNullOrEmpty(path))
      return;
    Material material = (Material) Resources.Load<Material>(path);
    if (!Object.op_Implicit((Object) material))
      return;
    Renderer component = (Renderer) ((Component) this).GetComponent<Renderer>();
    if (!Object.op_Implicit((Object) component))
      return;
    component.set_material(material);
  }
}
