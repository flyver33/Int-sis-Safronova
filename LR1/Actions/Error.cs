public class Error : Action {
    private int value;
    public Error(int value) {
        this.value = value;
    }
    public (int, string) Act(Stack<int> stateStack, Stack<string> symbolStack, int curState, string curSymbol, Lexer lexer) {
        throw new DoesNotBelongGrammarException();
    }
}