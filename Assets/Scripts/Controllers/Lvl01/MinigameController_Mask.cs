using System.Collections.Generic;
using UnityEngine;

public class MinigameController_Mask : MonoBehaviour
{
    public GameObject prefabMask;
    public GameObject prefabChild;

    public Transform objParent;

    public RectTransform[] positionsChild;
    public RectTransform[] positionsMask;

    private float _size;
    private List<int> listPositions = new List<int>() { 0,1,2,3,4,5,6,7,8,9 };

    public static MinigameController_Mask Instance { get; set; }

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Establecer un tamaño para el personaje y la mascarilla
        _size = Random.Range(0.35f, 1.1f);

        InstantiateImageCharacter();

        GenerateMultipleMasks();
    }

    public void InstantiateImageCharacter()
    {
        // Instanciar el objeto del personaje
        GameObject objChild = Instantiate(prefabChild);
        objChild.transform.SetParent(objParent);

        // Modifica el tamaño del personaje
        objChild.transform.localScale = new Vector3(_size, _size, 1f);

        // Establece la posicion del personaje
        int posChild = GetRandomPosition("Child");
        objChild.transform.position = positionsChild[posChild].position;
    }

    public void GenerateMultipleMasks()
    {
        InstantiateImageMask(true);

        int numMasks = Random.Range(0, 4);
        for (int i = 0; i < numMasks; i++)
            InstantiateImageMask(false);
    }

    private void InstantiateImageMask(bool isNewMask)
    {
        // Instanciar el objeto de la mascarilla
        GameObject objMask = Instantiate(prefabMask);
        objMask.transform.SetParent(objParent);

        // Modifica el tamaño de la mascarilla
        objMask.transform.localScale = new Vector3(_size, _size, 1f);

        // Modifica el estado de la mascarilla (no hace falta si es falso, ya que viene por defecto)
        if (isNewMask)
            objMask.GetComponent<MaskController>().isNewMask = isNewMask;
        
        // Evitar que la mascarilla se instancie en la misma ubicación
        // Establece la posicion de la mascarilla
        int posMask = GetRandomPosition("Mask");
        objMask.transform.position = positionsMask[posMask].position;
    }

    private int GetRandomPosition(string name)
    {
        int postition;
        if (name == "Child")
        {
            postition = Random.Range(0, positionsChild.Length);
            listPositions.Remove(postition);
            return postition;
        }
        else
        {
            postition = Random.Range(0, listPositions.Count);
            postition = listPositions[postition];
            listPositions.Remove(postition);
            return postition;
        }
    }
}