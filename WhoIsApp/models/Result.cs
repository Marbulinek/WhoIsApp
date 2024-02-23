namespace WhoIsApp.models
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public T Payload { get; set; }
    }
}
