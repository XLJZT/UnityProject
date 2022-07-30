using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialog : MonoBehaviour
{
    
    [Header("组件")]
    public Text textLabel;
    public Image image;

    [Header("文本")]
    public TextAsset textAsset;
    public int index;
    [Header("头像")]
    public Sprite headA, headB;

    public float timeSpeed;
    //判断协程是否结束，防止多个协程同时出现
    //初始时设置为true
    bool textFinshed = true;
    //存储文本
    List<string> textList = new List<string>();
    void Awake()
    {
        GetTextFormFilr(textAsset);
    }
    private void OnEnable()
    {
        //当启用时，显示第一句话
        StartCoroutine(SetText());
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (index >= textList.Count)
            {
                gameObject.SetActive(false);
                index = 0;
            }
            else if (textFinshed)
            {
                StartCoroutine(SetText());
            }
            else if (!textFinshed)
            {
                StopCoroutine(SetText());
                textFinshed = true;
                
                textLabel.text = textList[index];
                index++;
            }


        }
    }

    void GetTextFormFilr(TextAsset text)
    {
        textList.Clear();
        index = 0;
        //通过回车符分割
        var list = text.text.Split('\n');
        foreach (var item in list)
        {
            textList.Add(item);
        }
    }

    IEnumerator SetText()
    {
        textFinshed = false;
        //清空textLabel里的文字
        textLabel.text = "";
        //这里不清楚是不是我的文本的问题，获取时总是获取到两个字符，但不知道第二个字符是什么
        switch (textList[index][0])
        {
            case 'A':
                image.sprite = headA;
                index++;
                break;

            case 'B':
                image.sprite = headB;
                index++;
                break;
        }
        //每隔timeSpeed秒进行显示一次
        for (int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];

            yield return new WaitForSeconds(timeSpeed);
        }
        //更改index值，并更改状态
        
        textFinshed = true;
        index++;

    }

}
