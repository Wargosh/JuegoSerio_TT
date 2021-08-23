using UnityEngine;
using UnityEngine.UI;

public class ChildCharacter : MonoBehaviour
{
    public enum Child { Boy, Girl }

    public bool randomGender = true;
    public Child child = Child.Boy;
    [Header("Colores de piel")]
    public Color[] colorSkin;
    public Image[] skin;

    [Header("Colores de cabello")]
    public Color[] colorHair;
    public Image[] hair_girl;
    public Image[] hair_boy;

    [Header("Accesorios niña")]
    public Color[] colorAccesories;
    public Image[] accesories;

    public Image mouth;

    [Header("Camisa")]
    public Color[] colorShirt;
    public Image[] shirt;

    [Header("Corbatin")]
    public bool randomTie = true;
    public Image tie;

    [Header("Vestido")]
    public Color[] colorDress;
    public Image dress;

    [Header("Pantalon y Zapatos")]
    public Color[] colorPants;
    public Image pants;
    public Image[] shoes;

    void Start()
    {
        if (randomGender)
            SelectRandomGender();

        ShowCharacterAppearance();
    }

    private void SelectRandomGender()
    {
        if (Random.Range(0, 2) == 0)
            child = Child.Boy;
        else
            child = Child.Girl;
    }

    public void ShowCharacterAppearance()
    {
        ResetImages();

        ShowSkinColor();

        // color de camisa
        Color colorSh = colorShirt[Random.Range(0, colorShirt.Length)];
        for (int i = 0; i < shirt.Length; i++)
            shirt[i].color = colorSh;

        if (randomTie)
            RandomTie();

        if (child == Child.Boy)
        { // es niño
            // color de cabello
            int rr = Random.Range(0, hair_boy.Length);
            hair_boy[rr].gameObject.SetActive(true);
            hair_boy[rr].color = colorHair[Random.Range(0, colorHair.Length)];
            // Pantalon y zapatos
            Color _colorPants = colorPants[Random.Range(0, colorPants.Length)];
            pants.color = _colorPants;
            for (int i = 0; i < shoes.Length; i++)
                shoes[i].color = _colorPants;
        }
        else
        { // es niña
            // color de cabello
            int rr = Random.Range(0, hair_girl.Length);
            hair_girl[rr].gameObject.SetActive(true);
            hair_girl[rr].color = colorHair[Random.Range(0, colorHair.Length)];
            // color de accesorios
            Color colorAcc = colorAccesories[Random.Range(0, colorAccesories.Length)];
            for (int i = 0; i < accesories.Length; i++)
            {
                accesories[i].gameObject.SetActive(true);
                accesories[i].color = colorAcc;
            }
            // Zapatos
            for (int i = 0; i < shoes.Length; i++)
                shoes[i].color = colorAcc;
            // color de vestido
            dress.gameObject.SetActive(true);
            dress.color = colorDress[Random.Range(0, colorDress.Length)];
        }
    }

    private void RandomTie() {
        int rr = Random.Range(0, 2);
        if (rr == 0)
            tie.gameObject.SetActive(false);
        else
            tie.gameObject.SetActive(true);
    }

    private void ShowSkinColor()
    {
        Color color = colorSkin[Random.Range(0, colorSkin.Length)];
        for (int i = 0; i < skin.Length; i++)
            skin[i].color = color;
    }

    private void ResetImages()
    {
        for (int i = 0; i < hair_boy.Length; i++)
            hair_boy[i].gameObject.SetActive(false);
        for (int i = 0; i < hair_girl.Length; i++)
            hair_girl[i].gameObject.SetActive(false);
        for (int i = 0; i < accesories.Length; i++)
            accesories[i].gameObject.SetActive(false);

        dress.gameObject.SetActive(false);
    }
}
