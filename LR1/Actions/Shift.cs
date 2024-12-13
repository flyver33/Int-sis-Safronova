public class Shift : Action {
    private int value;
    public Shift(int value) {
        this.value = value;
    }
    public (int, string) Act(Stack<int> stateStack, Stack<string> symbolStack, int curState, string curSymbol, Lexer lexer) {
        symbolStack.Push(curSymbol);
        lexer.NextEl();
        string nextSymbol = lexer.GetCurEl();

        stateStack.Push(value);
        int nextState = value;

        return (nextState, nextSymbol);
    }
}