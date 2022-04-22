namespace OnlineAdvising.Core.Results
{
    public class Result<T>
    {
        public bool IsSucceeded { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }
    }
}