using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardManager : MonoBehaviour
{
    public static cardManager instance;
    [SerializeField]
    private List<Sprite> cardSprits;
    [SerializeField]
    private GameObject cardHolder, cardPrefab, dummyCardPrefab,parentHolder;
    private int k,childCount;
    private cardView selectedCard, previousCard, nextCard;
    private GameObject dummyCardobj;
    public cardView SelectedCard{ get => selectedCard; }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        for(int i=0;i<13;i++)
        {
            k = i;
            SpawnCard();
        }
    }
    public void SetSelectedCard(cardView card)
    {
        int selectedCardIndex = card.transform.GetSiblingIndex();
        selectedCard = card;
        selectedCard.childIndex = selectedCardIndex;
        GetDummycard().SetActive(true);
        GetDummycard().transform.SetSiblingIndex(selectedCardIndex);
        selectedCard.transform.SetParent(parentHolder.transform);
        childCount = cardHolder.transform.childCount;
        if(selectedCardIndex + 1 < childCount)
        {
            nextCard = cardHolder.transform.GetChild(selectedCardIndex + 1).GetComponent<cardView>();

        }
        if (selectedCardIndex - 1 >=0)
        {
            previousCard = cardHolder.transform.GetChild(selectedCardIndex - 1).GetComponent<cardView>();

        }
    }
    public void moveCard(Vector2 position)
    {
        if (selectedCard != null)
        {
            selectedCard.transform.position = position;
            checkWithNextCard();
            checkWithPreviousCard();
        }
    }
    public void ReleseCard()
    {
        if (selectedCard != null)
        {
            GetDummycard().SetActive(false);
            selectedCard.transform.SetParent(cardHolder.transform);
            
            if(Mathf.Abs(selectedCard.transform.position.y - dummyCardobj.transform.position.y) > 80)
            {
                GetDummycard().transform.SetParent(parentHolder.transform);
                selectedCard.transform.SetSiblingIndex(selectedCard.childIndex);
            }
            else
            {
                selectedCard.transform.SetSiblingIndex(GetDummycard().transform.GetSiblingIndex());
                GetDummycard().transform.SetParent(parentHolder.transform);
            }
            
            selectedCard = null;
        }
    }
    void checkWithNextCard()
    {
        if (nextCard != null)
        {
            if (selectedCard.transform.position.x > nextCard.transform.position.x)
            {
                int index = nextCard.transform.GetSiblingIndex();
                nextCard.transform.SetSiblingIndex(dummyCardobj.transform.GetSiblingIndex());
                dummyCardobj.transform.SetSiblingIndex(index);
                previousCard = nextCard;

                if (index + 1 < childCount)
                {
                    nextCard = cardHolder.transform.GetChild(index + 1).GetComponent<cardView>();

                }
                else
                {
                    nextCard = null;
                }
            }
        }
    }
    void checkWithPreviousCard()
    {
        if (previousCard != null)
        {
            if (selectedCard.transform.position.x < previousCard.transform.position.x)
            {
                int index = previousCard.transform.GetSiblingIndex();
                previousCard.transform.SetSiblingIndex(dummyCardobj.transform.GetSiblingIndex());
                dummyCardobj.transform.SetSiblingIndex(index);
                nextCard = previousCard;

                if (index - 1 >= 0)
                {
                    previousCard = cardHolder.transform.GetChild(index - 1).GetComponent<cardView>();

                }
                else
                {
                    previousCard = null;
                }
            }
        }

    }
    void SpawnCard()
    {
        GameObject card = Instantiate(cardPrefab);
        card.name = "card" + k;
        card.transform.SetParent(cardHolder.transform);
        card.GetComponent<cardView>().SetImg(cardSprits[k]);
    }
    GameObject GetDummycard()
    {
        if (dummyCardobj != null)
        {
            if(dummyCardobj.transform.parent!= cardHolder.transform)
            {
                dummyCardobj.transform.SetParent(cardHolder.transform);
            }
            return dummyCardobj;
        }
        else
        {
            dummyCardobj = Instantiate(dummyCardPrefab);
            dummyCardobj.name = "DummyCard";
            dummyCardobj.transform.SetParent(cardHolder.transform);
        }
        return dummyCardobj;
    }
}
