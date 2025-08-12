using DG.Tweening;
using NTPackage.UI;
namespace Rubik.ClawMachine
{

    public class LevelCompleted : PopupUI
    {
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            GameMemoryController.instance.StartConfety();
            DOVirtual.DelayedCall(4.5f, delegate
            {
                this.OffUI();
            });

        }
        public override void OffUI()
        {
            base.OffUI();
          
            GameMemoryController.instance.EndConfetty();
            DOVirtual.DelayedCall(0.35f, delegate
            {
                CardsController.instance.Init();
                //PopupManager.Instance.OnUI(PopupCode.NextLevel);
            });
        }
    }
}