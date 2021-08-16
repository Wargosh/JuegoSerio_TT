using UnityEngine;
using UnityEngine.UI;

public class ChildCharacter : MonoBehaviour
{
    public enum Child { Boy, Girl }

    public Child child = Child.Boy;

    public Image hair_girl;
    public Image hair_boy;
    public Image mouth;
    public Image accesories;

    void Start()
    {
        ShowCharacterAppearance();
    }

    void ShowCharacterAppearance()
    {
        ResetImages();

        if (child == Child.Boy)
        {
            hair_boy.gameObject.SetActive(true);
            hair_boy.color = RandomHairColor();
        }
        else
        {
            accesories.gameObject.SetActive(true);
            accesories.color = RandomColorAccesorieGirl();
            hair_girl.gameObject.SetActive(true);
            hair_girl.color = RandomHairColor();
        }
    }

    private Color RandomHairColor()
    {
        int rr = Random.Range(0, 5);
        switch (rr)
        {
            case 0: // café
                return new Color(.5f, .3f, .2f);
            case 1: // amarillo
                return new Color(1f, .87f, .39f);
            case 2: // negro
                return new Color(.1f, .1f, .1f);
            case 3: // rojo
                return new Color(.64f, .12f, .01f);
            case 4: // marrón
                return new Color(.6f, .3f, .1f);
            default:// café
                return new Color(.5f, .3f, .2f);
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
        hair_boy.gameObject.SetActive(false);
        hair_girl.gameObject.SetActive(false);
        accesories.gameObject.SetActive(false);
    }
}
