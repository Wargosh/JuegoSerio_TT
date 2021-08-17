using UnityEngine;
using UnityEngine.UI;

public class ChildCharacter : MonoBehaviour
{
    public enum Child { Boy, Girl }

    public Child child = Child.Boy;

    public Image[] skinColor;
    public Image[] hair_girl;
    public Image[] hair_boy;
    public Image mouth;
    public Image accesories;

    void Start()
    {
        ShowCharacterAppearance();
    }

    public void ShowCharacterAppearance()
    {
        ResetImages();

        ShowSkinColor();

        if (child == Child.Boy)
        {
            int rr = Random.Range(0, hair_boy.Length);
            hair_boy[rr].gameObject.SetActive(true);
            hair_boy[rr].color = RandomHairColor();
        }
        else
        {
            int rr = Random.Range(0, hair_girl.Length);
            accesories.gameObject.SetActive(true);
            accesories.color = RandomColorAccesorieGirl();
            hair_girl[rr].gameObject.SetActive(true);
            hair_girl[rr].color = RandomHairColor();
        }
    }

    private void ShowSkinColor()
    {
        Color color = RandomSkinColor();
        for (int i = 0; i < skinColor.Length; i++)
            skinColor[i].color = color;
    }

    private Color RandomSkinColor()
    {
        int rr = Random.Range(0, 4);
        switch (rr)
        {
            case 0:
                return new Color(1f, 1f, 1f);
            case 1:
                return new Color(.5f, .4f, .3f);
            case 2:
                return new Color(1f, .99f, .88f);
            case 3:
                return new Color(1f, .91f, .92f);
            default:
                return new Color(1f, 1f, 1f);
        }
    }

    private Color RandomHairColor()
    {
        int rr = Random.Range(0, 4);
        switch (rr)
        {
            case 0: // café
                return new Color(.26f, .08f, 0f);
            case 1: // amarillo
                return new Color(1f, .87f, .39f);
            case 2: // negro
                return new Color(.1f, .1f, .1f);
            case 3: // rojo
                return new Color(.84f, .26f, .13f);
            default:// café
                return new Color(.26f, .08f, 0f);
        }
    }

    private Color RandomColorAccesorieGirl()
    {
        int rr = Random.Range(0, 6);
        switch (rr)
        {
            case 0: // rosa
                return new Color(.9f, .5f, .5f);
            case 1: // azul
                return new Color(0f, .4f, .6f);
            case 2: // verde
                return new Color(0f, .4f, .1f);
            case 3: // rojo
                return new Color(.6f, .08f, .08f);
            case 4: // lila
                return new Color(.8f, .5f, .7f);
            case 5: // violeta
                return new Color(.6f, .3f, .9f);
            default:// rosa
                return new Color(.9f, .5f, .5f);
        }
    }

    private void ResetImages()
    {
        for (int i = 0; i < hair_boy.Length; i++)
            hair_boy[i].gameObject.SetActive(false);
        for (int i = 0; i < hair_girl.Length; i++)
            hair_girl[i].gameObject.SetActive(false);
        accesories.gameObject.SetActive(false);
    }
}
