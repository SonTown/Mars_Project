using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using Character_Function;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 30f;
    public float maxSpeed;
    public float maxRotSpeed;
    public GameObject cam;
    public PlayerStates currentState=PlayerStates.Build;

    //화면 컨트롤을 통한 이동을 위한 변수들
    private bool isMove=false;
    private bool isRotate=false;
    private int moveTouch_index = -1;
    private int rotateTouch_index = -1;
    private Touch MoveTouch;
    private Touch RotateTouch;
    private Vector3 startPosition;
    private Vector3 moveVector;
    private Vector3 startRotation;
    private Vector3 rotateVector;

    //craft 기능을 위한 변수들
    private RaycastHit Selected;
    private float range=5f;
    private string Craftlayer = Globals.LayerName.Craft;
    private Vector3 craftPoint;
    private Ray targetingRay;
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
                    if (touch.position.x < Screen.width / 2f)
                    {
                        moveTouch_index = i;
                        startPosition = touch.position;
                        moveVector = Vector3.zero;
                        isMove = true;
                    }
                    else
                    {
                        rotateTouch_index = i;
                        startRotation = touch.position;
                        rotateVector = Vector3.zero;
                        isRotate = true;
                    }
                }
                else if(touch.phase == TouchPhase.Ended)
                {
                    if (moveTouch_index == i)
                    {
                        moveTouch_index = -1;
                        moveVector = Vector3.zero;
                        isMove = false;
                    }
                    if (rotateTouch_index == i)
                    {
                        rotateTouch_index = -1;
                        rotateVector = Vector3.zero;
                        isRotate = false;
                    }
                }
            }
            if (isMove == true)
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
            if (isRotate == true)
            {
                RotateTouch = Input.GetTouch(rotateTouch_index);
                if (RotateTouch.phase == TouchPhase.Moved)
                {
                    rotateVector = new Vector3(-RotateTouch.position.y + startRotation.y, RotateTouch.position.x - startRotation.x,0);
                    if (rotateVector.magnitude > maxRotSpeed)
                    {
                        rotateVector = rotateVector.normalized * maxRotSpeed;
                    }
                    cam.transform.Rotate(rotateVector.x * maxRotSpeed * Time.deltaTime,0,0,Space.Self);
                    transform.Rotate(0, rotateVector.y * maxRotSpeed * Time.deltaTime, 0, Space.Self);
                }
                else if (MoveTouch.phase == TouchPhase.Stationary)
                {
                    cam.transform.Rotate(rotateVector.x * maxRotSpeed * Time.deltaTime, 0, 0, Space.Self);
                    transform.Rotate(0, rotateVector.y * maxRotSpeed * Time.deltaTime, 0, Space.Self);
                }
            }
        }
        //Action Part- The thing That Player will do
        switch (currentState)
        {
            case PlayerStates.Build:
                if (build_Func.BuildPosition(cam,out available_Position, range, Craftlayer))
                {
                    Prefab2.transform.position=available_Position;
                    Prefab.transform.position = new Vector3(Utilites.floatoN(available_Position.x, 1, 0), Utilites.floatoN(available_Position.y, 1, 0), Utilites.floatoN(available_Position.z, 1, 0));
                }
                break;
            case PlayerStates.Fight:
                break;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

            GameManager.Instance.SlowGame(0.1f);

        }
    }
}
