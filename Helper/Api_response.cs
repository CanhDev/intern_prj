namespace intern_prj.Helper
{
    public class Api_response
    {
        public bool? success { get; set; } = false;
        public string? message { get; set; } = null;
        public object? data { get; set; } = null;
    }
}
