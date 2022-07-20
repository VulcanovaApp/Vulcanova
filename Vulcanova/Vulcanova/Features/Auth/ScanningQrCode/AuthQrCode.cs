using System;

namespace Vulcanova.Features.Auth.ScanningQrCode;

public record AuthQrCode(string ApiAddress, string Token)
{
    public static AuthQrCode FromQrString(string qr)
    {
        var values = QrCodeDecoder.DecodeQrCode(qr).Split('#');

        if (values.Length != 4)
        {
            throw new ArgumentException("Invalid QR code", nameof(qr));
        }

        return new AuthQrCode(values[1], values[2]);
    }
}