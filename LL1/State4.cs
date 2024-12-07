public class State4: State { // ac: 0, re: 0, er: 1, st: 0
    int stateNumber;
    int? next;
    Lexer lexer;

    public State4(int stateNumber, Lexer lexer, int? next) {
        this.stateNumber = stateNumber;
        this.lexer = lexer;
        this.next = next;
    }

    public int? Next() {
        lexer.Error(stateNumber);

        return next;
    }
}