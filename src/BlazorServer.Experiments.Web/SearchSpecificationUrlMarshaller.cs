using System.IO.Compression;
using System.Text;
using System.Text.Json;
using BlazorServer.Experiments.Web.Application;
using Microsoft.AspNetCore.WebUtilities;

namespace BlazorServer.Experiments.Web;

public class SearchSpecificationUrlMarshaller : ISearchSpecificationFormatter, ISearchSpecificationParser
{
    public async Task<string> FormatAsync(SearchSpecification searchSpecification)
    {
        var jsonSerialized = JsonSerializer.Serialize(searchSpecification);
        var jsonSerializedBytes = Encoding.UTF8.GetBytes(jsonSerialized);
        var compressedJsonSerializedBytes = await Compress(jsonSerializedBytes);
        return Base64UrlTextEncoder.Encode(compressedJsonSerializedBytes);
    }

    public async Task<SearchSpecificationParserResult> ParseAsync(string s)
    {
        try
        {
            var compressedJsonSerializedBytes = Base64UrlTextEncoder.Decode(s);
            var jsonSerializedBytes = await Decompress(compressedJsonSerializedBytes);
            var jsonSerialized = Encoding.UTF8.GetString(jsonSerializedBytes);
            var searchSpecification = JsonSerializer.Deserialize<SearchSpecification>(jsonSerialized);
            return new SearchSpecificationParserResult { SearchSpecification = searchSpecification };
        }
        catch
        {
            return new SearchSpecificationParserResult { SearchSpecification = null };
        }
    }

    private static async Task<byte[]> Compress(byte[] bytes)
    {
        using var decompressed = new MemoryStream(bytes);
        using var compressed = new MemoryStream();
        await using (var compressor = new GZipStream(compressed, CompressionMode.Compress))
        {
            await decompressed.CopyToAsync(compressor);
        }

        return compressed.ToArray();
    }

    private static async Task<byte[]> Decompress(byte[] bytes)
    {
        using var compressed = new MemoryStream(bytes);
        using var decompressed = new MemoryStream();
        await using (var gzipStream = new GZipStream(compressed, CompressionMode.Decompress))
        {
            await gzipStream.CopyToAsync(decompressed);
        }

        return decompressed.ToArray();
    }
}