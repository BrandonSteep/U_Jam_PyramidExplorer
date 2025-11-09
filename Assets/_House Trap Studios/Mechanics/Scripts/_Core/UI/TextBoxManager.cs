using DG.Tweening;
using TMPro;
using UnityEngine;

namespace HouseTrap.Core {
    public class TextBoxManager : MonoBehaviour {
        public static TextBoxManager Instance;
        // UITools tools = new UITools();

        private static List<UI.UITextFade> activePopups = new();

        private static Tween typewriterTween;

        private void Awake() {
            Instance = this;
        }

        #region Create Text Boxes

        public static GameObject CreateNewHoveringTextBox_TypewriterFX(string _text, GameObject _textBoxPrefab, Transform _transform, float _typeSpeed) {
            var textAsset = Instantiate(_textBoxPrefab, _transform.position, Quaternion.identity, Instance.transform)
                .GetComponent<TMP_Text>();

            Instance.ClosePopups();

            var fader = textAsset.GetComponent<UI.UITextFade>();
            activePopups.Add(fader);

            var blankText = "";

            typewriterTween = DOTween.To(() => blankText, x => blankText = x, _text, _text.Length / _typeSpeed)
                .OnUpdate(() => { textAsset.text = blankText; });
            if (!fader.GetFadeOnEnable()) {
                typewriterTween.OnComplete(fader.FadeAfterDelay);
            }

            var hover = textAsset.transform.GetComponent<UIHoverAtWorldPosition>();
            if (hover) {
                hover.SetHoverTransform(_transform);
            }

            return textAsset.gameObject;
        }


        // For Text Boxes that Remain in Place and aren't added to the Fade List eg. Item Names
        public static GameObject CreateNewStaticTextBox_TypewriterFX(string _text, GameObject _textBoxPrefab, float _typeSpeed) {
            var textAsset = Instantiate(_textBoxPrefab, Instance.transform).GetComponent<TMP_Text>();

            var blankText = "";

            typewriterTween = DOTween.To(() => blankText, _x => blankText = _x, _text, _text.Length / _typeSpeed)
                .OnUpdate(() => { textAsset.text = blankText; });

            return textAsset.gameObject;
        }

        #endregion



        #region Manage Text Boxes

        public void ClosePopups() {
            if (activePopups.Count < 1) return;
            foreach (var i in activePopups) {
                i.Fade();
            }
        }

        #endregion
    }
}