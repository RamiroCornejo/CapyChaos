using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleLocalization
{
    public class LocalizedString : MonoBehaviour
    {
        [SerializeField] string LocalizationKey;
        string val;

        public string Text
        {
            get
            {
                return val;
            }
        }

        public void Start()
        {
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        private void Localize()
        {
            val = LocalizationManager.Localize(LocalizationKey);
        }
    }
}
