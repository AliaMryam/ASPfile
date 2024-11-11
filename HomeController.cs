using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace PaintingUploadApp.Controllers
{
    public class HomeController : Controller
    {
        // Display the Upload form
        public IActionResult Index()
        {
            return View();
        }

        // Handle the uploaded painting
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile painting)
        {
            if (painting != null && painting.Length > 0)
            {
                // Create a folder to store uploaded paintings if it doesn't exist
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                // Set the file path (save with original name)
                var filePath = Path.Combine(uploadDirectory, painting.FileName);

                // Save the uploaded painting to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await painting.CopyToAsync(stream);
                }

                // Return success message to the user
                ViewBag.Message = "Painting uploaded successfully!";
            }
            else
            {
                // If no file is uploaded, show an error message
                ViewBag.Message = "Please select a painting to upload.";
            }

            return View("Index");
        }
    }
}