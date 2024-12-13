public class RulesReader {
    public List<(string, List<string>)> Read(string path) {
        string readText = File.ReadAllText(path);

        List<string> rows = readText.Split("\r\n").ToList();
        rows.RemoveAt(rows.Count-1);
        List<List<string>> splitRows = new();

         rows.ForEach(r => {
            splitRows.Add(r.Split(' ').ToList());
        });

        List<(string, List<string>)> rules = new();

            splitRows.ForEach(r => {
            int index = r.IndexOf("''");
            try {
                r.RemoveAt(index);
            } catch (ArgumentOutOfRangeException) {}
            
            string symbol = r[0];
            r.RemoveAt(0);
            r.RemoveAt(0);
            
            rules.Add((symbol, r));
        });

        return rules;
    }
}