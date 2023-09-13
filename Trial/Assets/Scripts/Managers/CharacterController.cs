using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 30f;

    private void Update()
    {
        // ��ġ �Է� ó��
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // ù ��° ��ġ�� �����ɴϴ�.

            // ��ġ ȭ���� ��� ��ġ
            Vector2 touchCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

            // ��ġ ��ġ�� ȭ���� ���� ���� ���
            if (touch.position.x < touchCenter.x)
            {
                // ĳ���͸� �̵�
                Vector3 moveDirection = new Vector3(touch.deltaPosition.x, 0, touch.deltaPosition.y);
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
                Debug.Log("1");
            }
            // ��ġ ��ġ�� ȭ���� ������ ���� ���
            else
            {
                // ĳ���͸� ȸ��
                float rotationAmount = -touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount);
            }
        }
    }
}
