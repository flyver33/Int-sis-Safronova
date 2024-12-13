using System.Text.RegularExpressions;

public class LexerLR1 : Lexer {

    string line;
    public List<string> statement = new();
    int curElement = 0;
    List<(string, List<string>)> rules;

    public LexerLR1(string line, List<(string, List<string>)> rules) {
        this.line = line;
        this.rules = rules;

    }
    public (string, List<string>) GetRules(int index) {
        return rules[index];
    }
    public void NextEl() {
        curElement++;
    }
    public void SplitLine() {
        string lineWithEnd = line + " $";
        List<string> lines = lineWithEnd.Split().ToList();

        Regex regexDigits = new Regex("^\\d+$");
        Regex regexID = new Regex("^[A-Za-z_][\\w_]*$");
        Regex regexBoolean = new Regex("^true$|^false$");
        Regex regexFunc = new Regex("^func$");

        lines.ForEach(s => {
            string replacement;
            
            replacement = regexBoolean.Replace(s, "1Boolean");
            replacement = regexFunc.Replace(replacement, "1Func");
            replacement = regexID.Replace(replacement, "ID");
            replacement = regexDigits.Replace(replacement, "Digit");

            statement.Add(replacement);

        });
        
    }

    public string GetCurEl() {
        return statement[curElement];
    }
}