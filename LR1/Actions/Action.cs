public interface Action {
    public (int, string) Act(Stack<int> stateStack, Stack<string> symbolStack, int curState, string curSymbol, Lexer lexer);
}
