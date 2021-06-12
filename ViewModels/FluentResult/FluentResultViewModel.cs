namespace ViewModels.FluentResult
{
    public class FluentResultViewModel<T>
    {
        public T ValueOrDefault { get; set; }
        public T Value { get; set; }
        public bool IsFailed { get; set; }
        public bool IsSuccess { get; set; }
        public System.Collections.Generic.IList<Reason> Reasons { get; set; }
        public System.Collections.Generic.IList<Error> Errors { get; set; }
        public System.Collections.Generic.IList<Success> Successes { get; set; }
    }

    public class FluentResultViewModel
    {
        public bool IsFailed { get; set; }
        public bool IsSuccess { get; set; }
        public System.Collections.Generic.IList<Reason> Reasons { get; set; }
        public System.Collections.Generic.IList<Error> Errors { get; set; }
        public System.Collections.Generic.IList<Success> Successes { get; set; }
    }
}
