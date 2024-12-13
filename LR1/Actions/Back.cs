public class Back : Action {
    private int value;
    public Back(int value) {
        this.value = value;
    }
    public (int, string) Act(Stack<int> stateStack, Stack<string> symbolStack, int curState, string curSymbol, Lexer lexer) {
        string nextSymbol = lexer.GetCurEl();
        stateStack.Push(value);

        return (value, nextSymbol);
    }
}