using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Animou.App.Extensions
{
    public static class Upload
    {
        public static async Task<bool> UploadImage(IFormFile file, 
                                                    string imagePrefix, 
                                                    ModelStateDictionary modelState)
        {
            if (file.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), 
                                    "wwwroot/images/uploaded", 
                                    imagePrefix + file.FileName);

            if (File.Exists(path))
            {
                modelState.AddModelError(string.Empty, "There is a file whith this name already!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }
    }
}
