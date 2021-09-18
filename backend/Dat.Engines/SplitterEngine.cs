using Dat.Model.Offer;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Dat.Engines
{
    public class SplitterEngine
    {
        [NotNull]
        private readonly List<ISplitter> Splitters;

        public SplitterEngine(List<ISplitter> splitters)
        {
            Splitters = splitters ?? throw new ArgumentNullException(nameof(splitters));
        }

        [return: NotNull]
        public Root Run([NotNull] Root sourceData) 
        {
            var result = sourceData;
            foreach (var splitter in Splitters)
            {
                result = splitter.Split(result);
            }
            return result;
        }
    }
}
