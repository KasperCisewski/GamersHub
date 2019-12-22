using System;

namespace GamersHub.Api.Domain
{
    public class GameImage
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public long Length { get; set; }
        public string ContentType { get; set; }
    }
}
