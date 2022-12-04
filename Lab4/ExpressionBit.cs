namespace Lab4
{
    public class ExpressionBit : IComparable<ExpressionBit>
    {
        public string Data { get; set; }
        public int Key { get; set; }
        public bool isOperator { get; set; }
        public int Priority { get; set; }

        public int CompareTo(ExpressionBit? other)
        {
            return this.Key - other.Key;
        }

        public override string ToString()
        {
            return Data;
        }
    }
}
