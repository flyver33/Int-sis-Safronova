public class State3: State { // ac: 1, re: 1, er: 1, st: 0
    int stateNumber;
    Lexer lexer;
    Stack<int?> stack;

    public State3(int stateNumber, Lexer lexer, Stack<int?> stack) {
        this.stateNumber = stateNumber;
        this.lexer = lexer;
        this.stack = stack;
    }

    public int? Next() {
        lexer.Error(stateNumber);
        lexer.Accept();

        return stack.Pop();
    }
}