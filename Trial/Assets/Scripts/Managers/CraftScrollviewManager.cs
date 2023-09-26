using Structs;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftScrollviewManager : BaseMonoBehaviour
{
    private IGameManager _gameManager;
    public IGameManager GameManager
    {
        get { return _gameManager; }
    }

    public void Init(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }
    private int contentNum = 10;
    private int poolSize = 10;
    public GameObject CraftUIPrefab;
    private List<GameObject> CraftUIpool = new List<GameObject>();
    void Start()
    {
        // 풀에 GameObject를 미리 생성하여 추가합니다.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(CraftUIPrefab,this.transform);
            obj.SetActive(true);
            CraftUIpool.Add(obj);
        }
    }
    public void Content_Change(CraftUIinfo[] uIinfos)
    {
        contentNum = uIinfos.Length;
        this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(contentNum * 120,60);
        for(int i = poolSize; i < contentNum; i++)
        {
            GameObject obj = Instantiate(CraftUIPrefab,this.transform);
            obj.SetActive(false);
            CraftUIpool.Add(obj);
        }
        foreach(GameObject prefab in CraftUIpool)
        {
            prefab.SetActive(false);
        }
        for(int i=0;i<contentNum;i++)
        {
            CraftUIpool[i].GetComponentInChildren<TMP_Text>().text=uIinfos[i].name;
            CraftUIpool[i].GetComponentInChildren<Image>().sprite= Resources.Load<Sprite>(uIinfos[i].imageName);
            CraftUIpool[i].SetActive(true);
        }
    }
}
