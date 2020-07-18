using System;
using System.Linq;
using AngleParse.Resource;
using AngleParse.Selector;
using AngleParse.Test.Resource.ElementResource;
using AngleParse.Test.Resource.StringResource;
using AngleParse.Test.Selector.StringSelector;
using Xunit;

namespace AngleParse.Test
{
    public class StringSelectorTest
    {
        // Should throw TypeInitializationException
        // private static readonly StringSelector invalidSelector = new InvalidStringSelector();
        private static readonly StringSelector validSelector = new ValidStringSelector();
        private static readonly StringSelector notMatchingSelector = new NotMatchingStringSelector();

        // AngleSharp tries to parse even broken HTML, so that never throws exception.
        private static readonly ElementResource invalidElementResource = new InvalidElementResource();
        private static readonly ElementResource validElementResource = new ValidElementResource();
        private static readonly ElementResource emptyElementResource = new EmptyElementResource();
        private static readonly StringResource validStringResource = new ValidStringResource();

        [Fact]
        public void InitializingInvalidSelectorThrowsTypeInitializationException()
        {
            Assert.Throws<TypeInitializationException>(() =>
            {
                var _ = new InvalidStringSelector();
            });
        }

        [Fact]
        public void SelectByNotMatchingSelectorReturnsEmptySequence()
        {
            var actual = notMatchingSelector.Select(validElementResource).Select(r => r.AsString());
            Assert.Empty(actual);
        }

        [Fact]
        public void SelectByValidSelectorWorks()
        {
            var expected = new[] {"Windows XP SP2", "Windows Server 2003 SP1", "general availability"};
            var actual = validSelector.Select(validElementResource).Select(r => r.AsString());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectOnEmptyResourceReturnsEmptySequence()
        {
            var actual = validSelector.Select(emptyElementResource).Select(r => r.AsString());
            Assert.Empty(actual);
        }

        [Fact]
        public void SelectOnInvalidResourceReturnsEmptySequence()
        {
            var actual = validSelector.Select(invalidElementResource).Select(r => r.AsString());
            Assert.Empty(actual);
        }

        [Fact]
        public void SelectOnStringResourceThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => validSelector.Select(validStringResource));
        }
    }
}