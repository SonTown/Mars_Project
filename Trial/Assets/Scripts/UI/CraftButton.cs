using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CraftButton : BaseMonoBehaviour, IPointerUpHandler
{
    public PreBuildControl prebuild;
    public Material redMaterial;
    private Material originMaterial;
    private Renderer objectRenderer;
    private void Awake()
    {
        prebuild = GameManager.Instance.GetCraftManager().GetPreBuild();
        objectRenderer = prebuild.gameObject.GetComponentInChildren<Renderer>();
        originMaterial = objectRenderer.material;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (prebuild.CraftCheck(prebuild.currentBuildingType))
        {
            Debug.Log(prebuild.ResourceCheck(prebuild.currentBuildingType, 0));
            GameManager.Instance.GetCraftManager().ObjectBuild(prebuild.currentBuildingType, prebuild.transform.position, prebuild.transform.rotation);
        }
        else
        {
            ChangerColorToRed();
            Debug.Log("Impossible to Build");
        }
    }
    public void ChangerColorToRed()
    {
        objectRenderer.material = redMaterial;
        StartCoroutine(RevertColor());
    }
    IEnumerator RevertColor()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        objectRenderer.material = originMaterial;
    }
}
