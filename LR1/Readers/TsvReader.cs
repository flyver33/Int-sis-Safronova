public class TsvReader {
    public TsvReader() {}

    public Dictionary<int, Dictionary<string, Action>> Read(string path, Dictionary<string, Func<int, Action>> actionsDict) {
        string readText = File.ReadAllText(path);

        List<string> rows = readText.Split("\r\n").ToList();

        Dictionary<int, Dictionary<string, Action>> actionTable = new();

        List<string> symbols = rows[0].Split('\t').ToList();
        symbols.RemoveAt(0);

        rows.RemoveAt(rows.Count-1);
        rows.RemoveAt(0);

        rows.ForEach(r => {
            List<string> items = r.Split('\t').ToList();

            int state = int.Parse(items[0]);
            items.RemoveAt(0);

            Queue<Action> actions = new();
            items.ForEach(i => {
                string actionString = i[0].ToString();
                int value = int.Parse(i.Substring(1));

                Action action = actionsDict[actionString](value);

                actions.Enqueue(action);
            });

            actionTable.Add(state, new Dictionary<string, Action>());

            symbols.ForEach(s => {
                actionTable[state].Add(s, actions.Dequeue());
            });
        });

        return actionTable;
    }
}