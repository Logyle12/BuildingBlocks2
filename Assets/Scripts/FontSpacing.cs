using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontSpacing : MonoBehaviour
{
    public List<GameObject> gameUIPanels = new List<GameObject>();
    private List<TextMeshProUGUI> gameUITextComponents = new List<TextMeshProUGUI>();
    private List<Button> gameUIButtonComponents = new List<Button>();


    // Start is called before the first frame update
    private TextMeshProUGUI text;
    public TextMeshProUGUI sampleText;

    private int index = 0;
    public int defaultIndex = 0;

    public TMP_FontAsset[] fontAssets;

    public List<string> data = new List<string>();
    void Start()
    {
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        text.text = data[defaultIndex];

        transform.Find("Left").GetComponent<Button>().onClick.AddListener(OnLeftClicked);
        transform.Find("Right").GetComponent<Button>().onClick.AddListener(OnRightClicked);

        foreach (GameObject panel in gameUIPanels)
        {
            TextMeshProUGUI[] textComponentsArray = panel.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI item in textComponentsArray)
            {
                gameUITextComponents.Add(item);
            }
        }


    }

    public int indexValue
    {
        get
        {

            return index;

        }

        set
        {
            index = value;
            text.text = data[index];

        }

    }

    public string selectedValue
    {
        get
        {

            return data[index];
        }

    }

    void OnLeftClicked()
    {

        if (index == 0)
        {
            index = data.Count - 1;

        }

        else
        {
            index--;
        }

        indexValue = defaultIndex;
        SetFontFamily();
    }

    void OnRightClicked()
    {
        if (index + 1 >= data.Count)
        {
            index = 0;

        }

        else
        {
            index++;
        }

        text.text = data[index];
        SetFontFamily();

    }

    public int getIndexValue()
    {

        return index;

    }

    public void SetFontFamily()
    {
        int currentFontIndex = getIndexValue();

        sampleText.font = fontAssets[currentFontIndex];
        text.font = fontAssets[currentFontIndex];

        foreach (TextMeshProUGUI textComponent in gameUITextComponents)
        {
            textComponent.font = fontAssets[currentFontIndex];
        }

    }
}
