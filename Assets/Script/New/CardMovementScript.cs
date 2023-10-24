using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovementScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Camera _mainCamera;
    private Vector3 _offset;
    private Transform _defaultParent,_defaultTempCardParent;
    GameObject TempCardGO;
    private bool _isDraggable;

    public Transform DefaultParent
    {
        get
        {
            return _defaultParent;
        }
        set
        {
            _defaultParent = value;
        }
    }

    public Transform DefaultTempCardParent
    {
        get
        {
            return _defaultTempCardParent;
        }
        set
        {
            _defaultTempCardParent = value;
        }
    }

    private void Awake()
    {
        _mainCamera = Camera.allCameras[0];
        TempCardGO = GameObject.Find("TempCardGO");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //calculate offset
        _offset = transform.position - _mainCamera.ScreenToWorldPoint(eventData.position);

        //store current parent
        _defaultParent = _defaultTempCardParent = transform.parent;

        _isDraggable = _defaultParent.GetComponent<DropPlaceScript>().Type == FieldType.PLAYER_HAND;

        if (!_isDraggable)
            return;

        TempCardGO.transform.SetParent(_defaultParent);
        TempCardGO.transform.SetSiblingIndex(transform.GetSiblingIndex());

        //dissatach from previous parent
        transform.SetParent(_defaultParent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDraggable)
            return;

        Vector3 newPos = _mainCamera.ScreenToWorldPoint(eventData.position);
        
        transform.position = newPos + _offset;

        if (TempCardGO.transform.parent != _defaultTempCardParent)
            TempCardGO.transform.SetParent(_defaultTempCardParent);

        CheckPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isDraggable)
            return;

        transform.SetParent(_defaultParent);

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetSiblingIndex(TempCardGO.transform.GetSiblingIndex());
        TempCardGO.transform.SetParent(GameObject.Find("Canvas").transform);
        TempCardGO.transform.localPosition = new Vector3(3055, 0);
    }

    private void CheckPosition()
    {
        int newIndex = _defaultTempCardParent.childCount;

        for(int i = 0; i < _defaultTempCardParent.childCount; i ++)
        {
            if(transform.position.x < _defaultTempCardParent.GetChild(i).position.x)
            {
                newIndex = i;

                if (TempCardGO.transform.GetSiblingIndex() < newIndex)
                {
                    newIndex--;
                }

                break;
            }
        }

        TempCardGO.transform.SetSiblingIndex(newIndex);
    }
}
