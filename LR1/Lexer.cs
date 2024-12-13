public interface Lexer {
    void SplitLine();
    string GetCurEl();
    void NextEl();
    (string, List<string>) GetRules(int index);
}