public class State2: State { // ac: 0, re: 1, er: 1, st: 0
    int stateNumber;
    Lexer lexer;
    Stack<int?> stack;

    public State2(int stateNumber, Lexer lexer, Stack<int?> stack) {
        this.stateNumber = stateNumber;
        this.lexer = lexer;
        this.stack = stack;
    }

    public int? Next() {
        lexer.Error(stateNumber);

        return stack.Pop();
    }
}