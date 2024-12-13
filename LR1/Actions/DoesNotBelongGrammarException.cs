public class DoesNotBelongGrammarException : Exception {
    public DoesNotBelongGrammarException() : base() { }
    public DoesNotBelongGrammarException(string message) : base(message) { }
    public DoesNotBelongGrammarException(string message, Exception inner) : base(message, inner) { }
}