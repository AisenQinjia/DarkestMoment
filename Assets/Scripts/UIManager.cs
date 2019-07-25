using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private IEnumerator coroutine;
    private static UIManager instance;
    private Dictionary<string, Dictionary<string, GameObject>> allWidgets; //保存对所有控件的引用
    public static float offsetX = 0;
    public static float offsetY = 3;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                instance = obj.AddComponent<UIManager>();
                obj.name = "UICanvas";
                obj.AddComponent<Canvas>().sortingOrder = 10;
                DontDestroyOnLoad(obj);
            }
            return instance;
        }

    }

    private GameObject hintPf;
    private GameObject typeWriterPf;
    private GameObject Canvas;

    private static bool hasCreated = false;
    private GameObject bloodSprite;

    private Vector3 hintPos = new Vector2(0, -100);

    private void Awake()
    {

        instance = this;

        allWidgets = new Dictionary<string, Dictionary<string, GameObject>>();

        Canvas = this.gameObject;
        hintPf = Resources.Load("Prefabs/UiHint") as GameObject;
        this.bloodSprite = this.transform.Find("bloodSprite").gameObject;
        typeWriterPf = Resources.Load("Prefabs/TypeWriter") as GameObject;
        DontDestroyOnLoad(this.gameObject);

    }


    public void EnabaleBloodSprite()
    {
        bloodSprite.SetActive(true);
    }

    public void DisableBloodSprite()
    {
        bloodSprite.SetActive(false);
    }

    private GameObject hintGo;
    private GameObject typeWriter;

    public void PopHint(string str)
    {
        if (hintGo == null)
        {
            hintGo = Instantiate(this.hintPf);
        }
        hintGo.GetComponentInChildren<Text>().text = str;

        //  hintGo.transform.localPosition = this.hintPos;
        hintGo.transform.SetParent(this.transform, false);

        Destroy(hintGo, 2);
    }

    //打字机打一段话(有个s...)
    public void PopTypewriterSentences(string[] str, float wordTime, float sentenceTime, Transform trans = null)
    {
        if (typeWriter == null)
        {
            typeWriter = Instantiate(this.typeWriterPf);
            typeWriter.transform.SetParent(this.transform, false);
        }
        if (trans != null)
        {
            Camera mainCam = Camera.main;
            if (mainCam == null) Debug.Log("no Main Camera");
            else
            {
                Vector3 offset = new Vector3(trans.position.x, trans.position.y + 1, trans.position.z);
                var playerScreenPos = mainCam.WorldToScreenPoint(offset);
                Vector2 localPoint;
  
                RectTransformUtility.ScreenPointToLocalPointInRectangle(this.transform.GetComponent<RectTransform>(), playerScreenPos, null, out localPoint);
                typeWriter.GetComponent<RectTransform>().localPosition = localPoint;
            }
        }
        else typeWriter.GetComponent<RectTransform>().anchoredPosition = new Vector2(640f, 700f);
        typeWriter.SetActive(true);
        coroutine = TypeStrings(str, typeWriter.GetComponent<Text>(), sentenceTime, wordTime);
        StartCoroutine(coroutine);
    }

    //打字机打一句话
    public void PopTypewriterSentence(string str, float wordTime, Transform trans = null)
    {
        if (typeWriter == null)
        {
            typeWriter = Instantiate(this.typeWriterPf);
            typeWriter.transform.SetParent(this.transform, false);
            Debug.Log(typeWriter.transform.position);
        }
        if (trans != null)
        {
            //Debug.Log("trans != null");
            Camera mainCam = Camera.main;
            if (mainCam == null) Debug.Log("no Main Camera");
            else
            {
                Vector3 offset = new Vector3(trans.position.x, trans.position.y + 1, trans.position.z);
                var playerScreenPos = mainCam.WorldToScreenPoint(offset);
                Vector2 localPoint;
               
                RectTransformUtility.ScreenPointToLocalPointInRectangle(typeWriter.GetComponentInParent<RectTransform>(), playerScreenPos, null, out localPoint);
                Debug.Log("Screen: " + playerScreenPos.x + "   " + playerScreenPos.y);
                Debug.Log("local: " + localPoint.x + "   " + localPoint.y);
                typeWriter.GetComponent<RectTransform>().localPosition = localPoint;
            }
        }
        else typeWriter.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 350f);
        typeWriter.SetActive(true);
        coroutine = TypeString(str, typeWriter.GetComponent<Text>(), wordTime);
        StartCoroutine(coroutine);
    }

    IEnumerator TypeString(string str, Text textReference, float wordTime, Transform trans = null)
    {
        string tmp = "";
        for (int i = 0; i < str.Length; ++i)
        {
            tmp += str[i];
            textReference.text = tmp;
            yield return new WaitForSeconds(wordTime);
        }
        typeWriter.SetActive(false);
    }

    IEnumerator TypeStrings(string[] str, Text textReference, float senteceTime, float wordTime)
    {
        string tmps = "";
        string tmp = "";
        for (int i = 0; i < str.Length; ++i)
        {
            tmps = str[i];
            tmp = "";
            for (int j = 0; j < tmps.Length; ++j)
            {
                tmp += tmps[j];
                textReference.text = tmp;
                yield return new WaitForSeconds(wordTime);
            }
            yield return new WaitForSeconds(senteceTime);
        }
        typeWriter.SetActive(false);
    }


    private Dictionary<string, GameObject> panelDict = new Dictionary<string, GameObject>();

    public GameObject PopPanel(string name, TweenCallback tweenCallback)
    {
        GameObject panel = null;
        panelDict.TryGetValue(name, out panel);

        if (panel == null)
        {
            panel = Instantiate(Resources.Load(GameDefine.panelPath + name)) as GameObject;
            panel.transform.SetParent(this.Canvas.transform, false);
            panelDict.Add(name, panel);

        }
        else
        {
            panel.SetActive(true);
        }

        // Tween tween = panel.transform.DOScale(1, 0.5f);
        // tween.OnComplete(tweenCallback);

        BasePanel p = panel.GetComponent<BasePanel>();
        if (p)
        {
            p.OnPop();
        }

        return panel;
    }

    public GameObject PopPanel(string name)
    {
        GameObject panel = null;
        panelDict.TryGetValue(name, out panel);

        if (panel == null)
        {
            panel = Instantiate(Resources.Load(name)) as GameObject;
            panel.transform.SetParent(this.Canvas.transform, false);
            panelDict.Add(name, panel);
        }
        else
        {
            panel.SetActive(true);
        }
        // Tween tween = panel.transform.DOScale(1, 0.5f);
        BasePanel p = panel.GetComponent<BasePanel>();
        if (p)
        {
            p.OnPop();
        }
        return panel;
    }

    public void DestroyAllPanel()
    {
        foreach (KeyValuePair<string, GameObject> panels in panelDict)
        {
            Destroy(panels.Value);

        }
        panelDict.Clear();
    }


    public void ClosePanel(string name)
    {
        GameObject panel = null;
        panelDict.TryGetValue(name, out panel);

        if (panel == null)
            return;
        else
        {
            //panel.transform.DOScale(0, 0.5f).OnComplete(() =>
            //{
            panel.SetActive(false);
            //});

        }

        BasePanel p = panel.GetComponent<BasePanel>();
        if (p)
        {
            p.OnClose();
        }

    }

    public void ClosePanel(string name, TweenCallback tweenCallback)
    {
        GameObject panel = null;
        panelDict.TryGetValue(name, out panel);

        if (panel == null)
            return;
        else
        {
            //panel.transform.DOScale(0, 0.5f).OnComplete(() =>
            //{

            //    tweenCallback();
            panel.SetActive(false);
            //});

        }
        BasePanel p = panel.GetComponent<BasePanel>();
        if (p)
        {
            p.OnClose();
        }

    }
}
