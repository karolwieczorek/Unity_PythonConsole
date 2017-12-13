using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityPythonConsole.Assets.Code { 
    public class Resizer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        public float minSize;
        public RectTransform resizedTransform;

        Vector3 prevMousePos;

        public void OnBeginDrag(PointerEventData eventData) {
            prevMousePos = Input.mousePosition;
        }

        public void OnDrag(PointerEventData eventData) {
            var mouseMovmentY = Input.mousePosition.y - prevMousePos.y;
            ChangeRectHeight(mouseMovmentY);

            prevMousePos = Input.mousePosition;
        }

        private void ChangeRectHeight(float mouseMovmentY) {
            var size = resizedTransform.sizeDelta;
            size.y = Mathf.Max(size.y + mouseMovmentY, minSize);
            resizedTransform.sizeDelta = size;
        }

        public void OnEndDrag(PointerEventData eventData) {
        }
    }
}