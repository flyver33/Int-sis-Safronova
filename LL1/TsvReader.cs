public class TsvReader {
    public TsvReader() {}

    public (Dictionary<int, int?>, Dictionary<int, List<string>>) Read(string path) {
        string readText = File.ReadAllText(path);

        List<string> rows = readText.Split("\n").ToList();

        Dictionary<int, int?> nextStates =  new();
        Dictionary<int, List<string>> stateSymbols = new();

        rows.RemoveAt(rows.Count-1);

        rows.ForEach(r => {
            string[] items = r.Split('\t');
            try {
                nextStates.Add(int.Parse(items[0]), int.Parse(items[2]));
            }
            catch (FormatException) {
                nextStates.Add(int.Parse(items[0]), null);
            }

            stateSymbols.Add(int.Parse(items[0]), items[1].Split(" ").ToList());
        });

        return (nextStates, stateSymbols);
    }
}