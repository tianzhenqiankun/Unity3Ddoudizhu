using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;
using System;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour ,IPointerEnterHandler
{

    Card card;
    Image image;
    bool isSelect;
    Button button;

    public Card Card
    {
        get
        {
            return card;
        }

        set
        {
            card = value;
            SetImage();
        }
    }

    public bool IsSelect
    {
        get
        {
            return isSelect;
        }

        set
        {
            if (card.Character != CharacterType.Player || isSelect == value)
                return;
            if (value)
            {
                Sound.Instance.PlayEffect(Contsts.Select);
                transform.localPosition += Vector3.up * 30;
            }
            else
            {
                Sound.Instance.PlayEffect(Contsts.Select);
                transform.localPosition -= Vector3.up * 30;
            }
            isSelect = value;
        }
    }
    public void SetImage()
    {
        if (card.Character==CharacterType.Player||card.Character==CharacterType.Desk)
        {
            image.sprite = Resources.Load<Sprite>("Pokers/"+card.CardName);
        }
        else
        {
            image.sprite = Resources.Load<Sprite>("Pokers/FixedBack");
        }
    }
    public void SetImageAgain()
    {
        image.sprite = Resources.Load<Sprite>("Pokers/CardBack");
    }
    public void SetPostion(Transform parent,int index)
    {
        transform.SetParent(parent, false);
        transform.SetSiblingIndex(index);
        if (card.Character==CharacterType.Player||card.Character==CharacterType.Desk)
        {
            transform.localPosition = Vector3.right * index*55;
            if (IsSelect)
            {
                transform.localPosition += Vector3.up * 30;
            }
        }
        if (card.Character == CharacterType.ComputerLeft || card.Character == CharacterType.ComputerRight)
        {
            transform.localPosition = -Vector3.up * 16*index + Vector3.left * 16*index;
        }
    }

    public void OnSpawn()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Button_OnClick);
    }

    private void Button_OnClick()
    {
        //if (card.Character == CharacterType.Player)
        //{
        //    IsSelect = !IsSelect;
        //}
    }
    public void OnDespawn()
    {
        isSelect = false;
        image.sprite = null;
        card = null;
        button.onClick.RemoveListener(Button_OnClick);
    }

    public void Destroy()
    {
        LeanPool.Despawn(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            if (card.Character == CharacterType.Player)
            {
                IsSelect = !IsSelect;
            }
        }
    }
}
