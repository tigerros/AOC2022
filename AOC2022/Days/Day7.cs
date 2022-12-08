namespace AOC2022.Days;

internal sealed class Node {
	public string Name { get; }
	public Node? Parent { get; private set; }
	public List<Node>? Children { get; }
	private int _size;
	public int Size {
		get {
			if (Children == null) return _size;

			_size = 0;
				
			for (int i = 0; i < Children.Count; i++) {
				_size += Children[i].Size;
			}
				
			return _size;
		}
	}

	private Node(string name, int size, Node? parent, List<Node>? children) {
		Name = name;
		_size = size;
		Parent = parent;
		Children = children;
	}

	public static Node File(string name, int size) => new(name, size, null, null);
	public static Node Directory(string name) => new(name, 0, null, new List<Node>());

	public void Add(Node node) {
		if (Children == null) throw new Exception("Cannot add node to file.");
			
		Children.Add(node);
		node.Parent = this;
	}

	public IEnumerable<Node> Nodes() {
		var queue = new Queue<Node>();
		queue.Enqueue(this);

		while (queue.Count > 0) {
			Node current = queue.Dequeue();

			yield return current;

			if (current.Children == null) continue;

			foreach (Node child in current.Children) queue.Enqueue(child);
		}
	}

	public Node Root() {
		Node current = this;

		while (true) {
			if (current.Parent == null) break;
				
			current = current.Parent;
		}

		return current;
	}
}

internal sealed class Day7 : Day<Day7, int, int> {
	private const int DirectorySizeLimit = 100_000;
	private const int DiskSpace = 70_000_000;
	private const int UpdateSize = 30_000_000;
	private Node _current = Node.Directory("/");

	private void HandleCd(string dir) {
		if (dir == ".." && _current.Parent != null) {
			_current = _current.Parent;
			return;
		}

		Node newDir = Node.Directory(dir);
		_current.Add(newDir);
		_current = newDir;
	}

	private void HandleLs(string[] lines, int currentIndex) {
		for (int j = currentIndex + 1; j < lines.Length; j++) {
			if (lines[j][0] == '$') break;
			if (!char.IsDigit(lines[j][0])) continue;

			string[] split = lines[j].Split(' ');
			int size = int.Parse(split[0]);
			string name = split[1];
						
			_current.Add(Node.File(name, size));
		}
	}

	public Node ParseInput(string[] lines) {
		for (int i = 1; i < lines.Length; i++) {
			string line = lines[i];

			if (line.StartsWith("$ cd")) HandleCd(line[5..]);
			else if (line.StartsWith("$ ls")) HandleLs(lines, i);
		}

		return _current;
	}

	public override DayResult<int> Part1(int correctAnswer, bool isExample) {
		_current = Node.Directory("/");
		
		string[] lines = GetInputLines(isExample);
		
		// Less than 100 000 size directories size sum
		int eligibleDirsSizeSum = 0;

		Node root = ParseInput(lines).Root();

		foreach (Node node in root.Nodes()) {
			if (node.Children != null && node.Size < DirectorySizeLimit) eligibleDirsSizeSum += node.Size;
		}
		
		return new DayResult<int>(correctAnswer, eligibleDirsSizeSum);
	}

	public override DayResult<int> Part2(int correctAnswer, bool isExample) {
		_current = Node.Directory("/");
		
		string[] lines = GetInputLines(isExample);
		Node root = ParseInput(lines).Root();
		int usedSpace = root.Size;
		int unusedSpace = DiskSpace - usedSpace;
		int neededSpace = UpdateSize - unusedSpace;

		if (neededSpace <= 0) return new DayResult<int>(correctAnswer, 0);

		Node dirToDelete = root;

		foreach (Node node in root.Nodes()) {
			if (node.Children != null && node.Size >= neededSpace && node.Size < dirToDelete.Size) {
				dirToDelete = node;
			}
		}

		return new DayResult<int>(correctAnswer, dirToDelete.Size);
	}
}