using Amazon.S3.Transfer;
using Amazon.S3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.Service;
using PRN231_Library_Project.Utils;
using Newtonsoft.Json;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private AdminService adminService;
        private readonly IConfiguration _configuration;


        public AdminController(AdminService adminService, IConfiguration configuration)
        {
            this.adminService = adminService;
            _configuration = configuration;
        }

        [HttpPost("secure/add/book")]
        [Authorize]
        public async Task<IActionResult> PostBook([FromHeader] string authorization)
        {
            try
            {
                string isAdmin = ExtractJWT.PayloadJWTExtraction(authorization, "\"userType\"");
                if (isAdmin == null || !isAdmin.Equals("admin"))
                {
                    return Unauthorized("Just for admin!");
                }


                var file = Request.Form.Files.GetFile("file");
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded");
                }

                var bookJson = Request.Form["book"];
                var book = JsonConvert.DeserializeObject<AddBookRequest>(bookJson);

                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded");
                }

                var accessKey = _configuration["AWS:AccessKey"];
                var secretKey = _configuration["AWS:SecretKey"];
                var bucketName = _configuration["AWS:BucketName"];
                var region = _configuration["AWS:Region"];

                using (var client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.GetBySystemName(region)))
                using (var transferUtility = new TransferUtility(client))
                {
                    using (var stream = file.OpenReadStream())
                    {
                        await transferUtility.UploadAsync(stream, bucketName, file.FileName);
                    }
                }

                book.FilePath = $"https://{bucketName}.s3.{region}.amazonaws.com/{file.FileName}";


                adminService.PostBook(book);

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        //[HttpPost]
        //public IActionResult UploadFile(IFormFile file)
        //{
        //    try
        //    {
        //        if (file == null || file.Length == 0)
        //            return BadRequest("No file uploaded");

        //        string filePath = Path.Combine("BooksContent", file.FileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            file.CopyTo(fileStream);
        //        }

        //        // Lưu đường dẫn vào cơ sở dữ liệu
        //        //var fileEntity = new FileEntity { FilePath = filePath };
        //        //_dbContext.Files.Add(fileEntity);
        //        //_dbContext.SaveChanges();

        //        // Đọc file PDF (nếu cần) và thực hiện xử lý tương ứng

        //        return Ok("File uploaded successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            var accessKey = _configuration["AWS:AccessKey"];
            var secretKey = _configuration["AWS:SecretKey"];
            var bucketName = _configuration["AWS:BucketName"];
            var region = _configuration["AWS:Region"];

            using (var client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.GetBySystemName(region)))
            using (var transferUtility = new TransferUtility(client))
            {
                using (var stream = file.OpenReadStream())
                {
                    await transferUtility.UploadAsync(stream, bucketName, file.FileName);
                }
            }

            return Ok("File uploaded successfully");
        }

        [HttpPut("secure/increase/book/quantity")]
        [Authorize]
        public IActionResult IncreaseBookQuantity([FromHeader] string authorization, [FromQuery] int bookId)
        {
            string isAdmin = ExtractJWT.PayloadJWTExtraction(authorization, "\"userType\"");
            if (isAdmin == null || !isAdmin.Equals("admin"))
            {
                return Unauthorized("Just for admin!");
            }
            adminService.IncreaseBookQuantity(bookId);
            return NoContent();
        }

        [HttpPut("secure/decrease/book/quantity")]
        [Authorize]
        public IActionResult DecreaseBookQuantity([FromHeader] string authorization, [FromQuery] int bookId)
        {
            string isAdmin = ExtractJWT.PayloadJWTExtraction(authorization, "\"userType\"");
            if (isAdmin == null || !isAdmin.Equals("admin"))
            {
                return Unauthorized("Just for admin!");
            }
            adminService.DecreaseBookQuantity(bookId);
            return NoContent();

        }

        [HttpDelete("secure/delete/book")]
        [Authorize]

        public IActionResult DeleteBook([FromHeader] string authorization, [FromQuery] int bookId)
        {
            try
            {
                string isAdmin = ExtractJWT.PayloadJWTExtraction(authorization, "\"userType\"");
                if (isAdmin == null || !isAdmin.Equals("admin"))
                {
                    return Unauthorized("Just for admin!");
                }
                adminService.DeleteBook(bookId);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

    }
}
