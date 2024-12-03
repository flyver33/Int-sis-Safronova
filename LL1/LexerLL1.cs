using System.Text.RegularExpressions;

public class LexerLL1 : Lexer {

    string line;
    List<string> statement = new();
    int curElement = 0;
    Dictionary<int, List<string>> stateSymbols;

    public LexerLL1(string line, Dictionary<int, List<string>> stateSymbols) {
        this.line = line;
        this.stateSymbols = stateSymbols;

    }
    public void Error(int stateNumber) {
        // try {
            stateSymbols[stateNumber].Find(s => s == statement[curElement]);
        // } catch (ArgumentNullException e) {
        //     Console.WriteLine("Выражение не относится к этой грамматике");
        //     throw e;
        // }

    }
    public void Accept() {
        curElement++;
    }
    public void SplitLine() {
        string lineWithEnd = line + " #";
        List<string> lines = lineWithEnd.Split().ToList();

        Regex regexDigits = new Regex("^\\d+$");
        Regex regexID = new Regex("^[A-Za-z|_][\\w|_]*");
        Regex regexBoolean = new Regex("^true$|^false$");
        Regex symbols = new Regex("\\+|\\-|\\/|\\*|\\(|\\)|#");

        lines.ForEach(s => {
            string replacement;
            
            replacement = regexBoolean.Replace(s, "1Boolean");
            replacement = regexID.Replace(replacement, "ID");
            replacement = regexDigits.Replace(replacement, "Digit");
            replacement = symbols.Replace(replacement, s);

            statement.Add(replacement);

        });
    }

    public string GetCurEl() {
        return statement[curElement];
    }
}