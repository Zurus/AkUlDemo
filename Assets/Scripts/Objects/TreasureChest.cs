using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TreasureChest : Interactable
{

    public Item contents;
    public Inverntory playerInverntory;
    public bool isOpen;
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!isOpen)
            {
                OpenChest();
            } else
            {
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        //Dialog window on
        //
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescript;
        //Debug.Log(contents.itemDescript);
        //Debug.Log(dialogText.text);
        playerInverntory.AddItem(contents);
        playerInverntory.currentItem = contents;
        raiseItem.Raise();
        context.Raise();
        isOpen = true;
        anim.SetBool("opened", true);
    }

    public void ChestAlreadyOpen()
    {
        dialogBox.SetActive(false);
        //playerInverntory.currentItem = null;
        raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = false;
        }
    }

}
