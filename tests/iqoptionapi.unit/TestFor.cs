using System;
using System.Collections.Generic;
using System.Linq;

using AutoFixture;
using AutoFixture.AutoMoq;

using Moq;

using NUnit.Framework;

namespace IqOptionApi.unit {
    
     public abstract class TestFor<TUnit>  where TUnit : class
    {
        public virtual bool IsUsingFakeEngineContext { get; } = true;

        protected IFixture Fixture { get; private set; }

        private Lazy<TUnit> _lazyUnit;
        protected TUnit Unit => _lazyUnit.Value;

        [SetUp]
        public virtual void EveryTimeSetup()
        {
            _lazyUnit = new Lazy<TUnit>(CreateUnit);
        }

        protected virtual TUnit CreateUnit()
        {
            return Fixture.Create<TUnit>();
        }

        [SetUp]
        public void TestForSetup()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        protected T A<T>()
        {
            return Fixture.Create<T>();
        }

        protected List<T> Many<T>(int count = 3)
        {
            return Fixture.CreateMany<T>(count).ToList();
        }

        protected Mock<T> Mock<T>()
            where T : class
        {
            return new Mock<T>();
        }

        protected T Inject<T>(T instance)
            where T : class
        {
            Fixture.Inject(instance);
            return instance;
        }

        protected Mock<T> InjectMock<T>(params object[] args)
            where T : class
        {
            var mock = new Mock<T>(args);
            Fixture.Inject(mock.Object);
            return mock;
        }

        protected Mock<Func<T>> CreateFailingFunction<T>(T result, params Exception[] exceptions)
        {
            var function = new Mock<Func<T>>();
            var exceptionStack = new Stack<Exception>(exceptions.Reverse());
            function
                .Setup(f => f())
                .Returns(() =>
                {
                    if (exceptionStack.Any())
                    {
                        throw exceptionStack.Pop();
                    }

                    return result;
                });
            return function;
        }

        protected T Any<T>()
        {
            return It.IsAny<T>();
        }
    }
}