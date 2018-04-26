using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;


namespace PASdkLib.src
{
    public delegate void resultFunction(String result);


    public class ConnectManager
    {

        private static ConnectManager inst;
        
        public static ConnectManager getInst()
        {
            if (inst == null)
            {
                inst = new ConnectManager();
                inst.InitSetting();
            }

            return inst;
        }
       


        private Dictionary<string, string> hsHeader;
        private string result;
        public string _result
        {
            get
            {
                return result;
            }
        }

        public void InitSetting()
        {
            hsHeader = new Dictionary<string, string>();
        }
        public IEnumerator SendData(string url, WWWForm form, resultFunction resultFunction)
        {
            WWW www = null;
            byte[] rawData = form.data;
            //Hashtable headers = form.headers;
            //headers["Authorization"] = "Basic " + System.Convert.ToBase64String(
            //System.Text.Encoding.ASCII.GetBytes("username:password"));

            www = new WWW(url, rawData);

            yield return www;
            try
            {
                
               
                //SetSessionCookie(www);
                //Debug.Log("===============================");
                //Debug.Log("URL : " + url);
                //Debug.Log("Response : " + www.text);
                String s = www.text;
                s = s.Replace("null", "\"\"");
                resultFunction(s);
                //Debug.Log("SendData Success ");
            }
            catch (Exception e) {
                //Debug.Log("SendData exception ");
                resultFunction(null);
            }
            
        }
        /*public IEnumerator SendData(string url, WWWForm form, resultFunction resultFunction)
        {
            //WWW www;
            WWW www = null;
            if (this.hsHeader.Count > 0)
            {
                www = new WWW(url, form.data, hsHeader);
            }
            else
            {
                www = new WWW(url, form.data);
            }
            yield return www;

            SetSessionCookie(www);
            result = www.text;

            www.Dispose();

            resultFunction(www.text);
        }*/

        private void SetSessionCookie(WWW www)
        {
            Dictionary<string, string> headers = www.responseHeaders;
            foreach (KeyValuePair<string, string> kvPair in headers)
            {
                if (kvPair.Key.ToLower().Equals("set-cookie"))
                {
                    string stHeader = "";
                    string stJoint = "";
                    string[] astCookie = kvPair.Value.Split(
                        new string[] { ";" }, System.StringSplitOptions.None);
                    foreach (string stCookie in astCookie)
                    {
                        if (!stCookie.Substring(0, 5).Equals("path ="))
                        {
                            stHeader += stJoint + stCookie;
                            stJoint = ";";
                        }
                    }
                    if (stHeader.Length > 0)
                    {
                        this.hsHeader["Cookie"] = stHeader;
                    }
                    else
                    {
                        this.hsHeader.Clear();
                    }
                }
            }
        }
    }



}
