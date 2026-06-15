namespace labo_10.Domain.Dto;

public record FileDownloadResponse(byte[] FileBytes, string ContentType, string FileName);