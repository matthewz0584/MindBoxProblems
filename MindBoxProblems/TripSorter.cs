using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MindBoxProblems
{
    public class TripSorter
    {
        public IEnumerable<TripLeg> Legs { get; set; }

        public TripSorter(IEnumerable<TripLeg> legs)
        {
            Legs = legs.ToList();
        }

        public IEnumerable<TripLeg> Sort()
        {
            Func<IEnumerable<TripLeg>> LegsFromFirstToStart = () => TraverseInOneDirection(leg => leg.End);
            Func<IEnumerable<TripLeg>> LegsFromFirstToFinish = () => TraverseInOneDirection(leg => leg.Begin);

            return LegsFromFirstToStart().Skip(1).Reverse().Concat(LegsFromFirstToFinish());
        }

        private IEnumerable<TripLeg> TraverseInOneDirection(Func<TripLeg, string> EndSelector)
        {
            var citiesLegs = Legs.ToDictionary(EndSelector);
            for (var leg = Legs.FirstOrDefault(); leg != null; leg = citiesLegs.SafeGetValue(leg.OtherEnd(EndSelector(leg))))
                yield return leg;
        }
    }

    public class TripLeg
    {
        public TripLeg(string begin, string end)
        {
            Debug.Assert(!String.IsNullOrEmpty(begin), "!String.IsNullOrEmpty(begin)");
            Debug.Assert(!String.IsNullOrEmpty(end), "!String.IsNullOrEmpty(end)");

            Begin = begin;
            End = end;
        }

        public string Begin { get; private set; }
        public string End { get; private set; }

        public string OtherEnd(string end) { return end == Begin ? End : Begin; }
    }

    public static class Utils
    {
        public static U SafeGetValue<T, U>(this Dictionary<T, U> me, T key) where U : class
        {
            return me.ContainsKey(key) ? me[key] : null;
        }
    }
}