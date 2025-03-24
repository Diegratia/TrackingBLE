namespace TrackingBle.src.Common.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
        public CollectionData<T> Collection { get; set; }
    }

    public class CollectionData<T>
    {
        public T Data { get; set; }
    }
}