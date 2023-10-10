using System.Collections.Generic;

namespace p23_ExpressionTrees
{
    public class Filter
    {
        /// <summary>
        /// Property name, which is filtering
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Filtering value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Operator between conditions (null for first)
        /// </summary>
        public ConditionKind ConditionKind { get; set; }

        /// <summary>
        /// Operator of filter
        /// </summary>
        public ConditionOperatorKind? ConditionOperatorKind { get; set; }

        public List<Filter> ChildFilters { get; set; }
    }
}