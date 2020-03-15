namespace ISAPI.Model
{
    public class ServiceResponse<T>
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public T Response { get; set; }
    }
}
