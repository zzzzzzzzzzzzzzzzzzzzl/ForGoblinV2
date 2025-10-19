using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelSelect : UIMaterialManager
{
    public GameObject portraitPrefab;
    List<GameObject> ais = new();//have a manager class for these just temp setup
    List<GameObject> levelIcons = new();
    public Transform layout;
    public RectTransform scrollRect;
    public RectTransform viewPort;
    public Button leftButton;
    public Button rightButton;
    public Button startButton;
    int _selectedIDX = 0;
    Vector3 targetPos;
    public int selectedIDX//make the idx wrap around when it goes out of bounds
    {
        get => _selectedIDX; set
        {
            _selectedIDX = value;
            if (value < 0)
            {
                _selectedIDX = ais.Count - 1;
            }
            if (value > ais.Count - 1)
            {
                _selectedIDX = 0;
            }
        }
    }
    protected override void Start()
    {
        base.Start();//inherits from manager to set materials
        loadAIPannels();//instantiate the ai pannels// we could probably make the prefab the pannel instead//
        leftButton.onClick.AddListener(() => { incrementAI(-1); });
        rightButton.onClick.AddListener(() => { incrementAI(1); });
        startButton.onClick.AddListener(() => { loadGame(); });
        aiType.selected=ais[0].GetComponent<aiType>();
    }

    void loadAIPannels()//
    {
        ais = ResourceLoader.AIPrefabs.Values.ToList();
        foreach (aiType ai in ais.Select(g => g.GetComponent<aiType>()))
        {
            GameObject portraitGO = Instantiate(portraitPrefab, layout);
            portraitGO.transform.SetParent(layout);
            levelSelectPortrait portrait = portraitGO.GetComponent<levelSelectPortrait>();

            portrait.image.sprite = ai.sprite;
            portrait.image.material = ai.material;
            portrait.button.onClick.AddListener(() =>
            {
                selectAI(ai);
            });
            levelIcons.Add(portraitGO);

        }
    }
    void selectAI(aiType ai)
    {
        selectedIDX = ais.IndexOf(ai.gameObject);
        targetPos = scrollRect.transform.position - (levelIcons[selectedIDX].transform.position - viewPort.transform.position);
        ellapsedFrames = 0;
        aiType.selected = ai;
    }
    void incrementAI(int dir)
    {
        selectedIDX += dir;
        selectAI(ais[selectedIDX].GetComponent<aiType>());
    }
    float ellapsedFrames = 0;
    void Update()
    {
        if (scrollRect.transform.position != targetPos && targetPos != new Vector3())
        {
            ellapsedFrames += .001f;
            scrollRect.transform.position = Vector3.Lerp(scrollRect.transform.position, targetPos, ellapsedFrames);
            if (scrollRect.transform.position == targetPos)
            {
                ellapsedFrames = 0;
            }
        }
    }
    void loadGame()
    {
        cleanStaticData.clear();
        SceneManager.LoadScene("game");
    }
}