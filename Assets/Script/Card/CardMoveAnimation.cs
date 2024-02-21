using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Card
{
    class CardMoveAnimation : MonoBehaviour
    {
        IEnumerator MoveToTargetAnimation(Transform target)
        {
            Vector3 originalPosition = transform.position;
            Transform originalParent = transform.parent;
            int originalIndex = transform.GetSiblingIndex();
            
            PrepareForAnimation(originalParent);
            
            yield return AnimateMoveToTarget(target);
            
            yield return AnimateMoveBack(originalPosition);

            ResetAfterAnimation(originalParent, originalIndex);
        }

        private void ResetAfterAnimation(Transform originalParent, int originalIndex)
        {
            transform.SetParent(originalParent);
            transform.SetSiblingIndex(originalIndex);
            originalParent.GetComponent<HorizontalLayoutGroup>().enabled = true;
        }
        private void PrepareForAnimation(Transform originalParent)
        {
            originalParent.GetComponent<HorizontalLayoutGroup>().enabled = false;
            transform.SetParent(GameObject.Find("Canvas").transform);
        }

        private IEnumerator AnimateMoveToTarget(Transform target)
        {
            transform.DOMove(target.position, .25f);
            yield return new WaitForSeconds(.25f);
        }

        private IEnumerator AnimateMoveBack(Vector3 originalPosition)
        {
            transform.DOMove(originalPosition, .25f);
            yield return new WaitForSeconds(.25f);
        }
        public void MovetoField(Transform field) => transform.DOMove(field.position, .5f);
        public void MovetoTarget(Transform target) => StartCoroutine(MoveToTargetAnimation(target));
    }
}
