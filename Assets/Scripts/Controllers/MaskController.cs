using UnityEngine;
using UnityEngine.UI;

public class MaskController : MonoBehaviour
{
    public Image imgMask;
    public Image filter;
    public Color[] colorMask;

    public bool isNewMask = true;

    void Start()
    {
        SelectRandomColor();

        if (!isNewMask)
            SelectRandomDefectiveMask();
    }

    void SelectRandomColor()
    {
        imgMask.color = colorMask[Random.Range(0, colorMask.Length)];
    }

    void SelectRandomDefectiveMask()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/Mask/");

        filter.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
