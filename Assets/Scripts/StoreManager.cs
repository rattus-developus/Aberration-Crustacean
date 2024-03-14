using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    //Testing
    [SerializeField] bool raise;
    [SerializeField] bool lower;
    //


    [SerializeField] Vector3 loweredPos;
    [SerializeField] Vector3 raisedPos;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform slot1Tran;
    [SerializeField] Transform slot2Tran;
    [SerializeField] Transform slot3Tran;
    [SerializeField] TMP_Text slot1PriceText;
    [SerializeField] TMP_Text slot2PriceText;
    [SerializeField] TMP_Text slot3PriceText;
    [SerializeField] GameObject[] pickups;

    [SerializeField] AudioSource storeMusic;
    [SerializeField] AudioSource gameMusic;
    [SerializeField] AudioSource impact;
    [SerializeField] float musicFadeSpeed;

    bool raising;
    bool lowering;

    void Start()
    {
        RaiseStore();
    }

    public void LowerStore()
    {
        if(lowering || transform.position == loweredPos) return;

        storeMusic.Play();

        RefreshItems();
        GetComponent<Renderer>().enabled = true;
        lowering = true;
    }

    public void RaiseStore()
    {
        if(raising || transform.position == raisedPos) return;
        GetComponent<Collider>().enabled = false;
        slot1Tran.GetChild(0).GetComponent<Collider>().enabled = false;
        slot2Tran.GetChild(0).GetComponent<Collider>().enabled = false;
        slot3Tran.GetChild(0).GetComponent<Collider>().enabled = false;
        raising = true;
    }

    public void RefreshItems()
    {
        if(slot1Tran.childCount > 0) Destroy(slot1Tran.GetChild(0).gameObject);
        if(slot2Tran.childCount > 0) Destroy(slot2Tran.GetChild(0).gameObject);
        if(slot3Tran.childCount > 0) Destroy(slot3Tran.GetChild(0).gameObject);

        int slot1Index = Random.Range(0, pickups.Length);
        int slot2Index = Random.Range(0, pickups.Length);
        int slot3Index = Random.Range(0, pickups.Length);

        Pickup item1 = Instantiate(pickups[slot1Index]).GetComponent<Pickup>();
        Pickup item2 = Instantiate(pickups[slot2Index]).GetComponent<Pickup>();
        Pickup item3 = Instantiate(pickups[slot3Index]).GetComponent<Pickup>();

        item1.transform.SetParent(slot1Tran, true);
        item2.transform.SetParent(slot2Tran, true);
        item3.transform.SetParent(slot3Tran, true);

        item1.transform.localPosition = Vector3.zero;
        item2.transform.localPosition = Vector3.zero;
        item3.transform.localPosition = Vector3.zero;

        item1.transform.localScale *= 2f;
        item2.transform.localScale *= 2f;
        item3.transform.localScale *= 2f;

        item1.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        item2.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        item3.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        item1.isStoreItem = true;
        item2.isStoreItem = true;
        item3.isStoreItem = true;

        slot1PriceText.text = item1.price.ToString();
        slot2PriceText.text = item2.price.ToString();
        slot3PriceText.text = item3.price.ToString();
    }

    public void Update()
    {
        //Testing
        if(raise)
        {
            raise = false;
            RaiseStore();
        }
        if(lower)
        {
            lower = false;
            LowerStore();
        }
        //


        if(lowering)
        {
            if(gameMusic.volume > 0f) gameMusic.volume -= Time.deltaTime * musicFadeSpeed;

            transform.position -= new Vector3(0f, moveSpeed, 0f) * Time.deltaTime;
            if(transform.position.y <= loweredPos.y)
            {
                transform.position = loweredPos;
                lowering = false;
                GetComponent<Collider>().enabled = false;
                slot1Tran.GetChild(0).GetComponent<Collider>().enabled = true;
                slot2Tran.GetChild(0).GetComponent<Collider>().enabled = true;
                slot3Tran.GetChild(0).GetComponent<Collider>().enabled = true;
            }
        }
        else if(raising)
        {
            if(gameMusic.volume < 0.7f) gameMusic.volume += Time.deltaTime * musicFadeSpeed;

            transform.position += new Vector3(0f, moveSpeed, 0f) * Time.deltaTime;
            if(transform.position.y >= raisedPos.y)
            {
                storeMusic.Stop();
                raising = false;
                transform.position = raisedPos;
                GetComponent<Renderer>().enabled = false;

                Destroy(slot1Tran.GetChild(0).gameObject);
                Destroy(slot2Tran.GetChild(0).gameObject);
                Destroy(slot3Tran.GetChild(0).gameObject);
            }
        }
    }
}
