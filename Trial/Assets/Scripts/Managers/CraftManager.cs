using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class CraftManager : MonoBehaviour
{

    // ����, ������ ����
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


    // �ʿ��� �Լ� ����
    // 1. Ư�� ��ġ�� ������ ���������� Ȯ���ϴ� �Լ�
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

        // ��� ���� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        foreach (var obj in buildingTypeObjects)
        {
                obj.gameObject.SetActive(false);
        }

        // ������ ���� Ÿ�Կ� �ش��ϴ� ���� ������Ʈ�� Ȱ��ȭ�մϴ�.
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
                    // �ٸ� ���� Ÿ�Կ� ���� case �߰�
            }
        _craft.GetComponent<Craft_Data>().craft.type = type.ToString();
        _craft.GetComponent<Craft_Data>().craft.HP = (int)(Random.Range(0f, 100f));
        _craft.transform.position = position;
        _craft.transform.rotation = rotation;
    }
    // 2. Ư�� ��ġ�� ������ ��� �װ��� Database�� �����ϴ� �Լ�

}
