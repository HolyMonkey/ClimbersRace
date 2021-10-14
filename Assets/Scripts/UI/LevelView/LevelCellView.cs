using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelView
{
    public class LevelCellView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _highlightColor;
        [SerializeField] private Color _passedColor;
        [SerializeField] private Vector2 _baseScalling;
        [SerializeField] private Vector2 _highlightScalling;
        [SerializeField] private Vector2 _baseTextScalling;
        [SerializeField] private Vector2 _highlightTextScalling;

        public int TextValue { get; private set; }

        internal void SetTextValue(int value)
        {
            TextValue = value;
            _text.text = value.ToString();
        }

        internal void Highlight()
        {
            SetScalling(_highlightScalling, _highlightTextScalling);
            SetIconColor(_highlightColor);
        }

        internal void Downlight()
        {
            SetScalling(_baseScalling, _baseTextScalling);
            SetIconColor(_baseColor);
        }

        internal void MarkPassed()
        {
            SetScalling(_baseScalling, _baseTextScalling);
            SetIconColor(_passedColor);
        }

        private void SetScalling(Vector2 targetScalling, Vector2 targetTextScalling)
        {
            _icon.rectTransform.sizeDelta = targetScalling;
            _text.rectTransform.sizeDelta = targetTextScalling;
        }

        private void SetIconColor(Color targetColor)
        {
            _icon.color = targetColor;
        }
    }
}