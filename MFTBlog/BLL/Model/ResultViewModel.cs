namespace BLL.Model
{
	public abstract class ResultViewModel
	{
		public bool IsSuccessful { get; set; }
	}

	public class ResultEntityViewModel<T> : ResultViewModel
	{
		public T Entity { get; set; }
		public Exception Exception { get; set; }
		public string Message { get; set; }
	}
}
