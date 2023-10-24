using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilites
{
    public static bool isTargetAvailable(GameObject sourceObject,out GameObject targetObject,Transform TryTarget, float rayLength, string layer)
    {
        Vector3 sourcePosition;
        Vector3 sourceDirection;
        RaycastHit hit;
        // sourceObject의 위치와 방향을 가져옵니다.
        if (sourceObject.tag == Globals.TagName.Player)
        {
            sourcePosition = sourceObject.transform.position;
            sourceDirection = sourceObject.transform.forward;
            if (Physics.Raycast(sourcePosition, sourceDirection, out hit, rayLength, LayerNametoNum(layer)))
            {
                // 충돌한 게임 오브젝트를 반환합니다.
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer(layer))
                {
                    targetObject = hit.collider.gameObject;
                    return true;
                }
                else
                {
                    targetObject = null;
                    return false;
                }
            }
        }
        else
        {
            sourcePosition = sourceObject.transform.position;
            sourceDirection = TryTarget.position-sourceObject.transform.position;
            if(Physics.Raycast(sourcePosition, sourceDirection, out hit, rayLength))
            {
                // 충돌한 게임 오브젝트를 반환합니다.
                if (hit.collider.gameObject.layer == LayerNametoNum(layer))
                {
                    targetObject = hit.collider.gameObject;
                    return true;
                }
                else
                {
                    targetObject = null;
                    return false;
                }
            }
        }
        targetObject = null;
        // 아무 물체와 충돌하지 않은 경우 null을 반환합니다.
        return false;
    }
    public static int LayerNametoNum(string layer)
    {
        int layerMask = 1 << LayerMask.NameToLayer(layer);
        return layerMask;
    }
    public static float floatoN(float n,float divide,float bias)
    {
        int Nvalue;
        n = (n-bias)/ divide;
        if (n >= 0)
        {
            Nvalue = Mathf.FloorToInt(n + 0.5f); // 반올림
        }
        else
        {
            Nvalue = Mathf.CeilToInt(n - 0.5f); // 음수일 경우 반올림
        }
        return Nvalue * divide+bias;
    }
}
