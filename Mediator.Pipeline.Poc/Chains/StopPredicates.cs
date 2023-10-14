namespace Mediator.Pipeline.Poc.Chains
{
    public delegate bool StopPredicate<in TResult>(TResult result);

    public class StopPredicates<TResult>
    {
        private List<StopPredicate<TResult>> _predicates;

        internal void Add(StopPredicate<TResult> predicate)
        {
            _predicates ??= new List<StopPredicate<TResult>>();
            _predicates.Add(predicate);
        }

        public bool AnyMatch(TResult result)
        {
            if (_predicates == null) return false;

            return _predicates.Any(predicate => predicate(result));
        }
    }
}