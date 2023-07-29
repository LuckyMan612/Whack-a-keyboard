using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardLayout : MonoBehaviour
{
    public string[] layouts;
    public TMP_Text languageText;

    private int layoutNum;
    private int prevLayoutNum;
    private List<Key> specialList;
    
    // Start is called before the first frame update
    void Start()
    {
        layoutNum = 0;
        prevLayoutNum = layoutNum;
        specialList = new List<Key>();
        specialList.Add(GameObject.Find("Backslash").GetComponent<Key>());
        specialList.Add(GameObject.Find("'").GetComponent<Key>());
    }

    // Update is called once per frame
    void Update()
    {
        if(layoutNum != prevLayoutNum)
        {
            prevLayoutNum = layoutNum;
            switch(layouts[layoutNum])
            {
                case "English - US International":
                    languageText.text = "English - US International";
                    GameObject.Find(";").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = ";";
                    GameObject.Find("Slash").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "/";
                    GameObject.Find("[").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "[";
                    GameObject.Find("]").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "]";
                    GameObject.Find("-").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "-";
                    GameObject.Find("=").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "=";
                    GameObject.Find("BackQuote").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "`";
                    specialList[0].gameObject.SetActive(true);
                    specialList[1].gameObject.SetActive(true);
                    break;
                case "Spanish - Latin American":
                    languageText.text = "Spanish - Latin American";
                    GameObject.Find(";").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Ñ";
                    GameObject.Find("Slash").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "-";
                    GameObject.Find("[").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "´";
                    GameObject.Find("]").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "+";
                    GameObject.Find("-").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "'";
                    GameObject.Find("=").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "¿";
                    GameObject.Find("BackQuote").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "|";
                    specialList[0].gameObject.SetActive(false);
                    specialList[1].gameObject.SetActive(false);
                    break;
                case "Portuguese - Brazil ABNT":
                    languageText.text = "Portuguese - Brazil ABNT";
                    break;
            }
        }
    }

    public void ChangeLayout()
    {
        layoutNum++;
        if(layoutNum >= layouts.Length)
        {
            layoutNum = 0;
        }
    }
}
