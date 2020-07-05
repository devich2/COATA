namespace BLL.DTO.Result
{
    public class DataResult<T> : Result
    {
        public T Data { get; set; }
    }
}