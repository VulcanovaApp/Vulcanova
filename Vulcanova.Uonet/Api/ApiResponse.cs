namespace Vulcanova.Uonet.Api
{
    public class Envelope
    {
        public string RestURL { get; set; }
        public int LoginId { get; set; }
        public string UserLogin { get; set; }
        public string UserName { get; set; }
    }

    public class Status
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class ApiResponse<T>
    {
        public string EnvelopeType { get; set; }
        public T Envelope { get; set; }
        public Status Status { get; set; }
        public string RequestId { get; set; }
        public long Timestamp { get; set; }
        public string TimestampFormatted { get; set; }
        public object InResponseTo { get; set; }
    }
}