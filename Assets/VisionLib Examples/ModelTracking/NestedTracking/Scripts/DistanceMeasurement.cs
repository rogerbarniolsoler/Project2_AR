using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Visometry.Helpers;
using Visometry.VisionLib.SDK.Core;

namespace Visometry.VisionLib.SDK.Examples
{
    /// <summary>
    /// This component will draw a line between two points and calculate the distance between them.
    /// It requires two tracking anchors which will enable the calculation after both are tracked.
    /// The distance will be measured between the two reference points. 
    /// </summary>
    /// @ingroup Examples
    [HelpURL(DocumentationLink.APIReferenceURI.Examples + "distance_measurement.html")]
    public class DistanceMeasurement : MonoBehaviour
    {
        public Text sceneStateText;
        public TrackingAnchor parentAnchor;
        public TrackingAnchor childAnchor;

        public Transform parentReferencePoint;
        public Transform childReferencePoint;
        public TextMeshPro distanceText;

        private LineRenderer lineRenderer = null;

        private void Start()
        {
            this.lineRenderer = this.gameObject.AddComponent<LineRenderer>();
            this.lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            this.lineRenderer.widthMultiplier = 0.001f;
            this.lineRenderer.positionCount = 2;
            this.lineRenderer.numCapVertices = 5;
            this.lineRenderer.startColor = Color.red;
            this.lineRenderer.endColor = Color.red;
        }

        private void Update()
        {
            if (this.parentAnchor == null || this.childAnchor == null)
            {
                return;
            }
            if (!this.parentAnchor.IsTracking())
            {
                this.sceneStateText.text = "Track the car";
                ResetGUI();
                return;
            }
            if (!this.childAnchor.IsTracking())
            {
                this.sceneStateText.text = "Track the caravan";
                ResetGUI();
                return;
            }
            UpdateGUI();
        }

        private void ResetGUI()
        {
            this.lineRenderer.enabled = false;
            this.distanceText.text = "";
        }

        private void UpdateGUI()
        {
            var parentPosition = this.parentReferencePoint.position;
            var childPosition = this.childReferencePoint.position;

            // Draw line between 
            this.lineRenderer.enabled = true;
            this.lineRenderer.SetPosition(0, parentPosition);
            this.lineRenderer.SetPosition(1, childPosition);

            var distanceVector = childPosition - parentPosition;
            var textPosition = (parentPosition + childPosition) / 2;

            var cameraToText = textPosition - CameraProvider.MainCamera.transform.position;
            var modelUp = this.parentReferencePoint.up;
            var textRotation = Quaternion.LookRotation(cameraToText, modelUp);

            this.distanceText.transform.position = textPosition;
            this.distanceText.transform.rotation = textRotation;
            this.distanceText.text = $"{distanceVector.magnitude:f2} m";
        }
    }
}
