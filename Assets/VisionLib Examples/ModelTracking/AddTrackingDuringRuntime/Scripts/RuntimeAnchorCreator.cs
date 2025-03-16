using System;
using UnityEngine;
using Visometry.VisionLib.SDK.Core;
using Visometry.VisionLib.SDK.Core.Details;

namespace Visometry.VisionLib.SDK.Examples
{
    /// <summary>
    /// A utility class for managing creation and removal of <see cref="TrackingAnchor"/> instances
    /// during runtime.
    /// </summary>
    /// @ingroup Examples
    [HelpURL(DocumentationLink.APIReferenceURI.Examples + "runtime_anchor_creator.html")]
    public class RuntimeAnchorCreator : MonoBehaviour
    {
        /// <summary>
        /// The GameObject representing the tracking target.
        /// This GameObject requires a MeshFilter on at least one (child) GameObject.
        /// </summary>
        public GameObject trackingTarget;

        /// <summary>
        /// Adds a <see cref="TrackingAnchor"/> as a parent to the <see cref="trackingTarget"/>
        /// GameObject.
        /// </summary>
        public void AddAnchorToTrackingTarget()
        {
            if (this.trackingTarget == null ||
                this.trackingTarget.GetComponentInParent<TrackingAnchor>() != null)
            {
                return;
            }

            CreateTrackingAnchorAndAddAsChildren(this.trackingTarget);
        }

        /// <summary>
        /// Removes the <see cref="TrackingAnchor"/> from the hierarchy and re-parents its children.
        /// </summary>
        public void RemoveAnchorFromTrackingTarget()
        {
            if (this.trackingTarget == null)
            {
                return;
            }

            var trackingAnchor = this.trackingTarget.GetComponentInParent<TrackingAnchor>();
            if (trackingAnchor == null)
            {
                return;
            }

            TrackingObjectHelper.RemoveTrackingMeshesInSubTree(trackingAnchor.gameObject);
            RemoveGameObjectAndReparentChildren(trackingAnchor.gameObject);
        }

        /// <summary>
        /// Creates a <see cref="TrackingAnchor"/> GameObject and adds it as a parent to the specified
        /// <paramref name="gameObject"/>.
        /// Also configures rendering properties and selects the MeshFilter as tracking geometry.
        /// </summary>
        /// <param name="gameObject">
        /// The GameObject to which the <see cref="TrackingAnchor"/> will be added as a parent.
        /// </param>
        private static void CreateTrackingAnchorAndAddAsChildren(GameObject gameObject)
        {
            // Create a new GameObject with a TrackingAnchor
            var trackingAnchorGO = new GameObject(
                "TrackingAnchor",
                new Type[] {typeof(TrackingAnchor)});

            // Add a RenderedObject to the TrackingAnchor to use the TrackingGeometry as augmentation.
            var renderedObject = trackingAnchorGO.AddComponentUndoable<RenderedObject>();
            renderedObject.SetTrackingAnchor(trackingAnchorGO.GetComponent<TrackingAnchor>());
            renderedObject.renderMode = RenderedObject.RenderMode.Always;

            // Insert the new GameObject between the given gameObject and its parent. 
            trackingAnchorGO.transform.parent = gameObject.transform.parent;
            gameObject.transform.parent = trackingAnchorGO.transform;

            // Add TrackingMeshes to all MeshFilter of the TrackingAnchor. This will use them for tracking.
            TrackingObjectHelper.AddTrackingMeshesInSubTree(trackingAnchorGO);
        }

        /// <summary>
        /// Removes the specified <paramref name="gameObject"/> from the hierarchy and re-parents its
        /// children.
        /// </summary>
        /// <param name="gameObject">The GameObject to be removed from the hierarchy.</param>
        private static void RemoveGameObjectAndReparentChildren(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            for (var childIndex = 0; childIndex < gameObject.transform.childCount; childIndex++)
            {
                gameObject.transform.GetChild(childIndex).parent = gameObject.transform.parent;
            }
            gameObject.Destroy();
        }
    }
}
