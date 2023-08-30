using _Scripts.Counter;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject hasProgressGameObject;
        [SerializeField] private Image barImage;
        [SerializeField] private GameObject[] bars;
        private IHasProgress hasProgress;
        private bool isActive = true;
        private void Start()
        {
            
            hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
            if (hasProgress == null)
            {
                Debug.LogError($"Game Object {hasProgressGameObject} does not have a component that implements IHasProgress" );
                return;
            }
            hasProgress.OnProgressChange += HasProgress_OnProgressChange;
            barImage.fillAmount = 0;
            SetActiveUI(false);
        }

        private void HasProgress_OnProgressChange(object sender, IHasProgress.OnProgressChangedEventArgs e)
        {

            if (e.ProgressNormalized < 1)
            {
                barImage.fillAmount = e.ProgressNormalized;
                SetActiveUI(true);
            }
            else
            {
                SetActiveUI(false);
            }
        }
        private void SetActiveUI(bool active)
        {
            if(this.isActive == active) return;
            this.isActive = active;
            foreach(GameObject o in bars)
            {
                o.SetActive(isActive);
            }
        }
    }
}
