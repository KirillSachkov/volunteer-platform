using CSharpFunctionalExtensions;
using Minio;
using Minio.DataModel.Args;
using VolunteerPlatform.Application.Services;
using VolunteerPlatform.Domain.Entities;

namespace VolunteerPlatform.Infrastructure.Services;

public class MinioService : IMinioService
{

    private readonly IMinioClient _minioClient;

    public MinioService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<Result> UploadImage(Stream stream, MainPhoto mainPhoto, CancellationToken ct)
    {
        var bucketArgs = new BucketExistsArgs()
            .WithBucket(MainPhoto.BUCKET_NAME);

        var bucketExists = await _minioClient.BucketExistsAsync(bucketArgs, ct);
        if (bucketExists == false)
        {
            var makeBucketArgs = new MakeBucketArgs()
                .WithBucket(MainPhoto.BUCKET_NAME);

            await _minioClient.MakeBucketAsync(makeBucketArgs, ct);
        }

        var putObjectAtgs = new PutObjectArgs()
            .WithBucket(MainPhoto.BUCKET_NAME)
            .WithObject(mainPhoto.Path)
            .WithContentType("application/octet-stream")
            .WithStreamData(stream)
            .WithObjectSize(stream.Length);

        var response = await _minioClient.PutObjectAsync(putObjectAtgs, ct);

        var statObjectArgs = new StatObjectArgs()
            .WithBucket(MainPhoto.BUCKET_NAME)
            .WithObject(mainPhoto.Path);
        
        var stat = await _minioClient.StatObjectAsync(statObjectArgs, ct);
        
        var imageStream = new MemoryStream();

        var getObjectArgs = new GetObjectArgs()
            .WithBucket(MainPhoto.BUCKET_NAME)
            .WithObject(mainPhoto.Path)
            .WithCallbackStream(s =>
            {
                s.CopyTo(imageStream);
            });

        var result = await _minioClient.GetObjectAsync(getObjectArgs, ct);
        
        Console.WriteLine(imageStream.Length);
        
        return Result.Success();
    }

    public async Task<Stream> GetImage(string fileName, CancellationToken ct)
    {
        var imageStream = new MemoryStream();

        var getObjectArgs = new GetObjectArgs()
            .WithBucket(MainPhoto.BUCKET_NAME)
            .WithFile(fileName)
            .WithCallbackStream(stream => stream.CopyTo(imageStream));

        await _minioClient.GetObjectAsync(getObjectArgs, ct);

        return imageStream;
    }
}