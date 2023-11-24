using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class SlotController : ControllerBaseModel
{
    [SerializeField] private int objectCount;
    [SerializeField] private float xOffset;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Transform draggableParent;
    [SerializeField] private SlotType slotType;
    [ShowInInspector] private List<IDraggable> Draggables;

    [Button]
    public void Editor_SpawnRope()
    {
        if (Draggables.Count >= objectCount) return;
        Rope rope = RopeFactory.Instance.SpawnObject<Rope>();
        IDraggable draggable = rope.GetComponent<IDraggable>();
        AddDraggable(draggable);
    }

    [Button]
    public void Editor_SpawnProduct(ProductTypes type)
    {
        if (Draggables.Count >= objectCount) return;
        Product product = ProductFactory.Instance.SpawnObject<Product>(type);
        IDraggable draggable = product.GetComponent<IDraggable>();
        AddDraggable(draggable);
    }

    private void Start()
    {
        Draggables = new List<IDraggable>();
        for (int i = 0; i < draggableParent.childCount; i++)
        {
            Draggables.Add(draggableParent.GetChild(i).GetComponent<IDraggable>());
        }

        ArrangeDraggables(PositioningType.Instant);
    }

    private void ArrangeDraggables(PositioningType type)
    {
        for (int i = 0; i < Draggables.Count; i++)
        {
            // Vector3 position = startPosition + new Vector3(i * xOffset, 0f, 0f);
            Vector3 position = transform.localPosition + new Vector3(i * xOffset, 0f, 0f) + startPosition;
            switch (type)
            {
                case PositioningType.Instant:
                    ((MonoBehaviour)Draggables[i]).transform.position = position;
                    break;
                case PositioningType.Slide:
                    ((MonoBehaviour)Draggables[i]).transform.DOMove(position, 0.15f);
                    break;
            }
        }
    }

    private void AddDraggable(IDraggable newDraggable)
    {
        if (Draggables.Count >= objectCount) return;
        Draggables.Add(newDraggable);
        ArrangeDraggables(PositioningType.Instant);
    }

    private void RemoveDraggable(IDraggable draggable)
    {
        if (draggable != null && Draggables.Contains(draggable))
        {
            Draggables.Remove(draggable);
            ArrangeDraggables(PositioningType.Slide);
        }
    }

    private void OnEnable()
    {
        SlotActions.OnDraggableSpawned += AddDraggable;
        SlotActions.OnDraggableUsed += RemoveDraggable;
        if (slotType == SlotType.Product)
            SewingActions.OnProductReached += AddDraggable;
    }

    private void OnDisable()
    {
        SlotActions.OnDraggableSpawned -= AddDraggable;
        SlotActions.OnDraggableUsed -= RemoveDraggable;
        if (slotType == SlotType.Product)
            SewingActions.OnProductReached -= AddDraggable;
    }
}

public enum PositioningType
{
    Instant,
    Slide
}

public enum SlotType
{
    Rope,
    Product
}