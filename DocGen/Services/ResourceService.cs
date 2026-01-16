using Windows.ApplicationModel.Resources;

namespace DocGen.Services
{
    public class ResourceService
    {
        private ResourceLoader _resourceLoader = new ResourceLoader();
        public string GetLocalizedResource(string resourceKey)
        {
            try
            {
                var value = _resourceLoader.GetString(resourceKey);
                return string.IsNullOrEmpty(value) ? resourceKey : value;
            }
            catch
            {
                return resourceKey;
            }
        }
    }
}
