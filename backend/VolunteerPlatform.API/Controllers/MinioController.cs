using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;

namespace VolunteerPlatform.API.Controllers;

[ApiController]
[Route("api/images")]
public class ImageController : ControllerBase
{
    private readonly IMinioClient _minioClient;

    public ImageController(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        await using (var stream = file.OpenReadStream())
        {
            var args = new PutObjectArgs()
                .WithBucket("bucket")
                .WithObject("image.jpg")
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType("application/octet-stream");

            await _minioClient.PutObjectAsync(args);
        }

        var statArgs = new StatObjectArgs()
            .WithBucket("bucket")
            .WithObject("image.jpg");

        var stat = await _minioClient.StatObjectAsync(statArgs);
        return Ok(stat.ObjectName + ": " + stat.ETag + " " + stat.VersionId);
    }

    [HttpGet("{imageName}")]
    public async Task<IActionResult> GetImage(string imageName)
    {
        try
        {
            var memoryStream = new MemoryStream();

            var args = new GetObjectArgs()
                .WithBucket("bucket")
                .WithObject(imageName)
                .WithCallbackStream(s => s.CopyTo(memoryStream));

            await _minioClient.GetObjectAsync(args);

            return File(memoryStream.ToArray(), "image/jpeg");
        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }

    [HttpGet("stat/{imageName}")]
    public async Task<IActionResult> GetImageStat(string imageName)
    {
        try
        {
            StatObjectArgs statObjectArgs = new StatObjectArgs()
                .WithBucket("bucket-name")
                .WithObject(imageName);

            var objectStat = await _minioClient.StatObjectAsync(statObjectArgs);

            return Ok(objectStat);
        }
        catch (Exception e)
        {
            return BadRequest("Error occurred: " + e);
        }
    }

    [HttpGet("bucket")]
    public async Task<IActionResult> GetBucker()
    {
        // IMinioClient minio = new MinioClient()
        //     .WithEndpoint("127.0.0.1:9000")
        //     .WithCredentials("minio", "minio123")
        //     .Build();

        try
        {
            var args = new MakeBucketArgs().WithBucket("bucket");

            await _minioClient.MakeBucketAsync(args);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest("Error occurred: " + e);
        }
    }
}