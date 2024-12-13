public class Accept : Action {
    private int value;
    public Accept(int value) {
        this.value = value;
    }
    public (int, string) Act(Stack<int> stateStack, Stack<string> symbolStack, int curState, string curSymbol, Lexer lexer) {
        return (value, "accepted");
    }
}