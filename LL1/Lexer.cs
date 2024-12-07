public interface Lexer {
    void Error(int stateNumber);
    void Accept();
    void SplitLine();
    string GetCurEl();
}