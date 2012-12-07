﻿using Angela.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angela.Tests
{
    [TestFixture]
    class AngieTests
    {

        [Test]
        public void StringInNewClassIsPopulated()
        {
            var person = Angie.FastMake<Person>();
            Assert.IsTrue(!string.IsNullOrEmpty(person.FirstName));
        }

        [Test]
        public void IntInNewClassIsPopulated()
        {
            var person = Angie.FastMake<Person>();
            Assert.IsTrue(person.Age != default(int));
        }

        [Test]
        public void BasicTypesInExistingClassArePopulated()
        {
            var person = Angie.FastFill<Person>(new Person());

            // for test brievity
            Assert.IsTrue(!string.IsNullOrEmpty(person.FirstName), "String property was not populated. Aborting additional asserts in test.");
            Assert.IsTrue(person.Age != default(int), "Int property was not populated. Aborting additional asserts in test.");
        }

        [Test]
        public void PopulatedBasicTypesInExistingClassRemainsUnchanged()
        {
            var firstName = "Angie";
            var age = 29;
            var person = Angie.FastFill<Person>(new Person { FirstName = firstName, Age = age });

            // for test brievity
            Assert.AreEqual(person.FirstName, firstName, "String property was altered. Aborting additional asserts in test.");
            Assert.AreEqual(person.Age, age, "Int property was altered. Aborting additional asserts in test.");
        }

        [Test]
        public void IntMaxNotExceededOnGeneratedValue()
        {
            var maxAge = 5;

            for (int i = 0; i < 2500; i++)
            {
                var person = Angie
                    .Configure()
                    .MaxInt(maxAge)
                    .Make<Person>();

                if (!(person.Age <= maxAge))
                    Assert.Fail("Int max was exceeded: {0} ", person.Age);
            }

        }
        
        [Test]
        public void IntMinNotExceededOnGeneratedValue()
        {
            var minAge = 5;

            for (int i = 0; i < 2500; i++)
            {
                var person = Angie
                    .Configure()
                    .MinInt(minAge)
                    .Make<Person>();

                if (!(person.Age >= minAge))
                    Assert.Fail("Int min was exceeded: {0} ", person.Age);
            }

            

        }
        
        [Test]
        public void IntRangeWithinBoundsOnGeneratedValue()
        {
            var success = true;

            // use a small window to try to force collisions
            var minAge = 20;
            var maxAge = 22;

            for (int i = 0; i < 1000; i++)
            {
                var person = Angie
                    .Configure()
                    .IntRange(minAge, maxAge)
                    .Make<Person>();

                if (!(person.Age >= minAge && person.Age <= maxAge))
                    success = false;
                else
                    Assert.IsTrue(success, "Int was generated outside of range.{0}", person.Age);
            }

            

        }

        [Test]
        public void AgeIsAlwaysPositive()
        {
            var person = Angie.FastMake<Person>();
            Assert.IsTrue(person.Age >= 0);
        }

        [Test]
        public void DefaultListGenerates25Entries()
        {
            var people = Angie.FastList<Person>();

            foreach (var person in people)
            {
                // just because we can :)
                Debug.WriteLine("{0} {1}", person.FirstName, person.LastName);
            }

            Assert.AreEqual(Angie.Defaults.DEFAULT_LIST_COUNT, people.Count(),
                string.Format("Expected {0} but collection contained {1}", 
                Angie.Defaults.DEFAULT_LIST_COUNT, people.Count())
                );
        }

    }
}