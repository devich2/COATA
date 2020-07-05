namespace BLL.DTO.Attribute
{
    public class HttpStatusAttribute : System.Attribute
    {
        public int Value { get; private set; }
        public HttpStatusAttribute(int statusCode)
        {
            Value = statusCode;
        }
    }
}