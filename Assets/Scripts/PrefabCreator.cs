using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PrefabCreator : MonoBehaviour
{
    #region Variáveis Públicas (Arrastar no Inspetor)

    [Header("Peças do Foguete")]

    public GameObject[] rocketTop;
    public GameObject[] rocketMiddle;
    public GameObject[] rocketBottom;

    [Header("Stats das Peças")]
    public RocketStats[] topPartsStats;
    public RocketStats[] middlePartsStats;
    public RocketStats[] bottomPartsStats;

    [Header("UI - Tabela de Stats")]
    public Text textoPeso;
    public Text textoAerodinamica;
    public Text textoEstabilidade;
    public Text textoDistanciaPotencial;

    #endregion

    #region Variáveis Privadas (Lógica Interna)

    private ARTrackedImageManager arTrackedImageManager;

    private GameObject spawnedRocket;
    private GameObject[] spawnedTopParts;
    private GameObject[] spawnedMiddleParts;
    private GameObject[] spawnedBottomParts;

    private int currentTopIndex;
    private int currentMiddleIndex;
    private int currentBottomIndex;

    #endregion


    #region Ciclo de Vida da Unity e Eventos AR

    private void Awake()
    {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }
    private void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    private void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            if (spawnedRocket == null)
            {
                AssembleRocket(trackedImage);
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (spawnedRocket != null)
            {
                spawnedRocket.transform.SetPositionAndRotation(trackedImage.transform.position, trackedImage.transform.rotation);
                spawnedRocket.SetActive(trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking);
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            if (spawnedRocket != null)
            {
                Destroy(spawnedRocket);
                spawnedRocket = null;
            }
        }
    }

    #endregion

    #region Lógica Principal de Montagem

    private void AssembleRocket(ARTrackedImage trackedImage)
    {
        spawnedRocket = new GameObject("FogueteMontado");
        spawnedRocket.transform.SetParent(trackedImage.transform, false);

        spawnedTopParts = InstantiateParts(rocketTop, spawnedRocket.transform);
        spawnedMiddleParts = InstantiateParts(rocketMiddle, spawnedRocket.transform);
        spawnedBottomParts = InstantiateParts(rocketBottom, spawnedRocket.transform);

        currentTopIndex = 0;
        currentMiddleIndex = 0;
        currentBottomIndex = 0;

        spawnedTopParts[currentTopIndex].SetActive(true);
        spawnedMiddleParts[currentMiddleIndex].SetActive(true);
        spawnedBottomParts[currentBottomIndex].SetActive(true);

        UpdateStatsUI();
    }

    private GameObject[] InstantiateParts(GameObject[] partPrefabs, Transform parent)
    {
        GameObject[] instances = new GameObject[partPrefabs.Length];
        for (int i = 0; i < partPrefabs.Length; i++)
        {
            instances[i] = Instantiate(partPrefabs[i], parent);
            instances[i].SetActive(false);
        }
        return instances;
    }

    #endregion

    #region Funções Públicas para os Botões da UI

    public void NextTopPart() => ChangePart(ref currentTopIndex, spawnedTopParts, 1);
    public void PreviousTopPart() => ChangePart(ref currentTopIndex, spawnedTopParts, -1);

    public void NextMiddlePart() => ChangePart(ref currentMiddleIndex, spawnedMiddleParts, 1);
    public void PreviousMiddlePart() => ChangePart(ref currentMiddleIndex, spawnedMiddleParts, -1);

    public void NextBottomPart() => ChangePart(ref currentBottomIndex, spawnedBottomParts, 1);
    public void PreviousBottomPart() => ChangePart(ref currentBottomIndex, spawnedBottomParts, -1);

    #endregion

    #region Lógica de Troca e Stats

    private void ChangePart(ref int currentIndex, GameObject[] spawnedParts, int direction)
    {
        if (spawnedRocket == null || !spawnedRocket.activeInHierarchy) return;

        spawnedParts[currentIndex].SetActive(false);

        currentIndex += direction;
        int totalParts = spawnedParts.Length;
        if (currentIndex < 0) currentIndex = totalParts - 1;
        if (currentIndex >= totalParts) currentIndex = 0;

        spawnedParts[currentIndex].SetActive(true);

        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        float pesoTotal = topPartsStats[currentTopIndex].peso + middlePartsStats[currentMiddleIndex].peso + bottomPartsStats[currentBottomIndex].peso;
        float aeroTotal = topPartsStats[currentTopIndex].aerodinamica + middlePartsStats[currentMiddleIndex].aerodinamica + bottomPartsStats[currentBottomIndex].aerodinamica;
        float estabilidadeTotal = topPartsStats[currentTopIndex].estabilidade + middlePartsStats[currentMiddleIndex].estabilidade + bottomPartsStats[currentBottomIndex].estabilidade;

        float distanciaPotencial = (aeroTotal * 2) + estabilidadeTotal - (pesoTotal * 1.5f);

        textoPeso.text = $"Peso: {pesoTotal:F1}";
        textoAerodinamica.text = $"Aerodinâmica: {aeroTotal:F1}";
        textoEstabilidade.text = $"Estabilidade: {estabilidadeTotal:F1}";
        textoDistanciaPotencial.text = $"Potencial: {distanciaPotencial:F0}";
    }

    #endregion


}
