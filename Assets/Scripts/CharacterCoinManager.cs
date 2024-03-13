using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterCoinManager : MonoBehaviour
{
    public int coins = 0;
    TMP_Text coinsText;
    [SerializeField] AudioSource coinAudio;

    void Awake()
    {
        coinsText = GameObject.Find("CoinsText").GetComponent<TMP_Text>();
    }

    public void GetCoin()
    {
        coins += 1;
        coinAudio.pitch = Random.Range(0.9f, 1.1f);
        coinAudio.Play();
    }

    public void AttemptPurchase(int price)
    {
        if(price > coins)
        {
            //fail
        }
        else
        {
            //succeed
            coins -= price;
        }
    }

    void Update()
    {
        coinsText.text = coins.ToString();
    }
}
