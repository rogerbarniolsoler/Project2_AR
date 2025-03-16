using UnityEngine;
using UnityEngine.UI;
using Visometry.VisionLib.SDK.Core;
using Visometry.VisionLib.SDK.Core.Details;

namespace Visometry.VisionLib.SDK.Examples
{
    /// @ingroup Examples
    [HelpURL(DocumentationLink.APIReferenceURI.Examples + "u_eye_file_access.html")]
    public class UEyeFileAccess : MonoBehaviour
    {
        public InputField inputField;
        public UEyeCameraParameters parameters;
        
        private void Awake()
        {
            if (this.inputField == null)
            {
                this.inputField = GetComponent<InputField>();
            }
            
            if (this.parameters == null)
            {
                this.parameters = GetComponent<UEyeCameraParameters>();
            }
        }

        public void Load()
        {
            if (this.inputField == null)
            {
                LogHelper.LogWarning("'InputField' is null", this);
                return;
            }
            if (this.parameters == null)
            {
                LogHelper.LogWarning("'Parameters' is null", this);
                return;
            }
            this.parameters.LoadParametersFromFile(this.inputField.text);
        }
        
        public void Save()
        {
            if (this.inputField == null)
            {
                LogHelper.LogWarning("'InputField' is null", this);
                return;
            }
            if (this.parameters == null)
            {
                LogHelper.LogWarning("'Parameters' is null", this);
                return;
            }
            this.parameters.SaveParametersToFile(this.inputField.text);
        }
    }
}
