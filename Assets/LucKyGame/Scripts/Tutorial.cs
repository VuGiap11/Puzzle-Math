using NTPackage.UI;
using Rubik.LuckyGame;

public class Tutorial : PopupUI
{
    public override void OnUI(object data = null)
    {
        base.OnUI(data);
    }

    public void OnTransformChildrenChanged()
    {
        if (LuckyGameManager.Instance.isSpinning)
        {
            return;
        }
        base.OnUI();
    }

    public override void OffUI()
    {
        base.OffUI();
    }
}
