using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI
{
    public enum TypeAnimationButton
    {
        None,
        Scale,
        Move,
        Rotate,

    }

    public class SimpleAnimButton : Selectable, IPointerDownHandler, IEventSystemHandler, IPointerClickHandler
    {
        [SerializeField] private TypeAnimationButton _typeAnim = TypeAnimationButton.None;
        [SerializeField] private Vector3 _animValue;
        [SerializeField] private float _duration;
        [SerializeField] private UnityEvent _onClick;
        private Tween _animTween;

        public override void OnPointerDown(PointerEventData eventData)
        {
            switch (_typeAnim)
            {
                case TypeAnimationButton.Scale:
                    FirstPharseScaleAnimButton(transform);
                    break;
                case TypeAnimationButton.Move:
                    FirstPharseMoveAnimButton(transform);
                    break;
                case TypeAnimationButton.Rotate:
                    FirstPharseRotateAnim(transform);
                    break;
                default:
                    break;
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            switch (_typeAnim)
            {
                case TypeAnimationButton.Scale:
                    SecondPharseScaleAnimButton(transform);
                    break;
                case TypeAnimationButton.Move:
                    SecondPharseMoveAnimButton(transform);
                    break;
                case TypeAnimationButton.Rotate:
                    SecondPharseRotateAnim(transform);
                    break;
                default:
                    break;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Press();
            }
        }

        private void Press()
        {
            if (IsActive() && IsInteractable())
            {
                UISystemProfilerApi.AddMarker("Button.onClick", this);
                _onClick.Invoke();
                MyTools.Sound.MySoundManager.Instance.PlaySound(MyTools.Sound._SoundType.ClickUIButton);
            }
        }

        protected virtual void FirstPharseScaleAnimButton(Transform transformScaler, params Action[] callBack)
        {
            _animTween = transformScaler.DOScale(_animValue, _duration / 2).OnComplete(() =>
            {
                foreach (var action in callBack)
                {
                    action?.Invoke();
                }
            });
        }

        protected virtual void SecondPharseScaleAnimButton(Transform transformScaler, params Action[] callBack)
        {
            _animTween.Kill();
            _animTween = transformScaler.DOScale(Vector3.one, _duration / 2).OnComplete(() =>
            {
                foreach (var action in callBack)
                {
                    action?.Invoke();
                }
            });
        }

        protected virtual void FirstPharseMoveAnimButton(Transform transformMover, params Action[] callBack)
        {
            _animTween = transformMover.DOMove(transformMover.position - _animValue, _duration / 2).OnComplete(() =>
            {
                foreach (var action in callBack)
                {
                    action?.Invoke();
                }
            });
        }

        protected virtual void SecondPharseMoveAnimButton(Transform transformMover, params Action[] callBack)
        {
            _animTween.Kill();
            _animTween = transformMover.DOMove(transformMover.position + _animValue, _duration / 2).OnComplete(() =>
            {
                foreach (var action in callBack)
                {
                    action?.Invoke();
                }
            });
        }

        protected virtual void FirstPharseRotateAnim(Transform transformMover, params Action[] callBacks){
            _animTween = transformMover.DORotate(_animValue, _duration / 2).OnComplete(() =>
            {
                foreach (var action in callBacks)
                {
                    action?.Invoke();
                }
            });
        }

        protected virtual void SecondPharseRotateAnim(Transform transformMover, params Action[] callBacks){
            _animTween.Kill();
            _animTween = transformMover.DORotate(Vector3.zero, _duration / 2).OnComplete(() =>
            {
                foreach (var action in callBacks)
                {
                    action?.Invoke();
                }
            });
        }

        public UnityEvent OnClick => _onClick;
    }
}