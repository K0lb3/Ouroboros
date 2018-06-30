namespace SRPG
{
    using System;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Experimental.Networking;

    public class DownloadLogger : DownloadHandlerScript
    {
        private VersusAudienceManager mManager;
        private string mParam;

        public DownloadLogger()
        {
            this.mParam = string.Empty;
            base..ctor();
            return;
        }

        protected override void CompleteContent()
        {
            if (this.mManager == null)
            {
                goto Label_0016;
            }
            this.mManager.Disconnect();
        Label_0016:
            Debug.Log(this.mParam);
            return;
        }

        protected override bool ReceiveData(byte[] data, int dataLength)
        {
            char[] chArray2;
            char[] chArray1;
            string str;
            string[] strArray;
            int num;
            string[] strArray2;
            int num2;
            if (data == null)
            {
                goto Label_000F;
            }
            if (((int) data.Length) >= 1)
            {
                goto Label_001B;
            }
        Label_000F:
            Debug.Log("LoggingDownloadHandler :: ReceiveData - received a null/empty buffer");
            return 0;
        Label_001B:
            str = Encoding.UTF8.GetString(data).Replace("\r\n", string.Empty).Replace("\x0005", string.Empty);
            chArray1 = new char[] { 10 };
            strArray = str.Split(chArray1);
            if (this.mManager == null)
            {
                goto Label_00C2;
            }
            num = 0;
            goto Label_00B9;
        Label_006D:
            if (string.IsNullOrEmpty(strArray[num]) != null)
            {
                goto Label_00B5;
            }
            chArray2 = new char[] { 13 };
            strArray2 = strArray[num].Split(chArray2);
            num2 = 0;
            goto Label_00AB;
        Label_0096:
            this.mManager.Add(strArray2[num2]);
            num2 += 1;
        Label_00AB:
            if (num2 < ((int) strArray2.Length))
            {
                goto Label_0096;
            }
        Label_00B5:
            num += 1;
        Label_00B9:
            if (num < ((int) strArray.Length))
            {
                goto Label_006D;
            }
        Label_00C2:
            this.mParam = str;
            return 1;
        }

        public VersusAudienceManager Manager
        {
            set
            {
                this.mManager = value;
                return;
            }
        }

        public string Response
        {
            get
            {
                return this.mParam;
            }
        }
    }
}

