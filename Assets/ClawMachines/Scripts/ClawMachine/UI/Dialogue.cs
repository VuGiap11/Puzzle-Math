using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    //public TextMeshProUGUI textComponent;
    //public string[] lines;
    //public float textSpeed =0.3f;

    //private int index;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    textComponent.text = string.Empty;
    //    StartDialogue();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (textComponent.text == lines[index])
    //        {
    //            NextLine();
    //        }
    //        else
    //        {
    //            StopAllCoroutines();
    //            textComponent.text = lines[index];
    //        }
    //    }
    //}

    //void StartDialogue()
    //{
    //    index = 0;
    //    StartCoroutine(TypeLine());
    //}

    //IEnumerator TypeLine()
    //{
    //    foreach (char c in lines[index].ToCharArray())
    //    {
    //        textComponent.text += c;
    //        yield return new WaitForSeconds(textSpeed);
    //    }
    //}

    //void NextLine()
    //{
    //    if (index < lines.Length - 1)
    //    {
    //        index++;
    //        textComponent.text = string.Empty;
    //        StartCoroutine(TypeLine());
    //    }
    //    else
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}

    public TextMeshProUGUI textUI; // Kéo thả UI Text vào đây trong Inspector
    private float typingSpeed = 0.05f; // Tốc độ hiển thị từng chữ

    private string fullText; // Chuỗi đầy đủ
    private Coroutine typingCoroutine; // Lưu coroutine để dừng nếu cần

    private string fullTextOnNotice = "Bắt đầu trò chơi bằng cách nhét xu vào máy và ấn nút, sau đó dùng cần điều khiển để di chuyển càng gắp, canh đúng vị trí rồi nhấn nút để gắp thú bông!";


    void Start()
    {
        //fullText = textUI.text; // Lấy nội dung gốc
        //textUI.text = ""; // Ẩn nội dung ban đầu
        // StartTyping();
        //SetStart();

    }

    public void SetStart()
    {
        //fullText = textUI.text; // Lấy nội dung gốc
        fullText = fullTextOnNotice;
        textUI.text = ""; // Ẩn nội dung ban đầu
        StartTyping();
    }

    private void OnEnable()
    {
        SetStart();
    }

    public void StartTyping()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        textUI.text = ""; // Đặt lại văn bản rỗng
        foreach (char letter in fullText)
        {
            textUI.text += letter; // Thêm từng chữ cái
            yield return new WaitForSeconds(typingSpeed); // Chờ trước khi thêm chữ tiếp theo
        }
    }
}

