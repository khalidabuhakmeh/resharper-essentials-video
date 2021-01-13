using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using JetBrains.dotMemoryUnit;
using Models;
using Xunit;
using Xunit.Abstractions;

namespace Unicorn
{
    public class Tests
    {
        private readonly ITestOutputHelper output;

        private readonly Calculator calculator
            = new Calculator();

        public Tests(ITestOutputHelper output)
        {
            this.output = output;
            DotMemoryUnitTestOutput.SetOutputMethod(output.WriteLine);
        }

        [Fact]
        public void Add_1_plus_1_equals_2()
        {
            var actual = calculator.Add(1, 1);
            Assert.Equal(2, actual);
        }

        [Fact]
        public void Subtract_1_from_2_equals_1()
        {
            var actual = calculator.Subtract(2, 1);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void Multiply_2_and_2_equals_4()
        {
            var actual = calculator.Multiply(2, 2);
            Assert.Equal(4, actual);
        }

        [Fact]
        public void Divide_4_by_2_equals_2()
        {
            var actual = calculator.Divide(4, 2);
            Assert.Equal(2, actual);
        }

        [Fact]
        public void Divide_8_by_4_equals_2()
        {
            var actual
                = calculator.Divide(8, 4);
            Assert.Equal(2, actual);
        }

        [Fact]
        public void Will_Randomly_Fail()
        {
            var random = new Random();

            // have a 10% chance of failing
            Assert.NotEqual(1, random.Next(1, 11));
        }

        [Fact]
        public void Person_Test()
        {
            var person = new Person("bob", 21);
            Assert.NotNull(person);
        }

        [Fact, Category("Slow")]
        public void Slow_Test()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Assert.True(true);
        }

        [Fact, Category("Slow")]
        public void Very_Slow_Test()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Assert.True(true);
        }

        [Fact, DotMemoryUnit(FailIfRunWithoutSupport = false)]
        public void Profile_This_Method()
        {
            IDictionary<int, Person> list = new Dictionary<int, Person>();
            var checkpoint = dotMemory.Check();

            var count = 10_000;
            for (int i = 0; i < count; i++)
            {
                list.Add(i, new Person($"subject # {i}", i));
            }

            Assert.Equal(count, list.Count);

            dotMemory.Check(mem =>
            {
                var people = mem
                    .GetObjects(q => q.Type == typeof(Person));

                output.WriteLine($"Size of people in bytes is {people.SizeInBytes}");
               
                Assert.Equal(count, people.ObjectsCount);

                var difference = mem.GetDifference(checkpoint);
                
                output.WriteLine($"There are {difference.GetNewObjects().ObjectsCount} new objects since checkpoint.");
            });
        }

        // XUnit analyzer catches issues
        //[Fact]
        //public void Test(int number)
        //{

        //}
    }
}
