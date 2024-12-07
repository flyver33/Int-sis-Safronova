public class State1: State { // ac: 0, re: 0, er: 1, st: 1
    int stateNumber;
    int? next;
    Lexer lexer;
    Stack<int?> stack;

    public State1(int stateNumber, Lexer lexer, Stack<int?> stack, int? next) {
        this.stateNumber = stateNumber;
        this.lexer = lexer;
        this.stack = stack;
        this.next = next;
    }

    public int? Next() {
        lexer.Error(stateNumber);
        stack.Push(stateNumber + 1);

        return next;
    }
}