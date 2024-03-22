using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolGachaManager : MonoBehaviour
{
    [System.Serializable]
    public class GachaList
    {
        public List<CharacterStat> RateUpLegendaryPool;
        public List<CharacterStat> LegendaryPool;
        public List<CharacterStat> EpicPool;
    }

    [Header("LIST")]
    [SerializeField] private GachaList characterNormalPool;
    [SerializeField] private GachaList characterRateup1Pool;
    [SerializeField] private GachaList characterRateup2Pool;
    [SerializeField] private GachaList characterRateup3Pool;

    [SerializeField] private GachaList petNormalPool;
    [SerializeField] private GachaList petRateup1Pool;
    [SerializeField] private GachaList petRateup2Pool;

    [SerializeField] private List<GameObject> normalPool;

    [Space(8)]
    [Header("PAGE LIST GACHA")]
    public GameObject charNormalPanel;
    [SerializeField] private GameObject charRate1Panel;
    [SerializeField] private GameObject charRate2Panel;
    [SerializeField] private GameObject charRate3Panel;

    public GameObject petNormalPanel;
    [SerializeField] private GameObject petRate1Panel;
    [SerializeField] private GameObject petRate2Panel;

    [Space(8)]
    [Header("OTHER")]
    [SerializeField] private List<GameObject> cardSpoil;
    [Space(8)]
    [SerializeField] private Transform parentCharacter;
    [SerializeField] private Transform parentSpoil;

    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject spoilPanel;
    [SerializeField] private GameObject characterPanel;

    public GameObject[] characterShow;

    int countOfGarunteeChar;
    int countOfGarunteePet;
    public bool isOpenx10;
    private void OnEnable()
    {
        CloseAllPages();
        charNormalPanel.SetActive(true);
    }
    private void Start()
    {
        countOfGarunteeChar = PlayerPrefs.GetInt(Key.KEY_GARUNTEEDRAWCHAR, 0);
        countOfGarunteePet = PlayerPrefs.GetInt(Key.KEY_GARUNTEEDRAWPET, 0); 
    }

    private void SaveTotalDraws()
    {
        PlayerPrefs.SetInt(Key.KEY_GARUNTEEDRAWCHAR, countOfGarunteeChar);
        PlayerPrefs.SetInt(Key.KEY_GARUNTEEDRAWPET, countOfGarunteePet);
        PlayerPrefs.Save();
    }

    public void OnDisablePage()
    {
        resultPanel.SetActive(false);
        spoilPanel.SetActive(false);
        characterPanel.SetActive(false);

        foreach (GameObject obj in characterShow)
        {
            obj.SetActive(true);
        }

        DestroyChildren(parentSpoil);
        DestroyChildren(parentCharacter);

        StopAllCoroutines();
    }

    private void DestroyChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    public void ResultReward(CharacterStat.Tier r)
    {
        resultPanel.SetActive(true);

        HandleResultPanelChar(r, charNormalPanel, characterNormalPool);
        HandleResultPanelRateupChar(r, charRate1Panel, characterRateup1Pool);
        HandleResultPanelRateupChar(r, charRate2Panel, characterRateup2Pool);
        HandleResultPanelRateupChar(r, charRate3Panel, characterRateup3Pool);

        HandleResultPanelPet(r, petNormalPanel, petNormalPool);
        HandleResultPanelRateupPet(r, petRate1Panel, petRateup1Pool);
        HandleResultPanelRateupPet(r, petRate2Panel, petRateup2Pool);

    }
   

    private void HandleResultPanelChar(CharacterStat.Tier r, GameObject panel, GachaList gachaList)
    {
        if (panel.activeInHierarchy)
        {
            switch (r)
            {
                case CharacterStat.Tier.None:
                    CrateShowResultSpoil(cardSpoil[4]);
                    StartCoroutine(SpoilCard());
                    CrateShowResultNormal();
                    break;
                case CharacterStat.Tier.Epic:
                    CrateShowResultSpoil(cardSpoil[1]);
                    StartCoroutine(SpoilCard());
                    CrateShowResult(gachaList.EpicPool);
                    break;
                case CharacterStat.Tier.Legend:
                    CrateShowResultSpoil(cardSpoil[0]);
                    StartCoroutine(SpoilCard());
                    CrateShowResult(gachaList.LegendaryPool);
                    break;
            }
        }
    }
    
    private void HandleResultPanelRateupChar(CharacterStat.Tier r, GameObject panel, GachaList gachaList)
    {
        foreach(GameObject obj in characterShow)
        {
            obj.SetActive(false);
        }

        if (panel.activeInHierarchy)
        {
            switch (r)
            {
                case CharacterStat.Tier.None:
                    CrateShowResultSpoil(cardSpoil[4]);
                    StartCoroutine(SpoilCard());
                    CrateShowResultNormal();
                    break;
                case CharacterStat.Tier.Epic:
                    CrateShowResultSpoil(cardSpoil[1]);
                    StartCoroutine(SpoilCard());
                    CrateShowResult(gachaList.EpicPool);
                    break;
                case CharacterStat.Tier.Legend:
                    CrateShowResultSpoil(cardSpoil[0]);
                    StartCoroutine(SpoilCard());

                    if (countOfGarunteeChar >= 1)
                    {
                        CrateShowResult(gachaList.RateUpLegendaryPool);
                        countOfGarunteeChar = 0;
                        SaveTotalDraws();
                    }
                    else
                    {
                        CrateShowResult(gachaList.LegendaryPool);
                        countOfGarunteeChar++;
                        SaveTotalDraws();
                    }
                    break;
            }
        }
    }

    private void HandleResultPanelPet(CharacterStat.Tier r, GameObject panel, GachaList gachaList)
    {
        if (panel.activeInHierarchy)
        {
            switch (r)
            {
                case CharacterStat.Tier.None:
                    CrateShowResultSpoil(cardSpoil[4]);
                    StartCoroutine(SpoilCard());
                    CrateShowResultNormal();
                    break;
                case CharacterStat.Tier.Epic:
                    CrateShowResultSpoil(cardSpoil[3]);
                    StartCoroutine(SpoilCard());
                    CrateShowResult(gachaList.EpicPool);
                    break;
                case CharacterStat.Tier.Legend:
                    CrateShowResultSpoil(cardSpoil[2]);
                    StartCoroutine(SpoilCard());
                    CrateShowResult(gachaList.LegendaryPool);
                    break;
            }
        }
    }
    private void HandleResultPanelRateupPet(CharacterStat.Tier r, GameObject panel, GachaList gachaList)
    {
        if (panel.activeInHierarchy)
        {
            switch (r)
            {
                case CharacterStat.Tier.None:
                    CrateShowResultSpoil(cardSpoil[4]);
                    StartCoroutine(SpoilCard());
                    CrateShowResultNormal();
                    break;
                case CharacterStat.Tier.Epic:
                    CrateShowResultSpoil(cardSpoil[3]);
                    StartCoroutine(SpoilCard());
                    CrateShowResult(gachaList.EpicPool);
                    break;
                case CharacterStat.Tier.Legend:
                    CrateShowResultSpoil(cardSpoil[2]);
                    StartCoroutine(SpoilCard());

                    if (countOfGarunteePet >= 1)
                    {
                        CrateShowResult(gachaList.RateUpLegendaryPool);
                        countOfGarunteePet = 0;
                        SaveTotalDraws();
                    }
                    else
                    {
                        CrateShowResult(gachaList.LegendaryPool);
                        countOfGarunteePet++;
                        SaveTotalDraws();
                    }
                    break;
            }
        }
    }

    private void CrateShowResultSpoil(GameObject spoilPrefab)
    {
        GameObject go = Instantiate(spoilPrefab, parentSpoil.position, Quaternion.identity);
        go.transform.SetParent(parentSpoil.transform);
        go.transform.localScale = Vector3.one;
    }
    private void CrateShowResult(List<CharacterStat> characterPool)
    {
        GameObject go = Instantiate(characterPool[Random.Range(0, characterPool.Count)].GachaCharacter,
            parentCharacter.position, Quaternion.identity);
        go.transform.SetParent(parentCharacter.transform);
        go.transform.localScale = Vector3.one;


        CheckRateResult(characterPool);
    }
    private void CheckRateResult(List<CharacterStat> characterPool)
    {
        if (characterPool != null && characterPool.Count > 0)
        {
            for (int i = 0; i < characterPool.Count; i++)
            {
                float chance = 1.0f / characterPool.Count;
                Debug.Log($"Character {i + 1}: {chance * 100}% chance");
            }
        }
    }
    private void CrateShowResultNormal()
    {
        GameObject go = Instantiate(normalPool[Random.Range(0, normalPool.Count)],
            parentCharacter.position, Quaternion.identity);
        go.transform.SetParent(parentCharacter.transform);
        go.transform.localScale = new Vector3(1, 1, 1);
    }
    IEnumerator SpoilCard()
    {
        spoilPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        spoilPanel.SetActive(false);
        characterPanel.SetActive(true);
        SoundManager.Instance.OnOpenGachaSound();

    }
    public void OpenPage(GameObject go)
    {
        CloseAllPages();
        go.SetActive(true);
    }
    private void CloseAllPages()
    {
        charNormalPanel.SetActive(false);
        charRate1Panel.SetActive(false);
        charRate2Panel.SetActive(false);
        charRate3Panel.SetActive(false);
        petNormalPanel.SetActive(false);
        petRate1Panel.SetActive(false);
        petRate2Panel.SetActive(false);
    }
}
