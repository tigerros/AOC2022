namespace AOC2022;

internal readonly struct DayResult<TResult> {
	public bool IsCorrect { get; }
	public TResult CorrectResult { get; }
	public TResult Result { get; }
	
	public DayResult(TResult correctResult, TResult result) {
		if (correctResult == null) throw new ArgumentNullException(nameof(correctResult));
		
		IsCorrect = correctResult.Equals(result);
		CorrectResult = correctResult;
		Result = result;
	}

	public override string ToString() => $"IsCorrect: {IsCorrect}, Correct result: {CorrectResult}, Result: {Result}";
}