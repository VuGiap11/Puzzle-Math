using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class ActionText
    {
        public static IEnumerator ShowCountText(int fromNumber, int toNumber, UnityEngine.UI.Text txtShow, float delayPlay, float duration)
        {
            yield return new WaitForSeconds(delayPlay);
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;

                // Làm mượt animation bằng Lerp
                int currentNumber = Mathf.RoundToInt(Mathf.Lerp(fromNumber, toNumber, progress));
                txtShow.text = currentNumber.ToString();

                yield return null;
            }

            txtShow.text = toNumber.ToString();
        }

        public static IEnumerator ShowCountTextTimeToMinute(int fromSeconds, int toSeconds, UnityEngine.UI.Text txtShow, float duration, float delayPlay, System.Action<float> CallBackDuration)
        {
            yield return new WaitForSeconds(delayPlay);
            // Tổng thời gian animation
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;

                // Làm mượt animation bằng Lerp
                int currentSeconds = Mathf.RoundToInt(Mathf.Lerp(fromSeconds, toSeconds, progress));

                // Chuyển đổi số giây thành định dạng mm:ss
                int minutes = currentSeconds / 60;
                int seconds = currentSeconds % 60;
                txtShow.text = $"{minutes:00}:{seconds:00}";

                yield return null;
            }

            // Hiển thị giá trị cuối cùng
            int finalMinutes = toSeconds / 60;
            int finalSeconds = toSeconds % 60;
            txtShow.text = $"{finalMinutes:00}:{finalSeconds:00}";

            CallBackDuration?.Invoke(duration);
        }
        public static IEnumerator ShowCountTextPercent(int fromNumber, int toNumber, UnityEngine.UI.Text txtShow, UnityEngine.UI.Slider sliderShow, float delayPlay, float duration)
        {
            yield return new WaitForSeconds(delayPlay);
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;

                // Làm mượt animation bằng Lerp
                int currentNumber = Mathf.RoundToInt(Mathf.Lerp(fromNumber, toNumber, progress));
                txtShow.text = currentNumber.ToString() + "%";
                sliderShow.value = currentNumber / (float)toNumber;
                yield return null;
            }

            txtShow.text = toNumber.ToString() + "%";
        }

    }

}