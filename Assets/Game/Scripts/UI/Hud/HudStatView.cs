using TMPro;
using UnityEngine;

namespace Game.UI.Hud
{
    public class HudStatView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void SetText(string value) => _text.text = value;
    }
}
