using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Visometry.VisionLib.SDK.Core;
using Visometry.VisionLib.SDK.Core.API;

namespace Visometry.VisionLib.SDK.Examples
{
    /// <summary>
    /// This component stores the parameter values of its corresponding TrackingAnchor in Awake.
    /// The parameters will be reset to these values every time the tracker is initialized or the
    /// function is called manually. 
    /// </summary>
    /// @ingroup Examples
    [RequireComponent(typeof(TrackingAnchor))]
    [HelpURL(DocumentationLink.APIReferenceURI.Examples + "temporary_parameter_storage.html")]
    public class TemporaryParameterStorage : MonoBehaviour
    {
        private TrackingAnchor anchor;
        private AnchorRuntimeParameters parameters;

        private void Awake()
        {
            if (this.anchor == null)
            {
                this.anchor = this.gameObject.GetComponent<TrackingAnchor>();
            }
            this.parameters = JsonUtility.FromJson<AnchorRuntimeParameters>(
                JsonUtility.ToJson(this.anchor.GetAnchorRuntimeParameters()));
            TrackingManager.OnTrackerInitializing += ResetParametersToStoredVersion;
        }

        private void OnDestroy()
        {
            TrackingManager.OnTrackerInitializing -= ResetParametersToStoredVersion;
        }

        /// <summary>
        /// Resets the parameters of the corresponding TrackingAnchor to the initial stored values.
        /// </summary>
        public void ResetParametersToStoredVersion()
        {
            TrackingManager.CatchCommandErrors(
                this.anchor.GetAnchorRuntimeParameters()
                    .SetParametersAsync(this.parameters, this.anchor),
                this);
        }

        /// <summary>
        /// Resets the parameters of the corresponding TrackingAnchor to the initial stored values
        /// and activates the line model afterwards. 
        /// </summary>
        public void ResetParametersToStoredVersionAndActivateShowLineModel()
        {
            TrackingManager.CatchCommandErrors(
                ResetParametersToStoredVersionAndActivateShowLineModelAsync(),
                this);
        }

        private async Task<WorkerCommands.CommandWarnings>
            ResetParametersToStoredVersionAndActivateShowLineModelAsync()
        {
            var warnings = await this.anchor.GetAnchorRuntimeParameters()
                .SetParametersAsync(this.parameters, this.anchor);
            var additionalWarnings = await this.anchor.SetShowLineModelAsync(true);
            if (additionalWarnings.warnings != null)
            {
                warnings.warnings =
                    warnings.warnings?.Concat(additionalWarnings.warnings).ToArray();
            }
            return warnings;
        }
    }
}
