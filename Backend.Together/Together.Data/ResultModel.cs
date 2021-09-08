namespace Together.Data
{
    public class ResultModel<T>
    {
        public bool Success { get; set; }

        public bool Exception { get; set; }

        public string Message { get; set; }

        public T Result { get; set; }
    }
}
