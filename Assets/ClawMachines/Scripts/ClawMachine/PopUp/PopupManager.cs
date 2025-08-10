using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NTPackage.Functions;
using System;

namespace NTPackage.UI
{
    public class PopupManager : MonoBehaviour
    {
        public float currentLvUI = 0;
        public NTDictionary<PopupCode, PopupUI> PopupDic = new NTDictionary<PopupCode, PopupUI>();

        public static PopupManager Instance;
        private void Awake()
        {
            //base.Awake();
            if (Instance != null) Debug.LogWarning("Only 1 UIManager allow");
            Instance = this;
            this.PopupDic = new NTDictionary<PopupCode, PopupUI>();
        }

        //public override void LoadComponents()
        //{
        //    base.LoadComponents();
        //}
        private void Start()
        {
            //base.Start();
            this.OffAllPopupUI();
        }

        public virtual void OffAllPopupUI()
        {
            foreach (PopupUI popupUI in this.PopupDic.ToList())
            {
                popupUI.ActionOffUI = null;
                popupUI.OffUI();
            }
        }

        public PopupUI GetPopupUIByCode(PopupCode popupCode)
        {
            return this.PopupDic.Get(popupCode);
        }

        public void OnUI(PopupCode popupCode, object data = null, Action<PopupUI> action = null)
        {
            try
            {
                //NTPackage.Functions.NTLog.LogMessage("OnUI:" + popupCode.ToString(), gameObject);
                PopupUI popupUI = this.GetPopupUIByCode(popupCode);
                popupUI.OnUI(data);
                action?.Invoke(popupUI);
            }
            catch (Exception e)
            {
                //NTPackage.Functions.NTLog.LogError(popupCode+":"+e.ToString(), gameObject)
                //;
                Debug.LogError(e);
            }
        }
        public void OffUI(PopupCode popupCode)
        {
            try
            {
                //NTPackage.Functions.NTLog.LogMessage("OffUI:" + popupCode.ToString(), gameObject);
                this.GetPopupUIByCode(popupCode).OffUI();
            }
            catch (System.Exception e)
            {
                //NTPackage.Functions.NTLog.LogError(popupCode+":"+e.ToString(), gameObject);
                Debug.LogError(e);
            }
        }
        public void UpdateDataUI(PopupCode popupCode, object data = null)
        {
            try
            {
                //NTPackage.Functions.NTLog.LogMessage("UpdateData:" + popupCode.ToString(), gameObject);
                this.GetPopupUIByCode(popupCode).UpdateData(data);
            }
            catch (System.Exception e)
            {

                //NTPackage.Functions.NTLog.LogError(e.ToString(), gameObject);
                Debug.LogError(e);
            }
        }
    }
}
