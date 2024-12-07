public class State6: State { // ac: 0, re: 0, er: 0, st: 0
    int? next;
    StateSymbols stateSymbols;
    int stateNumber;
    Lexer lexer;

    public State6(int stateNumber, StateSymbols stateSymbols, Lexer lexer, int? next) {
        this.next = next;
        this.stateSymbols = stateSymbols;
        this.stateNumber = stateNumber;
        this.lexer = lexer;
    }

    public int? Next() {
        try {
            string? sym = stateSymbols.GetDict()[stateNumber].Find(s => s == lexer.GetCurEl());
            int l = sym.Length;
        } catch (NullReferenceException) {
            return stateNumber + 1;
        }
        return next;
    }
}