using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialog : MonoBehaviour
{
    
    [Header("���")]
    public Text textLabel;
    public Image image;

    [Header("�ı�")]
    public TextAsset textAsset;
    public int index;
    [Header("ͷ��")]
    public Sprite headA, headB;

    public float timeSpeed;
    //�ж�Э���Ƿ��������ֹ���Э��ͬʱ����
    //��ʼʱ����Ϊtrue
    bool textFinshed = true;
    //�洢�ı�
    List<string> textList = new List<string>();
    void Awake()
    {
        GetTextFormFilr(textAsset);
    }
    private void OnEnable()
    {
        //������ʱ����ʾ��һ�仰
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
        //ͨ���س����ָ�
        var list = text.text.Split('\n');
        foreach (var item in list)
        {
            textList.Add(item);
        }
    }

    IEnumerator SetText()
    {
        textFinshed = false;
        //���textLabel�������
        textLabel.text = "";
        //���ﲻ����ǲ����ҵ��ı������⣬��ȡʱ���ǻ�ȡ�������ַ�������֪���ڶ����ַ���ʲô
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
        //ÿ��timeSpeed�������ʾһ��
        for (int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];

            yield return new WaitForSeconds(timeSpeed);
        }
        //����indexֵ��������״̬
        
        textFinshed = true;
        index++;

    }

}
