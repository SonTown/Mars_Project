using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 30f;

    private void Update()
    {
        // 터치 입력 처리
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // 첫 번째 터치를 가져옵니다.

            // 터치 화면의 가운데 위치
            Vector2 touchCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

            // 터치 위치가 화면의 왼쪽 반인 경우
            if (touch.position.x < touchCenter.x)
            {
                // 캐릭터를 이동
                Vector3 moveDirection = new Vector3(touch.deltaPosition.x, 0, touch.deltaPosition.y);
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
                Debug.Log("1");
            }
            // 터치 위치가 화면의 오른쪽 반인 경우
            else
            {
                // 캐릭터를 회전
                float rotationAmount = -touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount);
            }
        }
    }
}
