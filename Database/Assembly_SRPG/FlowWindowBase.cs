namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class FlowWindowBase
    {
        private FlowWindowController m_Controller;
        protected GameObject m_Window;
        protected Animator m_Animator;
        protected AnimState m_AnimState;
        protected bool m_StartUp;
        protected List<int> m_Request;
        protected bool m_AnimStart;
        protected int m_FrameCount;
        protected List<IEnumerator> m_TaskReq;
        protected IEnumerator m_Task;

        public FlowWindowBase()
        {
            this.m_Request = new List<int>();
            this.m_TaskReq = new List<IEnumerator>();
            base..ctor();
            return;
        }

        protected void AddTask(IEnumerator enumrator)
        {
            this.m_TaskReq.Add(enumrator);
            return;
        }

        private void ClearTask()
        {
            this.m_TaskReq.Clear();
            this.m_Task = null;
            return;
        }

        public void Close(bool immidiate)
        {
            this.m_AnimState = 2;
            if ((this.m_Window != null) == null)
            {
                goto Label_0057;
            }
            if ((this.m_Animator != null) == null)
            {
                goto Label_0057;
            }
            this.m_Animator.SetBool("close", 1);
            if (immidiate == null)
            {
                goto Label_0057;
            }
            this.m_Animator.Play("closed");
            this.SetActiveChild(0);
        Label_0057:
            return;
        }

        public GameObject GetChild(string name)
        {
            return this.GetChild(this.m_Window, name);
        }

        public GameObject GetChild(GameObject root, string name)
        {
            Transform transform;
            int num;
            Transform transform2;
            if ((root != null) == null)
            {
                goto Label_0069;
            }
            if ((root.get_name() == name) == null)
            {
                goto Label_001F;
            }
            return root;
        Label_001F:
            transform = root.get_transform();
            num = 0;
            goto Label_005D;
        Label_002D:
            transform2 = transform.GetChild(num);
            if ((transform2 != null) == null)
            {
                goto Label_0059;
            }
            if ((transform2.get_name() == name) == null)
            {
                goto Label_0059;
            }
            return transform2.get_gameObject();
        Label_0059:
            num += 1;
        Label_005D:
            if (num < transform.get_childCount())
            {
                goto Label_002D;
            }
        Label_0069:
            return null;
        }

        public GameObject GetChildAll(string name)
        {
            return this.GetChildAll(this.m_Window, name);
        }

        public GameObject GetChildAll(GameObject root, string name)
        {
            Transform transform;
            List<GameObject> list;
            int num;
            Transform transform2;
            int num2;
            GameObject obj2;
            if ((root != null) == null)
            {
                goto Label_00B7;
            }
            if ((root.get_name() == name) == null)
            {
                goto Label_001F;
            }
            return root;
        Label_001F:
            transform = root.get_transform();
            list = new List<GameObject>();
            num = 0;
            goto Label_006F;
        Label_0033:
            transform2 = transform.GetChild(num);
            if ((transform2 != null) == null)
            {
                goto Label_006B;
            }
            if ((transform2.get_name() == name) == null)
            {
                goto Label_005F;
            }
            return transform2.get_gameObject();
        Label_005F:
            list.Add(transform2.get_gameObject());
        Label_006B:
            num += 1;
        Label_006F:
            if (num < transform.get_childCount())
            {
                goto Label_0033;
            }
            num2 = 0;
            goto Label_00AA;
        Label_0083:
            obj2 = this.GetChild(list[num2], name);
            if ((obj2 != null) == null)
            {
                goto Label_00A4;
            }
            return obj2;
        Label_00A4:
            num2 += 1;
        Label_00AA:
            if (num2 < list.Count)
            {
                goto Label_0083;
            }
        Label_00B7:
            return null;
        }

        public T GetChildAllComponent<T>(string name) where T: Component
        {
            GameObject obj2;
            obj2 = this.GetChildAll(name);
            if ((obj2 != null) == null)
            {
                goto Label_001B;
            }
            return obj2.GetComponent<T>();
        Label_001B:
            return (T) null;
        }

        public T GetChildComponent<T>(string name) where T: Component
        {
            GameObject obj2;
            obj2 = this.GetChild(name);
            if ((obj2 != null) == null)
            {
                goto Label_001B;
            }
            return obj2.GetComponent<T>();
        Label_001B:
            return (T) null;
        }

        public T GetChildComponent<T>(GameObject root, string name) where T: Component
        {
            GameObject obj2;
            obj2 = this.GetChild(root, name);
            if ((obj2 != null) == null)
            {
                goto Label_001C;
            }
            return obj2.GetComponent<T>();
        Label_001C:
            return (T) null;
        }

        public virtual void Initialize(SerializeParamBase param)
        {
            this.m_Request.Clear();
            if ((param.window != null) == null)
            {
                goto Label_0045;
            }
            this.m_Window = param.window;
            this.m_Animator = param.window.GetComponent<Animator>();
            this.m_Window.SetActive(1);
        Label_0045:
            return;
        }

        public bool IsStartUp()
        {
            return this.m_StartUp;
        }

        public unsafe bool IsState(string stateName)
        {
            AnimatorStateInfo info;
            if ((this.m_Window == null) != null)
            {
                goto Label_0064;
            }
            if ((this.m_Animator == null) != null)
            {
                goto Label_0064;
            }
            if ((this.m_Animator.get_runtimeAnimatorController() == null) != null)
            {
                goto Label_0064;
            }
            if (this.m_Animator.get_runtimeAnimatorController().get_animationClips() == null)
            {
                goto Label_0064;
            }
            if (((int) this.m_Animator.get_runtimeAnimatorController().get_animationClips().Length) != null)
            {
                goto Label_0066;
            }
        Label_0064:
            return 1;
        Label_0066:
            if (&this.m_Animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) == null)
            {
                goto Label_0082;
            }
            return 1;
        Label_0082:
            return 0;
        }

        public void LateUpdate(FlowNode flowNode)
        {
        }

        public virtual int OnActivate(int pinId)
        {
            return -1;
        }

        protected virtual int OnClosed()
        {
            return -1;
        }

        protected virtual int OnOpened()
        {
            return -1;
        }

        public void Open()
        {
            this.m_AnimState = 1;
            this.m_AnimStart = 1;
            this.m_FrameCount = Time.get_frameCount();
            this.SetActiveChild(1);
            return;
        }

        public virtual void Release()
        {
            this.ClearTask();
            this.m_Request.Clear();
            return;
        }

        public void RequestPin(int pinId)
        {
            this.m_Request.Add(pinId);
            return;
        }

        public void SetActiveChild(bool value)
        {
            this.SetActiveChild(this.m_Window, value);
            return;
        }

        public void SetActiveChild(GameObject root, bool value)
        {
            Transform transform;
            int num;
            Transform transform2;
            if ((root != null) == null)
            {
                goto Label_004A;
            }
            transform = root.get_transform();
            num = 0;
            goto Label_003E;
        Label_001A:
            transform2 = transform.GetChild(num);
            if ((transform2 != null) == null)
            {
                goto Label_003A;
            }
            transform2.get_gameObject().SetActive(value);
        Label_003A:
            num += 1;
        Label_003E:
            if (num < transform.get_childCount())
            {
                goto Label_001A;
            }
        Label_004A:
            return;
        }

        public virtual void Start()
        {
            this.StartUp();
            return;
        }

        public void StartUp()
        {
            this.m_StartUp = 1;
            return;
        }

        public virtual int Update()
        {
            int num;
            int num2;
            if (this.m_AnimState != 1)
            {
                goto Label_008D;
            }
            if (this.m_AnimStart == null)
            {
                goto Label_0057;
            }
            if (this.m_FrameCount == Time.get_frameCount())
            {
                goto Label_0057;
            }
            if ((this.m_Animator != null) == null)
            {
                goto Label_0049;
            }
            this.m_Animator.SetBool("close", 0);
        Label_0049:
            this.m_AnimStart = 0;
            this.m_FrameCount = 0;
        Label_0057:
            if (this.IsState("opened") == null)
            {
                goto Label_00CA;
            }
            num = this.OnOpened();
            if (num == -1)
            {
                goto Label_0081;
            }
            this.m_Controller.ActivateOutputLinks(num);
        Label_0081:
            this.m_AnimState = 3;
            goto Label_00CA;
        Label_008D:
            if (this.m_AnimState != 2)
            {
                goto Label_00CA;
            }
            if (this.IsState("closed") == null)
            {
                goto Label_00CA;
            }
            num2 = this.OnClosed();
            if (num2 == -1)
            {
                goto Label_00C3;
            }
            this.m_Controller.ActivateOutputLinks(num2);
        Label_00C3:
            this.m_AnimState = 4;
        Label_00CA:
            return -1;
        }

        public void Update(FlowWindowController controller)
        {
            int num;
            int num2;
            int num3;
            this.m_Controller = controller;
            if (this.UpdateTask() == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            if (this.m_Request.Count <= 0)
            {
                goto Label_006C;
            }
            num = 0;
            goto Label_0050;
        Label_002B:
            num2 = this.OnActivate(this.m_Request[num]);
            if (num2 == -1)
            {
                goto Label_004C;
            }
            controller.ActivateOutputLinks(num2);
        Label_004C:
            num += 1;
        Label_0050:
            if (num < this.m_Request.Count)
            {
                goto Label_002B;
            }
            this.m_Request.Clear();
        Label_006C:
            num3 = this.Update();
            if (num3 == -1)
            {
                goto Label_0081;
            }
            controller.ActivateOutputLinks(num3);
        Label_0081:
            this.m_Controller = null;
            return;
        }

        public bool UpdateTask()
        {
            int num;
            IEnumerator enumerator;
            bool flag;
            if (this.m_TaskReq.Count != null)
            {
                goto Label_0012;
            }
            return 0;
        Label_0012:
            num = 0;
            goto Label_0059;
        Label_0019:
            enumerator = this.m_TaskReq[num];
            flag = 0;
            if (enumerator != null)
            {
                goto Label_0035;
            }
            flag = 1;
            goto Label_003F;
        Label_0035:
            flag = enumerator.MoveNext() == 0;
        Label_003F:
            if (flag == null)
            {
                goto Label_0055;
            }
            this.m_TaskReq.RemoveAt(num);
            num -= 1;
        Label_0055:
            num += 1;
        Label_0059:
            if (num < this.m_TaskReq.Count)
            {
                goto Label_0019;
            }
            return 1;
        }

        public virtual string name
        {
            get
            {
                return "FriendPresentWindowBase";
            }
        }

        public bool isValid
        {
            get
            {
                return (this.m_Window != null);
            }
        }

        public bool isOpen
        {
            get
            {
                return (this.m_AnimState == 1);
            }
        }

        public bool isClose
        {
            get
            {
                return (this.m_AnimState == 2);
            }
        }

        public bool isOpened
        {
            get
            {
                return (this.m_AnimState == 3);
            }
        }

        public bool isClosed
        {
            get
            {
                return (this.m_AnimState == 4);
            }
        }

        public enum AnimState
        {
            IDEL,
            OPEN,
            CLOSE,
            OPENED,
            CLOSED
        }

        [Serializable]
        public class SerializeParamBase
        {
            public GameObject window;

            public SerializeParamBase()
            {
                base..ctor();
                return;
            }

            public virtual Type type
            {
                get
                {
                    return typeof(FlowWindowBase);
                }
            }
        }
    }
}

