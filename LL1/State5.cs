public class State5: State { // ac: 1, re: 0, er: 1, st: 0
    int stateNumber;
    int? next;
    Lexer lexer;

    public State5(int stateNumber, Lexer lexer, int? next) {
        this.stateNumber = stateNumber;
        this.lexer = lexer;
        this.next = next;
    }

    public int? Next() {
        lexer.Error(stateNumber);
        lexer.Accept();

        return next;
    }
}