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

        [Test]
        public void qq()
        {
            var sales = new[]
            {
                new {salesid = 10, productid = 2, datetime = DateTime.Now, customerid = 400},
                new {salesid = 20, productid = 3, datetime = DateTime.Now.AddMinutes(1), customerid = 400},
                new {salesid = 21, productid = 6, datetime = DateTime.Now.AddMinutes(1), customerid = 400},
                new {salesid = 30, productid = 2, datetime = DateTime.Now.AddMinutes(2), customerid = 500},
                new {salesid = 40, productid = 3, datetime = DateTime.Now.AddMinutes(3), customerid = 600},
            };

            var customersFirstPurchaseTimes = sales.GroupBy(s => s.customerid).Select(g => new { customerid = g.Key, datetime = g.Min(s => s.datetime)});
            var res = sales
                .Join(customersFirstPurchaseTimes, s => new { s.datetime, s.customerid }, sfpt => new { sfpt.datetime, sfpt.customerid }, (s, spft) => s.productid)
                .GroupBy(p => p)
                .Select(p => new { productid = p.Key, count = p.Count() });
            var resFull = sales
                .GroupBy(s => s.productid)
                .GroupJoin(res, pg => pg.Key, r => r.productid, (grouping, enumerable) => new { productid = grouping.Key, count = enumerable.Select(e => e.count).SingleOrDefault() });
            

            
            var qq = resFull.ToList();

//SELECT productid, COUNT(firstPurchaseTime)
//  FROM Sales
//  LEFT JOIN 
//    (SELECT customerid, MIN(datetime) firstPurchaseTime
//        FROM Sales
//        GROUP BY customerid) FirstPurchaseTimes
//  ON FirstPurchaseTimes.customerid = Sales.customerid AND FirstPurchaseTimes.firstPurchaseTime = Sales.datetime
//  GROUP BY productid
//GO
        }
    }
}