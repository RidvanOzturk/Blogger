namespace Blogger.Models.Responses;

public class ErrorResponseModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
