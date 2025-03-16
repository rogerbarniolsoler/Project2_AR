using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using Visometry.VisionLib.SDK.Core;

namespace Visometry.VisionLib.SDK.Examples
{
    /// <summary>
    ///  The InvariantCultureTextField makes it possible to set the text
    ///  of a Text component using different parameter types.
    /// </summary>
    /// @ingroup Examples
    [RequireComponent(typeof(Text))]
    [AddComponentMenu("VisionLib/Examples/Invariant Culture TextField")]
    [HelpURL(DocumentationLink.APIReferenceURI.Examples + "invariant_culture_text_field.html")]
    public class InvariantCultureTextField : MonoBehaviour
    {
        /// <summary>
        ///  Format specifies for the conversion to a string.
        /// </summary>
        /// <remarks>
        ///  See the documentation for "Standard Numeric Format Strings" in C# for
        ///  further details.
        /// </remarks>
        public string formatSpecifier;

        /// <summary>
        ///  Sets the text using a string.
        /// </summary>
        public void SetText(string text)
        {
            GetComponent<Text>().text = text;
        }

        /// <summary>
        ///  Sets the text using an integer.
        /// </summary>
        public void SetText(int value)
        {
            SetText(value.ToString(this.formatSpecifier, CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///  Sets the text using a floating point number.
        /// </summary>
        public void SetText(float value)
        {
            SetText(value.ToString(this.formatSpecifier, CultureInfo.InvariantCulture));
        }

        /// <summary>
        ///  Sets the text using a boolean.
        /// </summary>
        public void SetText(bool value)
        {
            SetText(value.ToString());
        }
    }
}
