using NTPackage.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.math
{
    public class AvarPanel : PopupUI
    {
        public List<Avar> listsAva;

        public override void OnUI(object data = null)
        {
            base.OnUI(data);
           // this.SetAvar();

        }
        public void ChangeAvar()
        {
            SoundController.instance.AudioButton();
            //DataController.instance.SaveData();
            UserManager.instance.SaveData();
            PopupManager.Instance.UpdateDataUI(PopupCode.NameChangeUI);
            this.OffUI();
        }

        public override void UpdateData(object data = null)
        {
            base.UpdateData(data);
            SetAvar();
        }
        public void SetAvar()
        {
            if (this.listsAva.Count != DataAssets.instance.imageAvar.Count) return;
            for (int i = 0; i < this.listsAva.Count; i++)
            {
                this.listsAva[i].Init(DataAssets.instance.imageAvar[i]);
            }
        }
    }
}