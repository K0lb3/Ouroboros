// Decompiled with JetBrains decompiler
// Type: SRPG.ProgressWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ProgressWindow : MonoBehaviour
  {
    private static ProgressWindow mInstance;
    public Animator WindowAnimator;
    public Slider ProgressBar;
    public ProgressWindow.ProgressRatio Ratios;
    public string CloseTrigger;
    public float DestroyDelay;
    public Text Title;
    public Text Lore;
    public Text Percentage;
    public string PercentageFormat;
    public Text Complete;
    public string CompleteFormat;
    public ImageArray Phase;
    public GameObject notice0;
    public GameObject notice1;
    [SerializeField]
    private GameObject noticeTxt;
    [SerializeField]
    private Text downloadTxt;
    public string ImageTable;
    public RawImage[] Images;
    public float DisplayImageThreshold;
    public GameObject ImageGroup;
    public GameObject buttonL;
    public GameObject buttonR;
    public Text introduction;
    public float MinVisibleTime;
    private float mLoadTime;
    private float mLoadProgress;
    private long mKeepTotalDownloadSize;
    private long mKeepCurrentDownloadSize;
    private string previousLanguage;
    private int mCurrentImageIndex;
    private List<KeyValuePair<string, string>> mImagePairs;

    public ProgressWindow()
    {
      base.\u002Ector();
    }

    public static void OpenGenericDownloadWindow()
    {
      UnityEngine.Object.Instantiate<GameObject>((M0) AssetManager.Load<GameObject>("UI/AssetsDownloading"));
    }

    public static void OpenVersusLoadScreen()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
        return;
      ProgressWindow progressWindow = AssetManager.Load<ProgressWindow>("UI/HomeMultiPlay_VersusMatching");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) progressWindow, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) ((Component) UnityEngine.Object.Instantiate<ProgressWindow>((M0) progressWindow)).get_gameObject());
      GameUtility.FadeIn(0.1f);
    }

    public static void OpenBackgroundDownloaderBar()
    {
      ProgressWindow progressWindow = AssetManager.Load<ProgressWindow>("SGDevelopment/Tutorial/BackgroundDownloaderBar");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) progressWindow, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) ((Component) UnityEngine.Object.Instantiate<ProgressWindow>((M0) progressWindow)).get_gameObject());
    }

    public static void OpenQuestLoadScreen(string title, string lore)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
      {
        ProgressWindow progressWindow = !MonoSingleton<GameManager>.Instance.IsVersusMode() ? AssetManager.Load<ProgressWindow>("UI/QuestLoadScreen") : AssetManager.Load<ProgressWindow>("UI/QuestLoadScreen_VS");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) progressWindow, (UnityEngine.Object) null))
        {
          UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) ((Component) UnityEngine.Object.Instantiate<ProgressWindow>((M0) progressWindow)).get_gameObject());
          GameUtility.FadeIn(0.1f);
        }
      }
      if (string.IsNullOrEmpty(title))
        title = string.Empty;
      if (string.IsNullOrEmpty(lore))
        lore = string.Empty;
      ProgressWindow.SetTexts(title, lore);
    }

    public static void OpenQuestLoadScreen(QuestParam quest)
    {
      string title = (string) null;
      string lore = (string) null;
      if (quest != null)
      {
        title = quest.name;
        if (quest.type == QuestTypes.Tower)
          title = quest.title + " " + quest.name;
        if (!string.IsNullOrEmpty(quest.storyTextID))
          lore = LocalizedText.Get(quest.storyTextID);
      }
      ProgressWindow.OpenQuestLoadScreen(title, lore);
    }

    public static void SetTexts(string title, string lore)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance.Title, (UnityEngine.Object) null))
        ProgressWindow.mInstance.Title.set_text(title);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance.Lore, (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance.Lore.set_text(lore);
    }

    public static void SetLoadProgress(float t)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance.mLoadProgress = t;
    }

    public static void SetDestroyDelay(float delay)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance.DestroyDelay = delay;
    }

    public static void Close()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
        return;
      Animator animator = !UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance.WindowAnimator, (UnityEngine.Object) null) ? (Animator) ((Component) ProgressWindow.mInstance).GetComponent<Animator>() : ProgressWindow.mInstance.WindowAnimator;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) animator, (UnityEngine.Object) null))
        animator.SetTrigger(ProgressWindow.mInstance.CloseTrigger);
      if ((double) ProgressWindow.mInstance.DestroyDelay >= 0.0)
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) ProgressWindow.mInstance).get_gameObject(), ProgressWindow.mInstance.DestroyDelay);
      ProgressWindow.mInstance = (ProgressWindow) null;
    }

    private void Start()
    {
      this.mCurrentImageIndex = 0;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.introduction))
      {
        this.previousLanguage = GameUtility.Config_Language;
        if (GameUtility.Config_Language != "None")
          this.introduction.set_text(LocalizedText.Get("download.FLAVOUR_TEXT" + (object) (this.mCurrentImageIndex + 1)));
        else
          this.introduction.set_text(string.Empty);
      }
      if (this.Images != null)
      {
        for (int index = 0; index < this.Images.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Images[index], (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Graphic) this.Images[index]).get_material(), (UnityEngine.Object) null))
            ((Graphic) this.Images[index]).set_material(new Material(((Graphic) this.Images[index]).get_material()));
        }
      }
      if (!string.IsNullOrEmpty(this.ImageTable))
      {
        this.LoadImageTable();
        this.StartCoroutine(this.AnimationThread());
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ImageGroup, (UnityEngine.Object) null))
        this.ImageGroup.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.downloadTxt, (UnityEngine.Object) null))
        return;
      this.downloadTxt.set_text(LocalizedText.Get("download.FILE_CHECK"));
    }

    private void OnEnable()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance = this;
    }

    private void OnDisable()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) this))
        return;
      ProgressWindow.mInstance = (ProgressWindow) null;
    }

    public static bool ShouldKeepVisible
    {
      get
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
          return (double) ProgressWindow.mInstance.mLoadTime < (double) ProgressWindow.mInstance.MinVisibleTime;
        return false;
      }
    }

    public void MovePrevious()
    {
      --this.mCurrentImageIndex;
      if (this.mImagePairs.Count == 0)
        this.LoadImageTable();
      if (this.mCurrentImageIndex < 0)
        this.mCurrentImageIndex = this.mImagePairs.Count - 1;
      this.introduction.set_text(LocalizedText.Get("download.FLAVOUR_TEXT" + (object) (this.mCurrentImageIndex + 1)));
      this.StartCoroutine(this.AnimationThread());
    }

    public void MoveNext()
    {
      ++this.mCurrentImageIndex;
      if (this.mImagePairs.Count == 0)
        this.LoadImageTable();
      if (this.mCurrentImageIndex > this.mImagePairs.Count - 1)
        this.mCurrentImageIndex = 0;
      this.introduction.set_text(LocalizedText.Get("download.FLAVOUR_TEXT" + (object) (this.mCurrentImageIndex + 1)));
      this.StartCoroutine(this.AnimationThread());
    }

    private void Update()
    {
      float num1 = 0.0f;
      this.mLoadTime += Time.get_unscaledDeltaTime();
      if ((double) this.Ratios.Download > 0.0)
        num1 += AssetDownloader.Progress / this.Ratios.Download;
      if ((double) this.Ratios.Deserilize > 0.0)
        num1 += this.mLoadProgress / this.Ratios.Deserilize;
      float num2 = num1 / (this.Ratios.Download + this.Ratios.Deserilize);
      this.ProgressBar.set_value(num2);
      long totalDownloadSize = AssetDownloader.TotalDownloadSize;
      long currentDownloadSize = AssetDownloader.CurrentDownloadSize;
      int phase = (int) AssetDownloader.Phase;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Phase, (UnityEngine.Object) null))
        this.Phase.ImageIndex = phase;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.downloadTxt, (UnityEngine.Object) null))
      {
        if (phase == 0)
          this.downloadTxt.set_text(LocalizedText.Get("download.FILE_CHECK"));
        else
          this.downloadTxt.set_text(LocalizedText.Get("download.DATA_DOWNLOAD"));
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.noticeTxt))
      {
        if (phase == 1)
          this.noticeTxt.SetActive(true);
        else
          this.noticeTxt.SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Percentage, (UnityEngine.Object) null))
      {
        string str = string.Format(this.PercentageFormat, (object) (int) ((double) num2 * 100.0));
        if (this.Percentage.get_text() != str)
          this.Percentage.set_text(str);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Complete, (UnityEngine.Object) null))
      {
        if (this.mKeepTotalDownloadSize != totalDownloadSize)
        {
          this.mKeepTotalDownloadSize = totalDownloadSize;
          this.mKeepCurrentDownloadSize = -1L;
        }
        if (this.mKeepCurrentDownloadSize < currentDownloadSize)
        {
          this.mKeepCurrentDownloadSize = currentDownloadSize;
          string str = string.Format(this.CompleteFormat, (object) currentDownloadSize, (object) totalDownloadSize);
          if (this.Complete.get_text() != str)
            this.Complete.set_text(str);
        }
      }
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.introduction) || !(this.previousLanguage != GameUtility.Config_Language))
        return;
      this.previousLanguage = GameUtility.Config_Language;
      this.introduction.set_text(LocalizedText.Get("download.FLAVOUR_TEXT" + (object) (this.mCurrentImageIndex + 1)));
    }

    [DebuggerHidden]
    private IEnumerator AnimationThread()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ProgressWindow.\u003CAnimationThread\u003Ec__Iterator10B() { \u003C\u003Ef__this = this };
    }

    private void LoadImageTable()
    {
      TextAsset textAsset = (TextAsset) Resources.Load<TextAsset>(this.ImageTable);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) textAsset, (UnityEngine.Object) null))
        return;
      StringReader stringReader = new StringReader(textAsset.get_text());
      string str;
      while ((str = stringReader.ReadLine()) != null)
      {
        if (!string.IsNullOrEmpty(str))
        {
          string[] strArray = str.Split('\t');
          this.mImagePairs.Add(new KeyValuePair<string, string>(strArray[0], strArray[1]));
        }
      }
    }

    [Serializable]
    public struct ProgressRatio
    {
      [Range(0.0f, 1f)]
      public float Download;
      [Range(0.0f, 1f)]
      public float Deserilize;

      public ProgressRatio(float a, float b)
      {
        this.Download = a;
        this.Deserilize = b;
      }
    }
  }
}
