using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class CraftManager : MonoBehaviour
{

    // 참조, 역참조 설정
    private IGameManager _gameManager;
    public IGameManager GameManager
    {
        get { return _gameManager; }
    }

    public void Init(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private PreBuildControl preBuild;
    private void Awake()
    {
        preBuild = FindObjectOfType<PreBuildControl>();
        preBuild.Init(this);
        Craft = Resources.Load<GameObject>("Craft");
    }
    public PreBuildControl GetPreBuild()
    {
        return preBuild;
    }


    // 필요한 함수 정의
    // 1. 특정 위치에 제작이 가능한지를 확인하는 함수
    private GameObject Craft;
    private GameObject _craft;
    private Transform[] buildingTypeObjects;
    public void ObjectBuild(CraftTypes type, Vector3 position,Quaternion rotation)
    {
        switch (type)
        {
            
        }
        _craft = GameObject.Instantiate(Craft);
        buildingTypeObjects = new Transform[_craft.transform.childCount];
        for (int i = 0; i < _craft.transform.childCount; i++)
        {
            buildingTypeObjects[i] = _craft.transform.GetChild(i);
        }

        // 모든 하위 오브젝트를 비활성화합니다.
        foreach (var obj in buildingTypeObjects)
        {
                obj.gameObject.SetActive(false);
        }

        // 선택한 건축 타입에 해당하는 하위 오브젝트를 활성화합니다.
        switch (type)
            {
                case CraftTypes.Floor:
                    buildingTypeObjects[0].gameObject.SetActive(true);
                    break;
                case CraftTypes.Column:
                    buildingTypeObjects[1].gameObject.SetActive(true);
                    break;
                case CraftTypes.Wall:
                    buildingTypeObjects[2].gameObject.SetActive(true);
                    break;
                    // 다른 건축 타입에 대한 case 추가
            }
        _craft.GetComponent<Craft_Data>().craft.type = type.ToString();
        _craft.GetComponent<Craft_Data>().craft.HP = (int)(Random.Range(0f, 100f));
        _craft.transform.position = position;
        _craft.transform.rotation = rotation;
    }
    // 2. 특정 위치에 제작할 경우 그것을 Database에 저장하는 함수

}
