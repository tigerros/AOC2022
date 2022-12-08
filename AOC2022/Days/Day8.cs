namespace AOC2022.Days;

internal sealed class Day8 : Day<Day8, int, int> {
	private static bool IsVisible(string[] lines, int y, int x) {
		int tree = lines[y][x];
		bool isVisible = false;

		for (int top = y - 1; top >= 0; top--) {
			int topTree = lines[top][x];

			if (topTree < tree) {
				isVisible = true;
			} else {
				isVisible = false;
				break;
			}
		}

		if (isVisible) return true;
		
		for (int bottom = y + 1; bottom < lines.Length; bottom++) {
			int bottomTree = lines[bottom][x];

			if (bottomTree < tree) {
				isVisible = true;
			} else {
				isVisible = false;
				break;
			}
		}
		
		if (isVisible) return true;
		
		for (int left = x - 1; left >= 0; left--) {
			int leftTree = lines[y][left];

			if (leftTree < tree) {
				isVisible = true;
			} else {
				isVisible = false;
				break;
			}
		}

		if (isVisible) return true;
		
		for (int right = x + 1; right < lines.Length; right++) {
			int rightTree = lines[y][right];

			if (rightTree < tree) {
				isVisible = true;
			} else {
				return false;
			}
		}

		return isVisible;
	}

	public override DayResult<int> Part1(int correctAnswer, bool isExample) {
		string[] lines = GetInputLines(isExample);
		int visibleTreeCount = ((lines.Length * 2) + (lines.Length * 2)) - 4;

		for (int y = 1; y < lines.Length - 1; y++) {
			for (int x = 1; x < lines.Length - 1; x++) {
				if (IsVisible(lines, y, x)) visibleTreeCount++;
			}
		}

		return new DayResult<int>(correctAnswer, visibleTreeCount);
	}

	private static int GetScenicScore(string[] lines, int y, int x) {
		int tree = lines[y][x];
		int topAmount = 0;
		int bottomAmount = 0;
		int leftAmount = 0;
		int rightAmount = 0;

		for (int top = y - 1; top >= 0; top--) {
			topAmount++;
			
			int topTree = lines[top][x];

			if (topTree >= tree) break;
		}

		for (int bottom = y + 1; bottom < lines.Length; bottom++) {
			bottomAmount++;
			
			int bottomTree = lines[bottom][x];

			if (bottomTree >= tree) break;
		}

		for (int left = x - 1; left >= 0; left--) {
			leftAmount++;
			
			int leftTree = lines[y][left];

			if (leftTree >= tree) break;
		}
		
		for (int right = x + 1; right < lines.Length; right++) {
			rightAmount++;
			
			int rightTree = lines[y][right];

			if (rightTree >= tree) break;
		}
		
		return topAmount * bottomAmount * leftAmount * rightAmount;
	}

	public override DayResult<int> Part2(int correctAnswer, bool isExample) {
		string[] lines = GetInputLines(isExample);
		int maxScenicScore = 0;
		
		for (int y = 1; y < lines.Length - 1; y++) {
			for (int x = 1; x < lines.Length - 1; x++) {
				int scenicScore = GetScenicScore(lines, y, x);

				if (scenicScore > maxScenicScore) maxScenicScore = scenicScore;
			}
		}
		
		return new DayResult<int>(correctAnswer, maxScenicScore);
	}
}