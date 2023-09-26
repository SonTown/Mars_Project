using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using UnityEngine.EventSystems;
using Character_Function;
using Structs;

public class CharacterController : BaseMonoBehaviour
{
    //�̵� �ӵ� �� ��Ÿ �������ͽ� ���� ��Ʈ
    public float moveSpeed = 5f;
    public float rotationSpeed = 30f;
    public float maxSpeed;
    public float maxRotSpeed;
    public GameObject cam;
    public PlayerStates currentState=PlayerStates.Build;

    //ȭ�� ��Ʈ���� ���� �̵��� ���� ������
    //���� ������ ���� üũ
    private bool isMove=false;
    private bool isRotate=false;
    //Touch�� ó���� index ��
    private int moveTouch_index = -1;
    private int rotateTouch_index = -1;
    private int moveTouch_fingerId = -1;
    private int rotateTouch_fingerId = -1;
    //Touch�� �̵��� ���� ���� ������
    private Touch MoveTouch;
    private Touch RotateTouch;
    private Vector3 startPosition;
    private Vector3 moveVector;
    private Vector3 rotateVector;

    //craft ����� ���� ������
    //Craft�� ���� ��ǥ ������ ���� ������
    private RaycastHit Selected;
    private float range=10f;
    private Vector3 craftPoint;
    private string Craftlayer = Globals.LayerName.Craft;
    private Ray targetingRay;
    private float lattice_length = 4f;
    //���� ������ �����͵�
    public GameObject Prefab;
    public GameObject Prefab2;
    private Build_Function build_Func = new Build_Function();
    private Vector3 available_Position;
    private void Update()
    {
        // Touch Control Part- Control move and other things
        if (Input.touchCount > 0)
        {

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        // UGUI ��� ������ ��ġ�� �߻��� ���, �ش� �Է��� ����
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
                // �޼� ��ġ�� ĳ���� �̵�
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
                if (build_Func.BuildPosition(cam,out available_Position, range, Craftlayer))
                {
                    //Prefab2.transform.position=available_Position;
                    Prefab.transform.position = new Vector3(Utilites.floatoN(available_Position.x,lattice_length, 0), Utilites.floatoN(available_Position.y, lattice_length, 0), Utilites.floatoN(available_Position.z, lattice_length, 0));
                }
                break;
            case PlayerStates.Fight:
                break;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

            GameManager.Instance.SlowGame(0.1f);
            CraftUIinfo[] craft=new CraftUIinfo[1];
           // craft[0] = new CraftUIinfo("Floor", "Floor");
            //GameManager._Craft_instance.Content_Change(craft);

        }
    }
}
