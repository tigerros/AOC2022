namespace AOC2022;

internal abstract class Day<TDay, TPart1Result, TPart2Result> where TDay : class, new() {
	private const string Path = "../../../Days/";
	
	private static string[] ResultLines => File.ReadAllLines(Path + typeof(TDay).Name + ".txt");
	private static string ResultText => File.ReadAllText(Path + typeof(TDay).Name + ".txt");
	private static string[] ExampleLines => File.ReadAllLines(Path + typeof(TDay).Name + "x.txt");
	private static string ExampleText => File.ReadAllText(Path + typeof(TDay).Name + "x.txt");
	
	private static TDay? _instance;
	public static TDay Instance => _instance ??= new TDay();

	protected static string[] GetInputLines(bool isExample) => isExample ? ExampleLines : ResultLines;
	protected static string GetInputText(bool isExample) => isExample ? ExampleText : ResultText;
	
	public abstract DayResult<TPart1Result> Part1(TPart1Result correctAnswer, bool isExample);
	public abstract DayResult<TPart2Result> Part2(TPart2Result correctAnswer, bool isExample);
}