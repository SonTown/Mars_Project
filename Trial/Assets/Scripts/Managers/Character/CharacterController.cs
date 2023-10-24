using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using UnityEngine.EventSystems;
using Character_Function;
using Structs;
using System;

public class CharacterController : BaseMonoBehaviour
{
    private CharacterManager characterManager;
    private PreBuildControl preBuild;
    //이동 속도 및 기타 스테이터스 관리 파트
    public float moveSpeed = 5f;
    public float rotationSpeed = 30f;
    public float maxSpeed;
    public float maxRotSpeed;
    public GameObject cam;
    private PlayerStates currentState=PlayerStates.Build;
    private CraftTypes currentCraft = CraftTypes.Floor;

    //화면 컨트롤을 통한 이동을 위한 변수들
    //현재 움직임 여부 체크
    private bool isMove=false;
    private bool isRotate=false;
    //Touch를 처리할 index 값
    private int moveTouch_index = -1;
    private int rotateTouch_index = -1;
    private int moveTouch_fingerId = -1;
    private int rotateTouch_fingerId = -1;
    //Touch와 이동을 위한 벡터 데이터
    private Touch MoveTouch;
    private Touch RotateTouch;
    private Vector3 startPosition;
    private Vector3 moveVector;
    private Vector3 rotateVector;

    //craft 기능을 위한 변수들
    //Craft를 위한 좌표 설정을 위한 변수들
    private RaycastHit Selected;
    private float range=10f;
    private Vector3 craftPoint;
    private string Craftlayer = Globals.LayerName.Craft;
    private Ray targetingRay;
    public InfoButton infoButton;
    //정보 띄우기 위한 버튼 활성화 여부
    //제작 연습용 데이터들
    public GameObject Prefab;
    public GameObject Prefab2;
    private Build_Function build_Func = new Build_Function();
    private Vector3 available_Position;
    public void Init(CharacterManager charactermanager)
    {
        characterManager = charactermanager;
    }
    private void Awake()
    {
        currentCraft = GameManager.Instance.GetCharacterManager().craftType;
        print(GameManager.Instance);
        print(GameManager.Instance.GetCraftManager());
        print(GameManager.Instance.GetCraftManager().GetPreBuild());
        preBuild = GameManager.Instance.GetCraftManager().GetPreBuild();
        infoButton = FindObjectOfType<InfoButton>();
        infoButton.transform.gameObject.SetActive(true);
    }
    private string text;
    private bool isLookingCraft;
    private GameObject craft;
    private GameObject lookingCraft;
    private Dataset_craft dataSave;
    private void Update()
    {

        // Touch Control Part- Control move and other things
        if (Input.touchCount > 0)
        {
            if (Input.touchCount > 1)
            {
                GameManager.Instance.SlowGame(1f);
            }
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        // UGUI 요소 위에서 터치가 발생한 경우, 해당 입력을 무시
                        continue;
                    }
                    if (touch.position.x < Screen.width / 2f)
                    {
                        moveTouch_fingerId = touch.fingerId;
                        startPosition = touch.position;
                        moveVector = Vector3.zero;
                        isMove = true;
                        moveTouch_index = i;
                    }
                    else
                    {
                        rotateTouch_fingerId = touch.fingerId;
                        rotateVector = Vector3.zero;
                        isRotate = true;
                        rotateTouch_index = i;
                    }
                }
                else if(touch.phase == TouchPhase.Ended)
                {
                    if (moveTouch_fingerId == touch.fingerId)
                    {
                        moveTouch_index = -1;
                        moveTouch_fingerId = -1;
                        moveVector = Vector3.zero;
                        isMove = false;
                    }
                    if (rotateTouch_fingerId == touch.fingerId)
                    {
                        rotateTouch_index = -1;
                        rotateTouch_fingerId = -1;
                        rotateVector = Vector3.zero;
                        isRotate = false;
                    }
                }
                else if (touch.fingerId == moveTouch_fingerId)
                {
                    moveTouch_index = i;
                }
                else if (touch.fingerId == rotateTouch_fingerId)
                {
                    rotateTouch_index = i;
                }
            }
            if (isMove == true && moveTouch_index != -1)
            {
                MoveTouch = Input.GetTouch(moveTouch_index);
                // 왼손 터치로 캐릭터 이동
                if (MoveTouch.phase == TouchPhase.Moved)
                {
                    moveVector = new Vector3(MoveTouch.position.x - startPosition.x, 0, MoveTouch.position.y - startPosition.y);
                    if (moveVector.magnitude > maxSpeed)
                    {
                        moveVector = moveVector.normalized * maxSpeed;
                    }
                    transform.Translate(moveVector * moveSpeed * Time.deltaTime);
                }
                else if (MoveTouch.phase == TouchPhase.Stationary)
                {
                    transform.Translate(moveVector * moveSpeed * Time.deltaTime);
                }
            }
            if (isRotate == true&&rotateTouch_index!=-1)
            {
                RotateTouch = Input.GetTouch(rotateTouch_index);
                if (RotateTouch.phase == TouchPhase.Moved)
                {
                    rotateVector = new Vector3(-RotateTouch.deltaPosition.y, RotateTouch.deltaPosition.x,0);
                    if (rotateVector.magnitude > maxRotSpeed)
                    {
                        rotateVector = rotateVector.normalized * maxRotSpeed;
                    }
                    cam.transform.Rotate(rotateVector.x * maxRotSpeed * Time.deltaTime,0,0,Space.Self);
                    transform.Rotate(0, rotateVector.y * maxRotSpeed * Time.deltaTime, 0, Space.Self);
                }
                else if (MoveTouch.phase == TouchPhase.Stationary)
                {
                    
                }
            }
        }
        //Action Part- The thing That Player will do
        switch (currentState)
        {
            case PlayerStates.Build:
                if (build_Func.BuildPosition(cam, out available_Position, out craft, range, Craftlayer))
                {
                    infoButton.transform.gameObject.SetActive(true);
                    
                    if (lookingCraft != null)
                    {
                        if(lookingCraft != craft)
                        {
                            GameManager.Instance.GetTooltipManager().HideTooltip();
                            isLookingCraft = false;
                            lookingCraft = null;
                        }
                    }
                    isLookingCraft = true;
                }
                else
                {
                    if (isLookingCraft)
                    {
                        GameManager.Instance.GetTooltipManager().HideTooltip();
                        isLookingCraft = false;
                        lookingCraft = null;
                        infoButton.transform.gameObject.SetActive(false);
                    }
                    preBuild.PositionControl(available_Position, characterManager.craftType);
                }
                break;
            case PlayerStates.Fight:
                break;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.SlowGame(1f);

        }
    }
    public bool InfoActivate()
    {
        if (isLookingCraft)
        {
            lookingCraft = craft;
            text = "<size=40>Level: " + (craft.GetComponentInParent<Craft_Data>().craft.Level).ToString() + "</size>\n";
            text += "<size=20>HP: " + (craft.GetComponentInParent<Craft_Data>().craft.HP).ToString() + "</size>\n";
            dataSave = GameManager.Instance.GetDataManager().craftRequests[(CraftTypes)Enum.Parse(typeof(CraftTypes), craft.GetComponentInParent<Craft_Data>().craft.type)];
            text += "<size=10>";
            for (int i = 0; i < dataSave.item_name.Length; i++)
            {
                if (dataSave.item_name[i] == null)
                {
                    continue;
                }
                text = text + dataSave.item_name[i] + ": " + dataSave.item_num[craft.GetComponentInParent<Craft_Data>().craft.Level + 1, i] + "piece needed\n";
            }
            text += "</size>";
            GameManager.Instance.GetTooltipManager().ShowTooltip(text);
            return true;
        }
        return false;
    }
}
