using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 30f;
    public float maxSpeed;
    public float maxRotSpeed;
    public GameObject cam;

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

    private void Update()
    {
        // 왼손과 오른손 터치 입력 가져오기
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
                Debug.Log(rotateTouch_index);
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
        if (Input.GetKeyDown(KeyCode.Space))

        {

            GameManager.Instance.SlowGame(0.1f);

        }
    }
}
