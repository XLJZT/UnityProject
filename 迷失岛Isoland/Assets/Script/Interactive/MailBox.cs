using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : Interactive
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D coll;

    public Sprite openSprite;
    private void OnEnable()
    {

        EventHandler.AfterScenceloadEvent += OnAfterScenceloadEvent;
    }
    private void OnDisable()
    {

        EventHandler.AfterScenceloadEvent -= OnAfterScenceloadEvent;
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

    }


    private void OnAfterScenceloadEvent()
    {
        if (!isDone)
        {

            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite = openSprite;
            coll.enabled = false;
        }
    }
    protected override void OnClickAction()
    {
        spriteRenderer.sprite = openSprite;
        coll.enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
