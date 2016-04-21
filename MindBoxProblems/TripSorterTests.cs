using System;
using System.Linq;
using NUnit.Framework;

namespace MindBoxProblems
{
    public class TripSorterTests
    {
        [Test]
        public void Acceptance()
        {
            var trip = new TripSorter(new[] { new TripLeg("Мельбурн", "Кельн"), new TripLeg("Москва", "Париж"), new TripLeg("Кельн", "Москва") }).Sort();

            Assert.That(trip.Select(l => l.Begin), Is.EqualTo(new[] { "Мельбурн", "Кельн", "Москва" }));
            Assert.That(trip.Select(l => l.End),               Is.EqualTo(new[] { "Кельн", "Москва", "Париж" }));
        }

        [Test]
        public void StayingAtHome()
        {
            Assert.That(new TripSorter(new TripLeg[0]).Sort(), Is.Empty);
        }

        [Test]
        public void OneLeg()
        {
            var trip = new TripSorter(new[] { new TripLeg("A", "B") }).Sort();

            Assert.That(trip.Single().Begin, Is.EqualTo("A"));
        }

        [Test]
        public void TwoLegs()
        {
            var trip = new TripSorter(new[] { new TripLeg("A", "C"), new TripLeg("C", "B") }).Sort();

            Assert.That(trip.Select(l => l.Begin), Is.EqualTo(new[] { "A", "C" }));
        }

        [Test]
        public void ThreeLegsInOrder()
        {
            var trip = new TripSorter(new[] { new TripLeg("A", "C"), new TripLeg("C", "B"), new TripLeg("B", "D") }).Sort();

            Assert.That(trip.Select(l => l.Begin), Is.EqualTo(new[] { "A", "C", "B" }));
        }

        [Test]
        public void ThreeLegsInReverseOrder()
        {
            var trip = new TripSorter(new[] { new TripLeg("B", "D"), new TripLeg("C", "B"), new TripLeg("A", "C") }).Sort();

            Assert.That(trip.Select(l => l.Begin), Is.EqualTo(new[] { "A", "C", "B" }));
        }
    }
}