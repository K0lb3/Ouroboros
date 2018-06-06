// Decompiled with JetBrains decompiler
// Type: MediaPlayerCtrl
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class MediaPlayerCtrl : MonoBehaviour
{
  public string m_strFileName;
  public GameObject[] m_TargetMaterial;
  private Texture2D m_VideoTexture;
  private Texture2D m_VideoTextureDummy;
  private MediaPlayerCtrl.MEDIAPLAYER_STATE m_CurrentState;
  private int m_iCurrentSeekPosition;
  private float m_fVolume;
  private int m_iWidth;
  private int m_iHeight;
  private float m_fSpeed;
  public bool m_bFullScreen;
  public bool m_bSupportRockchip;
  public MediaPlayerCtrl.VideoResize OnResize;
  public MediaPlayerCtrl.VideoReady OnReady;
  public MediaPlayerCtrl.VideoEnd OnEnd;
  public MediaPlayerCtrl.VideoError OnVideoError;
  public MediaPlayerCtrl.VideoFirstFrameReady OnVideoFirstFrameReady;
  private IntPtr m_texPtr;
  private int m_iAndroidMgrID;
  private bool m_bIsFirstFrameReady;
  private bool m_bFirst;
  public MediaPlayerCtrl.MEDIA_SCALE m_ScaleValue;
  public GameObject[] m_objResize;
  public bool m_bLoop;
  public bool m_bAutoPlay;
  private bool m_bStop;
  private bool m_bInit;
  private bool m_bCheckFBO;
  private bool m_bPause;
  private bool m_bReadyPlay;
  private AndroidJavaObject javaObj;
  private List<Action> unityMainThreadActionList;
  private bool checkNewActions;
  private object thisLock;

  public MediaPlayerCtrl()
  {
    base.\u002Ector();
  }

  [DllImport("BlueDoveMediaRender")]
  private static extern void InitNDK();

  private void Awake()
  {
    this.m_bSupportRockchip = SystemInfo.get_deviceModel().Contains("rockchip");
    if (SystemInfo.get_graphicsMultiThreaded())
      MediaPlayerCtrl.InitNDK();
    this.m_iAndroidMgrID = this.Call_InitNDK();
    this.Call_SetUnityActivity();
  }

  private void Start()
  {
    if (Application.get_dataPath().Contains(".obb"))
      this.Call_SetSplitOBB(true, Application.get_dataPath());
    else
      this.Call_SetSplitOBB(false, (string) null);
    this.m_bInit = true;
  }

  private void OnApplicationQuit()
  {
  }

  private void OnDisable()
  {
    if (this.GetCurrentState() != MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING)
      return;
    this.Pause();
  }

  private void OnEnable()
  {
    if (this.GetCurrentState() != MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED)
      return;
    this.Play();
  }

  private void Update()
  {
    if (string.IsNullOrEmpty(this.m_strFileName))
      return;
    if (this.checkNewActions)
    {
      this.checkNewActions = false;
      this.CheckThreading();
    }
    if (!this.m_bFirst)
    {
      string str = this.m_strFileName.Trim();
      if (this.m_bSupportRockchip)
      {
        this.Call_SetRockchip(this.m_bSupportRockchip);
        if (str.Contains("://"))
          this.Call_Load(str, 0);
        else
          this.StartCoroutine(this.CopyStreamingAssetVideoAndLoad(str));
      }
      else
        this.Call_Load(str, 0);
      this.Call_SetLooping(this.m_bLoop);
      this.m_bFirst = true;
    }
    if (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING || this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED)
    {
      if (!this.m_bCheckFBO)
      {
        if (this.Call_GetVideoWidth() <= 0 || this.Call_GetVideoHeight() <= 0)
          return;
        this.m_iWidth = this.Call_GetVideoWidth();
        this.m_iHeight = this.Call_GetVideoHeight();
        this.Resize();
        if (Object.op_Inequality((Object) this.m_VideoTexture, (Object) null))
        {
          if (Object.op_Inequality((Object) this.m_VideoTextureDummy, (Object) null))
          {
            Object.Destroy((Object) this.m_VideoTextureDummy);
            this.m_VideoTextureDummy = (Texture2D) null;
          }
          this.m_VideoTextureDummy = this.m_VideoTexture;
          this.m_VideoTexture = (Texture2D) null;
        }
        this.m_VideoTexture = !this.m_bSupportRockchip ? new Texture2D(this.Call_GetVideoWidth(), this.Call_GetVideoHeight(), (TextureFormat) 4, false) : new Texture2D(this.Call_GetVideoWidth(), this.Call_GetVideoHeight(), (TextureFormat) 7, false);
        ((Texture) this.m_VideoTexture).set_filterMode((FilterMode) 1);
        ((Texture) this.m_VideoTexture).set_wrapMode((TextureWrapMode) 1);
        this.m_texPtr = ((Texture) this.m_VideoTexture).GetNativeTexturePtr();
        this.Call_SetUnityTexture((int) this.m_texPtr);
        this.Call_SetWindowSize();
        this.m_bCheckFBO = true;
        if (this.OnResize != null)
          this.OnResize();
      }
      else if (this.Call_GetVideoWidth() != this.m_iWidth || this.Call_GetVideoHeight() != this.m_iHeight)
      {
        this.m_iWidth = this.Call_GetVideoWidth();
        this.m_iHeight = this.Call_GetVideoHeight();
        if (this.OnResize != null)
          this.OnResize();
        this.ResizeTexture();
      }
      this.Call_UpdateVideoTexture();
      this.m_iCurrentSeekPosition = this.Call_GetSeekPosition();
    }
    if (this.m_CurrentState != this.Call_GetStatus())
    {
      this.m_CurrentState = this.Call_GetStatus();
      if (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.READY)
      {
        if (this.OnReady != null)
          this.OnReady();
        if (this.m_bAutoPlay)
          this.Call_Play(0);
        if (this.m_bReadyPlay)
        {
          this.Call_Play(0);
          this.m_bReadyPlay = false;
        }
        this.SetVolume(this.m_fVolume);
      }
      else if (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.END)
      {
        if (this.OnEnd != null)
          this.OnEnd();
        if (this.m_bLoop)
          this.Call_Play(0);
      }
      else if (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.ERROR)
        this.OnError((MediaPlayerCtrl.MEDIAPLAYER_ERROR) this.Call_GetError(), (MediaPlayerCtrl.MEDIAPLAYER_ERROR) this.Call_GetErrorExtra());
    }
    GL.InvalidateState();
  }

  public void DeleteVideoTexture()
  {
    if (Object.op_Inequality((Object) this.m_VideoTextureDummy, (Object) null))
    {
      Object.Destroy((Object) this.m_VideoTextureDummy);
      this.m_VideoTextureDummy = (Texture2D) null;
    }
    if (!Object.op_Inequality((Object) this.m_VideoTexture, (Object) null))
      return;
    Object.Destroy((Object) this.m_VideoTexture);
    this.m_VideoTexture = (Texture2D) null;
  }

  public void ResizeTexture()
  {
    Debug.Log((object) ("ResizeTexture " + (object) this.m_iWidth + " " + (object) this.m_iHeight));
    if (this.m_iWidth == 0 || this.m_iHeight == 0)
      return;
    if (Object.op_Inequality((Object) this.m_VideoTexture, (Object) null))
    {
      if (Object.op_Inequality((Object) this.m_VideoTextureDummy, (Object) null))
      {
        Object.Destroy((Object) this.m_VideoTextureDummy);
        this.m_VideoTextureDummy = (Texture2D) null;
      }
      this.m_VideoTextureDummy = this.m_VideoTexture;
      this.m_VideoTexture = (Texture2D) null;
    }
    this.m_VideoTexture = !this.m_bSupportRockchip ? new Texture2D(this.Call_GetVideoWidth(), this.Call_GetVideoHeight(), (TextureFormat) 4, false) : new Texture2D(this.Call_GetVideoWidth(), this.Call_GetVideoHeight(), (TextureFormat) 7, false);
    ((Texture) this.m_VideoTexture).set_filterMode((FilterMode) 1);
    ((Texture) this.m_VideoTexture).set_wrapMode((TextureWrapMode) 1);
    this.m_texPtr = ((Texture) this.m_VideoTexture).GetNativeTexturePtr();
    this.Call_SetUnityTexture((int) this.m_texPtr);
    this.Call_SetWindowSize();
  }

  public void Resize()
  {
    if (this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING || this.Call_GetVideoWidth() <= 0 || (this.Call_GetVideoHeight() <= 0 || this.m_objResize == null))
      return;
    float num1 = (float) Screen.get_height() / (float) Screen.get_width();
    float num2 = (float) this.Call_GetVideoHeight() / (float) this.Call_GetVideoWidth();
    float num3 = num1 / num2;
    for (int index = 0; index < this.m_objResize.Length; ++index)
    {
      if (!Object.op_Equality((Object) this.m_objResize[index], (Object) null))
      {
        if (this.m_bFullScreen)
        {
          this.m_objResize[index].get_transform().set_localScale(new Vector3(20f / num1, 20f / num1, 1f));
          if ((double) num2 < 1.0)
          {
            if ((double) num1 < 1.0 && (double) num2 > (double) num1)
            {
              Transform transform = this.m_objResize[index].get_transform();
              transform.set_localScale(Vector3.op_Multiply(transform.get_localScale(), num3));
            }
            this.m_ScaleValue = MediaPlayerCtrl.MEDIA_SCALE.SCALE_X_TO_Y;
          }
          else
          {
            if ((double) num1 > 1.0)
            {
              if ((double) num2 >= (double) num1)
              {
                Transform transform = this.m_objResize[index].get_transform();
                transform.set_localScale(Vector3.op_Multiply(transform.get_localScale(), num3));
              }
            }
            else
            {
              Transform transform = this.m_objResize[index].get_transform();
              transform.set_localScale(Vector3.op_Multiply(transform.get_localScale(), num3));
            }
            this.m_ScaleValue = MediaPlayerCtrl.MEDIA_SCALE.SCALE_X_TO_Y;
          }
        }
        if (this.m_ScaleValue == MediaPlayerCtrl.MEDIA_SCALE.SCALE_X_TO_Y)
          this.m_objResize[index].get_transform().set_localScale(new Vector3((float) this.m_objResize[index].get_transform().get_localScale().x, (float) this.m_objResize[index].get_transform().get_localScale().x * num2, (float) this.m_objResize[index].get_transform().get_localScale().z));
        else if (this.m_ScaleValue == MediaPlayerCtrl.MEDIA_SCALE.SCALE_X_TO_Y_2)
          this.m_objResize[index].get_transform().set_localScale(new Vector3((float) this.m_objResize[index].get_transform().get_localScale().x, (float) (this.m_objResize[index].get_transform().get_localScale().x * (double) num2 / 2.0), (float) this.m_objResize[index].get_transform().get_localScale().z));
        else if (this.m_ScaleValue == MediaPlayerCtrl.MEDIA_SCALE.SCALE_X_TO_Z)
          this.m_objResize[index].get_transform().set_localScale(new Vector3((float) this.m_objResize[index].get_transform().get_localScale().x, (float) this.m_objResize[index].get_transform().get_localScale().y, (float) this.m_objResize[index].get_transform().get_localScale().x * num2));
        else if (this.m_ScaleValue == MediaPlayerCtrl.MEDIA_SCALE.SCALE_Y_TO_X)
          this.m_objResize[index].get_transform().set_localScale(new Vector3((float) this.m_objResize[index].get_transform().get_localScale().y / num2, (float) this.m_objResize[index].get_transform().get_localScale().y, (float) this.m_objResize[index].get_transform().get_localScale().z));
        else if (this.m_ScaleValue == MediaPlayerCtrl.MEDIA_SCALE.SCALE_Y_TO_Z)
          this.m_objResize[index].get_transform().set_localScale(new Vector3((float) this.m_objResize[index].get_transform().get_localScale().x, (float) this.m_objResize[index].get_transform().get_localScale().y, (float) this.m_objResize[index].get_transform().get_localScale().y / num2));
        else if (this.m_ScaleValue == MediaPlayerCtrl.MEDIA_SCALE.SCALE_Z_TO_X)
          this.m_objResize[index].get_transform().set_localScale(new Vector3((float) this.m_objResize[index].get_transform().get_localScale().z * num2, (float) this.m_objResize[index].get_transform().get_localScale().y, (float) this.m_objResize[index].get_transform().get_localScale().z));
        else if (this.m_ScaleValue == MediaPlayerCtrl.MEDIA_SCALE.SCALE_Z_TO_Y)
          this.m_objResize[index].get_transform().set_localScale(new Vector3((float) this.m_objResize[index].get_transform().get_localScale().x, (float) this.m_objResize[index].get_transform().get_localScale().z * num2, (float) this.m_objResize[index].get_transform().get_localScale().z));
        else
          this.m_objResize[index].get_transform().set_localScale(new Vector3((float) this.m_objResize[index].get_transform().get_localScale().x, (float) this.m_objResize[index].get_transform().get_localScale().y, (float) this.m_objResize[index].get_transform().get_localScale().z));
      }
    }
  }

  private void OnError(MediaPlayerCtrl.MEDIAPLAYER_ERROR iCode, MediaPlayerCtrl.MEDIAPLAYER_ERROR iCodeExtra)
  {
    string empty = string.Empty;
    string str1;
    switch (iCode)
    {
      case MediaPlayerCtrl.MEDIAPLAYER_ERROR.MEDIA_ERROR_UNKNOWN:
        str1 = "MEDIA_ERROR_UNKNOWN";
        break;
      case MediaPlayerCtrl.MEDIAPLAYER_ERROR.MEDIA_ERROR_SERVER_DIED:
        str1 = "MEDIA_ERROR_SERVER_DIED";
        break;
      case MediaPlayerCtrl.MEDIAPLAYER_ERROR.MEDIA_ERROR_NOT_VALID_FOR_PROGRESSIVE_PLAYBACK:
        str1 = "MEDIA_ERROR_NOT_VALID_FOR_PROGRESSIVE_PLAYBACK";
        break;
      default:
        str1 = "Unknown error " + (object) iCode;
        break;
    }
    string str2 = str1 + " ";
    MediaPlayerCtrl.MEDIAPLAYER_ERROR mediaplayerError = iCodeExtra;
    string str3;
    switch (mediaplayerError + 1010)
    {
      case (MediaPlayerCtrl.MEDIAPLAYER_ERROR) 0:
        str3 = str2 + "MEDIA_ERROR_UNSUPPORTED";
        break;
      case (MediaPlayerCtrl.MEDIAPLAYER_ERROR) 3:
        str3 = str2 + "MEDIA_ERROR_MALFORMED";
        break;
      default:
        switch (mediaplayerError)
        {
          case MediaPlayerCtrl.MEDIAPLAYER_ERROR.MEDIA_ERROR_IO:
            str3 = str2 + "MEDIA_ERROR_IO";
            break;
          case MediaPlayerCtrl.MEDIAPLAYER_ERROR.MEDIA_ERROR_TIMED_OUT:
            str3 = str2 + "MEDIA_ERROR_TIMED_OUT";
            break;
          default:
            str3 = "Unknown error " + (object) iCode;
            break;
        }
    }
    Debug.LogError((object) str3);
    if (this.OnVideoError == null)
      return;
    this.OnVideoError(iCode, iCodeExtra);
  }

  private void OnDestroy()
  {
    this.Call_UnLoad();
    if (Object.op_Inequality((Object) this.m_VideoTextureDummy, (Object) null))
    {
      Object.Destroy((Object) this.m_VideoTextureDummy);
      this.m_VideoTextureDummy = (Texture2D) null;
    }
    if (Object.op_Inequality((Object) this.m_VideoTexture, (Object) null))
      Object.Destroy((Object) this.m_VideoTexture);
    this.Call_Destroy();
  }

  private void OnApplicationPause(bool bPause)
  {
    Debug.Log((object) ("ApplicationPause : " + (object) bPause));
    if (bPause)
    {
      if (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED)
        this.m_bPause = true;
      this.Call_Pause();
    }
    else
    {
      this.Call_RePlay();
      if (!this.m_bPause)
        return;
      this.Call_Pause();
      this.m_bPause = false;
    }
  }

  public MediaPlayerCtrl.MEDIAPLAYER_STATE GetCurrentState()
  {
    return this.m_CurrentState;
  }

  public Texture2D GetVideoTexture()
  {
    return this.m_VideoTexture;
  }

  public void Play()
  {
    if (this.m_bStop)
    {
      this.SeekTo(0);
      this.Call_Play(0);
      this.m_bStop = false;
    }
    if (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED)
      this.Call_RePlay();
    else if (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.READY || this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.STOPPED || this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.END)
    {
      this.Call_Play(0);
    }
    else
    {
      if (this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.NOT_READY)
        return;
      this.m_bReadyPlay = true;
    }
  }

  public void Stop()
  {
    if (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING)
      this.Call_Pause();
    this.m_bStop = true;
    this.m_CurrentState = MediaPlayerCtrl.MEDIAPLAYER_STATE.STOPPED;
    this.m_iCurrentSeekPosition = 0;
  }

  public void Pause()
  {
    if (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING)
      this.Call_Pause();
    this.m_CurrentState = MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED;
  }

  public void Load(string strFileName)
  {
    if (this.GetCurrentState() != MediaPlayerCtrl.MEDIAPLAYER_STATE.NOT_READY)
      this.UnLoad();
    this.m_bReadyPlay = false;
    this.m_bIsFirstFrameReady = false;
    this.m_bFirst = false;
    this.m_bCheckFBO = false;
    this.m_strFileName = strFileName;
    if (!this.m_bInit)
      return;
    this.m_CurrentState = MediaPlayerCtrl.MEDIAPLAYER_STATE.NOT_READY;
  }

  public void SetVolume(float fVolume)
  {
    if (this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING && this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED && (this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.END && this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.READY) && this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.STOPPED)
      return;
    this.m_fVolume = fVolume;
    this.Call_SetVolume(fVolume);
  }

  public int GetSeekPosition()
  {
    if (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING || this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED || this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.END)
      return this.m_iCurrentSeekPosition;
    return 0;
  }

  public void SeekTo(int iSeek)
  {
    if (this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING && this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.READY && (this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED && this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.END) && this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.STOPPED)
      return;
    this.Call_SetSeekPosition(iSeek);
  }

  public void SetSpeed(float fSpeed)
  {
    if (this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING && this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.READY && (this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED && this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.END) && this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.STOPPED)
      return;
    this.m_fSpeed = fSpeed;
    this.Call_SetSpeed(fSpeed);
  }

  public int GetDuration()
  {
    if (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING || this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED || (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.END || this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.READY) || this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.STOPPED)
      return this.Call_GetDuration();
    return 0;
  }

  public float GetSeekBarValue()
  {
    if ((this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING || this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED || (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.END || this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.READY) || this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.STOPPED) && this.GetDuration() != 0)
      return (float) this.GetSeekPosition() / (float) this.GetDuration();
    return 0.0f;
  }

  public void SetSeekBarValue(float fValue)
  {
    if (this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING && this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED && (this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.END && this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.READY) && this.m_CurrentState != MediaPlayerCtrl.MEDIAPLAYER_STATE.STOPPED || this.GetDuration() == 0)
      return;
    this.SeekTo((int) ((double) this.GetDuration() * (double) fValue));
  }

  public int GetCurrentSeekPercent()
  {
    if (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING || this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED || (this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.END || this.m_CurrentState == MediaPlayerCtrl.MEDIAPLAYER_STATE.READY))
      return this.Call_GetCurrentSeekPercent();
    return 0;
  }

  public int GetVideoWidth()
  {
    return this.Call_GetVideoWidth();
  }

  public int GetVideoHeight()
  {
    return this.Call_GetVideoHeight();
  }

  public void UnLoad()
  {
    this.m_bCheckFBO = false;
    this.Call_UnLoad();
    this.m_CurrentState = MediaPlayerCtrl.MEDIAPLAYER_STATE.NOT_READY;
  }

  private AndroidJavaObject GetJavaObject()
  {
    if (this.javaObj == null)
      this.javaObj = new AndroidJavaObject("com.EasyMovieTexture.EasyMovieTexture", new object[0]);
    return this.javaObj;
  }

  private void Call_Destroy()
  {
    if (SystemInfo.get_graphicsMultiThreaded())
      GL.IssuePluginEvent(5 + this.m_iAndroidMgrID * 10 + 7000);
    else
      this.GetJavaObject().Call("Destroy", new object[0]);
  }

  private void Call_UnLoad()
  {
    if (SystemInfo.get_graphicsMultiThreaded())
      GL.IssuePluginEvent(4 + this.m_iAndroidMgrID * 10 + 7000);
    else
      this.GetJavaObject().Call("UnLoad", new object[0]);
  }

  private bool Call_Load(string strFileName, int iSeek)
  {
    if (SystemInfo.get_graphicsMultiThreaded())
    {
      this.GetJavaObject().Call("NDK_SetFileName", new object[1]
      {
        (object) strFileName
      });
      GL.IssuePluginEvent(1 + this.m_iAndroidMgrID * 10 + 7000);
      this.Call_SetNotReady();
      return true;
    }
    this.GetJavaObject().Call("NDK_SetFileName", new object[1]
    {
      (object) strFileName
    });
    if (this.GetJavaObject().Call<bool>("Load", new object[0]) != null)
      return true;
    this.OnError(MediaPlayerCtrl.MEDIAPLAYER_ERROR.MEDIA_ERROR_UNKNOWN, MediaPlayerCtrl.MEDIAPLAYER_ERROR.MEDIA_ERROR_UNKNOWN);
    return false;
  }

  private void Call_UpdateVideoTexture()
  {
    if (!this.Call_IsUpdateFrame())
      return;
    if (Object.op_Inequality((Object) this.m_VideoTextureDummy, (Object) null))
    {
      Object.Destroy((Object) this.m_VideoTextureDummy);
      this.m_VideoTextureDummy = (Texture2D) null;
    }
    for (int index = 0; index < this.m_TargetMaterial.Length; ++index)
    {
      if (Object.op_Implicit((Object) this.m_TargetMaterial[index]))
      {
        if (Object.op_Inequality((Object) this.m_TargetMaterial[index].GetComponent<MeshRenderer>(), (Object) null) && Object.op_Inequality((Object) ((Renderer) this.m_TargetMaterial[index].GetComponent<MeshRenderer>()).get_material().get_mainTexture(), (Object) this.m_VideoTexture))
          ((Renderer) this.m_TargetMaterial[index].GetComponent<MeshRenderer>()).get_material().set_mainTexture((Texture) this.m_VideoTexture);
        if (Object.op_Inequality((Object) this.m_TargetMaterial[index].GetComponent<RawImage>(), (Object) null) && Object.op_Inequality((Object) ((RawImage) this.m_TargetMaterial[index].GetComponent<RawImage>()).get_texture(), (Object) this.m_VideoTexture))
          ((RawImage) this.m_TargetMaterial[index].GetComponent<RawImage>()).set_texture((Texture) this.m_VideoTexture);
      }
    }
    if (SystemInfo.get_graphicsMultiThreaded())
      GL.IssuePluginEvent(3 + this.m_iAndroidMgrID * 10 + 7000);
    else
      this.GetJavaObject().Call("UpdateVideoTexture", new object[0]);
    if (this.m_bIsFirstFrameReady)
      return;
    this.m_bIsFirstFrameReady = true;
    if (this.OnVideoFirstFrameReady == null)
      return;
    this.OnVideoFirstFrameReady();
  }

  private void Call_SetVolume(float fVolume)
  {
    this.GetJavaObject().Call("SetVolume", new object[1]
    {
      (object) fVolume
    });
  }

  private void Call_SetSeekPosition(int iSeek)
  {
    this.GetJavaObject().Call("SetSeekPosition", new object[1]
    {
      (object) iSeek
    });
  }

  private int Call_GetSeekPosition()
  {
    return (int) this.GetJavaObject().Call<int>("GetSeekPosition", new object[0]);
  }

  private void Call_Play(int iSeek)
  {
    this.GetJavaObject().Call("Play", new object[1]
    {
      (object) iSeek
    });
  }

  private void Call_Reset()
  {
    this.GetJavaObject().Call("Reset", new object[0]);
  }

  private void Call_Stop()
  {
    this.GetJavaObject().Call("Stop", new object[0]);
  }

  private void Call_RePlay()
  {
    this.GetJavaObject().Call("RePlay", new object[0]);
  }

  private void Call_Pause()
  {
    this.GetJavaObject().Call("Pause", new object[0]);
  }

  private int Call_InitNDK()
  {
    return (int) this.GetJavaObject().Call<int>("InitNative", new object[1]{ (object) this.GetJavaObject() });
  }

  private int Call_GetVideoWidth()
  {
    return (int) this.GetJavaObject().Call<int>("GetVideoWidth", new object[0]);
  }

  private int Call_GetVideoHeight()
  {
    return (int) this.GetJavaObject().Call<int>("GetVideoHeight", new object[0]);
  }

  private bool Call_IsUpdateFrame()
  {
    return (bool) this.GetJavaObject().Call<bool>("IsUpdateFrame", new object[0]);
  }

  private void Call_SetUnityTexture(int iTextureID)
  {
    this.GetJavaObject().Call("SetUnityTexture", new object[1]
    {
      (object) iTextureID
    });
  }

  private void Call_SetWindowSize()
  {
    if (SystemInfo.get_graphicsMultiThreaded())
      GL.IssuePluginEvent(2 + this.m_iAndroidMgrID * 10 + 7000);
    else
      this.GetJavaObject().Call("SetWindowSize", new object[0]);
  }

  private void Call_SetLooping(bool bLoop)
  {
    this.GetJavaObject().Call("SetLooping", new object[1]
    {
      (object) bLoop
    });
  }

  private void Call_SetRockchip(bool bValue)
  {
    this.GetJavaObject().Call("SetRockchip", new object[1]
    {
      (object) bValue
    });
  }

  private int Call_GetDuration()
  {
    return (int) this.GetJavaObject().Call<int>("GetDuration", new object[0]);
  }

  private int Call_GetCurrentSeekPercent()
  {
    return (int) this.GetJavaObject().Call<int>("GetCurrentSeekPercent", new object[0]);
  }

  private int Call_GetError()
  {
    return (int) this.GetJavaObject().Call<int>("GetError", new object[0]);
  }

  private void Call_SetSplitOBB(bool bValue, string strOBBName)
  {
    this.GetJavaObject().Call("SetSplitOBB", new object[2]
    {
      (object) bValue,
      (object) strOBBName
    });
  }

  private int Call_GetErrorExtra()
  {
    return (int) this.GetJavaObject().Call<int>("GetErrorExtra", new object[0]);
  }

  private void Call_SetUnityActivity()
  {
    this.GetJavaObject().Call("SetUnityActivity", new object[1]
    {
      (object) (AndroidJavaObject) ((AndroidJavaObject) new AndroidJavaClass("com.unity3d.player.UnityPlayer")).GetStatic<AndroidJavaObject>("currentActivity")
    });
    if (SystemInfo.get_graphicsMultiThreaded())
      GL.IssuePluginEvent(0 + this.m_iAndroidMgrID * 10 + 7000);
    else
      this.Call_InitJniManager();
  }

  private void Call_SetNotReady()
  {
    this.GetJavaObject().Call("SetNotReady", new object[0]);
  }

  private void Call_InitJniManager()
  {
    this.GetJavaObject().Call("InitJniManager", new object[0]);
  }

  private void Call_SetSpeed(float fSpeed)
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Build$VERSION"))
    {
      if ((int) ((AndroidJavaObject) androidJavaClass).GetStatic<int>("SDK_INT") < 23)
        return;
      this.GetJavaObject().Call("SetSpeed", new object[1]
      {
        (object) fSpeed
      });
    }
  }

  private MediaPlayerCtrl.MEDIAPLAYER_STATE Call_GetStatus()
  {
    return (MediaPlayerCtrl.MEDIAPLAYER_STATE) this.GetJavaObject().Call<int>("GetStatus", new object[0]);
  }

  [DebuggerHidden]
  public IEnumerator DownloadStreamingVideoAndLoad(string strURL)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new MediaPlayerCtrl.\u003CDownloadStreamingVideoAndLoad\u003Ec__Iterator2() { strURL = strURL, \u003C\u0024\u003EstrURL = strURL, \u003C\u003Ef__this = this };
  }

  [DebuggerHidden]
  public IEnumerator DownloadStreamingVideoAndLoad2(string strURL)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new MediaPlayerCtrl.\u003CDownloadStreamingVideoAndLoad2\u003Ec__Iterator3() { strURL = strURL, \u003C\u0024\u003EstrURL = strURL, \u003C\u003Ef__this = this };
  }

  [DebuggerHidden]
  private IEnumerator CopyStreamingAssetVideoAndLoad(string strURL)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new MediaPlayerCtrl.\u003CCopyStreamingAssetVideoAndLoad\u003Ec__Iterator4() { strURL = strURL, \u003C\u0024\u003EstrURL = strURL, \u003C\u003Ef__this = this };
  }

  private void CheckThreading()
  {
    lock (this.thisLock)
    {
      if (this.unityMainThreadActionList.Count <= 0)
        return;
      using (List<Action>.Enumerator enumerator = this.unityMainThreadActionList.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current();
      }
      this.unityMainThreadActionList.Clear();
    }
  }

  private void AddActionForUnityMainThread(Action a)
  {
    lock (this.thisLock)
      this.unityMainThreadActionList.Add(a);
    this.checkNewActions = true;
  }

  public enum MEDIAPLAYER_ERROR
  {
    MEDIA_ERROR_UNSUPPORTED = -1010, // -0x000003F2
    MEDIA_ERROR_MALFORMED = -1007, // -0x000003EF
    MEDIA_ERROR_IO = -1004, // -0x000003EC
    MEDIA_ERROR_TIMED_OUT = -110, // -0x0000006E
    MEDIA_ERROR_UNKNOWN = 1,
    MEDIA_ERROR_SERVER_DIED = 100, // 0x00000064
    MEDIA_ERROR_NOT_VALID_FOR_PROGRESSIVE_PLAYBACK = 200, // 0x000000C8
  }

  public enum MEDIAPLAYER_STATE
  {
    NOT_READY,
    READY,
    END,
    PLAYING,
    PAUSED,
    STOPPED,
    ERROR,
  }

  public enum MEDIA_SCALE
  {
    SCALE_X_TO_Y,
    SCALE_X_TO_Z,
    SCALE_Y_TO_X,
    SCALE_Y_TO_Z,
    SCALE_Z_TO_X,
    SCALE_Z_TO_Y,
    SCALE_X_TO_Y_2,
  }

  public delegate void VideoEnd();

  public delegate void VideoReady();

  public delegate void VideoError(MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCode, MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCodeExtra);

  public delegate void VideoFirstFrameReady();

  public delegate void VideoResize();
}
