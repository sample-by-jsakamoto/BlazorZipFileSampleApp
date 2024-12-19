using BlazorZipSampleApp.Data;
using System.Text;

namespace BlazorZipSampleApp.Components
{
    public class ComponentMetadata
    {
        public required Type Type { get; set; }

        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }

    public static class ContentFactory
    {
        private static HashSet<string> _imageExtensions = new HashSet<string>() { ".jpg", ".jpeg", ".png" };
        private static HashSet<string> _textExtensions = new HashSet<string>() { ".txt", ".log", ".text" };

        public static ComponentMetadata CreateMetaData(ZipContent content)
        {
            try
            {
                var fileNameLower = content.Name.ToLower();

                if (_imageExtensions.Any(x => fileNameLower.EndsWith(x)))
                {
                    return new ComponentMetadata() { Type = typeof(ImagePreview), Parameters = new() { { "Base64Str", Convert.ToBase64String(content.Bytes) } } };
                }
                if (_textExtensions.Any(x => fileNameLower.EndsWith(x)))
                {
                    return new ComponentMetadata() { Type = typeof(TextPreview), Parameters = new() { { "Text", Encoding.UTF8.GetString(content.Bytes) } } };
                }
                return new ComponentMetadata() { Type = typeof(NotSupportedPreview) };
            }
            catch (Exception e)
            {
                return new ComponentMetadata() { Type = typeof(ErrorPreview), Parameters = new() { { "Exception", e } } };
            }
        }
    }
}
