using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Vulcanova.Features.Auth.ScanningQrCode;

// Based on https://github.com/wulkanowy/qr/blob/master/dotnet/Wulkanowy.Qr/Qr.cs
public class QrCodeDecoder
{
    private const string QrKey = "tDVS4ykCBBAeN33h";
        
    public static string DecodeQrCode(string qrCode)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(QrKey);
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.PKCS7;

        using var dec = aes.CreateDecryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream(Convert.FromBase64String(qrCode));
        using var cs = new CryptoStream(memoryStream, dec, CryptoStreamMode.Read);
        using var reader = new StreamReader(cs);

        return reader.ReadToEnd();
    }
}