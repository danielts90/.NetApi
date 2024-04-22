namespace MarketPlace.Helpers
{
    public class CustomResponse<T>(bool Succcess, string? Message, T Data)
    {
        public bool Succcess { get; } = Succcess;
        public string? Message { get; } = Message;
        public T? Data { get; } = Data;
    }
}
