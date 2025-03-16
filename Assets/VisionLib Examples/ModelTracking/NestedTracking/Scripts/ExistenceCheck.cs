using UnityEngine;
using UnityEngine.UI;
using Visometry.VisionLib.SDK.Core;

namespace Visometry.VisionLib.SDK.Examples
{
    /// <summary>
    /// This component allows to check the existence of the child anchor.
    /// A Text field will be adjusted according to the current state of the scene. Additionally, the
    /// material color of the child anchor will also represent the state of the existence check.
    /// </summary>
    /// @ingroup Examples
    [HelpURL(DocumentationLink.APIReferenceURI.Examples + "existence_check.html")]
    public class ExistenceCheck : MonoBehaviour
    {
        public Image sceneStateBackground;
        public Text sceneStateText;

        private readonly Color trackedColor = new Color(0, 1, 0, 0.5f);
        private readonly Color lostColor = new Color(1, 0, 0, 0.5f);

        public TrackingAnchor parentAnchor;
        public TrackingAnchor childAnchor;
        public Renderer existenceCheckObject;

        private void Update()
        {
            if (this.parentAnchor == null || this.childAnchor == null)
            {
                return;
            }
            if (!this.parentAnchor.IsTracking())
            {
                this.sceneStateText.text = "Car is not tracked";
                this.sceneStateBackground.color = this.lostColor;
                return;
            }
            if (!this.childAnchor.IsTracking())
            {
                this.sceneStateText.text = "Caravan is missing";
                this.sceneStateBackground.color = this.lostColor;
                this.existenceCheckObject.material.color = this.lostColor;
                return;
            }
            this.sceneStateText.text = "Caravan is present";
            this.sceneStateBackground.color = this.trackedColor;
            this.existenceCheckObject.material.color = this.trackedColor;
        }
    }
}
