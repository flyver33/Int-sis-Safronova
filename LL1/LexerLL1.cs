using System.Text.RegularExpressions;

public class LexerLL1 : Lexer {

    string line;
    public List<string> statement = new();
    int curElement = 0;
    Dictionary<int, List<string>> stateSymbols;

    public LexerLL1(string line, Dictionary<int, List<string>> stateSymbols) {
        this.line = line;
        this.stateSymbols = stateSymbols;

    }
    public void Error(int stateNumber) {
        string? match = stateSymbols[stateNumber].Find(s => s == statement[curElement]);
        int l = match.Length;

    }
    public void Accept() {
        curElement++;
    }
    public void SplitLine() {
        string lineWithEnd = line + " #";
        List<string> lines = lineWithEnd.Split().ToList();

        Regex regexDigits = new Regex("^\\d+$");
        Regex regexID = new Regex("^[A-Za-z_][\\w_]*$");
        Regex regexBoolean = new Regex("^true$|^false$");
        Regex regexFunc = new Regex("^func$");

        lines.ForEach(s => {
            string replacement;

            // Console.WriteLine(s);
            
            replacement = regexBoolean.Replace(s, "1Boolean");
            replacement = regexFunc.Replace(replacement, "1Func");
            replacement = regexID.Replace(replacement, "ID");
            replacement = regexDigits.Replace(replacement, "Digit");

            // Console.WriteLine(replacement);

            statement.Add(replacement);

        });

        // statement.ForEach(s => {
        //     Console.WriteLine(s);
        // });
        
    }

    public string GetCurEl() {
        return statement[curElement];
    }
}