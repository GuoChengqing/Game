using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class MyTemp : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject canvas;
    public int currentCardNumber = 0;
    public int maxRotateAngle = 60;
    public float cardWidth;
    public float cardContainer = 0.2f;

    private List<GameObject> cardList = new List<GameObject>(10);

    // Start is called before the first frame update
    void Start()
    {
        cardWidth = cardPrefab.GetComponent<RectTransform>().sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentCardNumber < 10)
        {
            currentCardNumber++;

            GameObject card = Instantiate(cardPrefab, new Vector3(0, 50, 0), Quaternion.identity);
            card.transform.SetParent(canvas.transform, false);
            cardList.Add(card);

            UpdataCardList();
        }
    }

    private void UpdataCardList()
    {
        int rotetaStep = 120 / cardList.Count;
        float xDirectionStep = 0.4f * cardPrefab.GetComponent<RectTransform>().sizeDelta.x;
        float startX = (Screen.width - xDirectionStep * cardList.Count) / 2;
        float currentX = startX;
        float currentRotate = (cardList.Count / 2) * 12;

        for (int i = 0; i < cardList.Count; i++)
        {
            cardList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(currentX, 50);
            cardList[i].GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, currentRotate);
            currentX += xDirectionStep;
            currentRotate -= 12;
        }
    }
}
